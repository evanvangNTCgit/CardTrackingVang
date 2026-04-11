using CardTrackingVang.AiServices;
using CardTrackingVang.DataServices;
using CardTrackingVang.Models;
using CardTrackingVang.ViewModel;
using CommunityToolkit.Maui.Core;
using System.ComponentModel;

namespace CardTrackingVang;

public partial class AnalyzeCardImage : ContentPage, INotifyPropertyChanged
{
    private readonly ComputerVisionService _computerVisionService;
    private readonly ImageRecognitionViewModel _imageRecognitionViewModel;
    private readonly ChatService _client;
    private readonly DataService _dataService;
    private readonly CardsListViewModel _cardListViewModel;

    public AnalyzeCardImage(ICameraProvider camProvider, ComputerVisionService cvs, ChatService cs, DataService ds, CardsListViewModel clvm)
	{
        InitializeComponent();
        this._client = cs;
        this._computerVisionService = cvs;
        this._imageRecognitionViewModel = new(cvs);
        this._dataService = ds;
        this.BindingContext = this;
        this.AiActivityIndicator.BindingContext = this._imageRecognitionViewModel;
        this._cardListViewModel = clvm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        //// Ask for camera permission:
        //var cameraPermissionRequest = await Permissions.RequestAsync<Permissions.Camera>();
        //if (cameraPermissionRequest != PermissionStatus.Granted)
        //{
        //    await DisplayAlertAsync("ALERT", "Camera permissions are needed to use the AI image view.\nPlease allow camera permissions for this service", "OK");
        //} 
        }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
    }

    private async void SelectImageBTN_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Pick a photo from the gallery
            var photo = await MediaPicker.PickPhotoAsync();
            if(photo == null) 
            {
                await DisplayAlertAsync("ALERT", "Please try again and choose an image", "OK");
                return;
            }
            var result = await this._imageRecognitionViewModel.PickAndAnalyzeImage(photo);
            if (string.IsNullOrWhiteSpace(result))
            {
                return;
            }

            // outputs something like: sample output: Title: Sprigatito, Value: 15.00, CardType: Pokemon
            var cardThought = await this._client.MakeCardBasedOnText(result);
            Card AiGenCard = this._client.TranslateAiToCard(cardThought);

            if(AiGenCard != null) 
            {
               bool userWantsToAdd = await DisplayAlertAsync("SUCCESS", $"Card generated:\nTitle{AiGenCard.Title}\nValue: {AiGenCard.Value}\n\nWould you like to add this card?", "OK", "NO");

                if (userWantsToAdd) 
                {
                    // Use AppDataDirectory so the OS doesn't delete the image randomly
                    string fileName = $"{Guid.NewGuid()}_{photo.FileName}";
                    string localFilePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                    using (Stream sourceStream = await photo.OpenReadAsync())
                    using (FileStream localFileStream = File.OpenWrite(localFilePath))
                    {
                        await sourceStream.CopyToAsync(localFileStream);
                    }

                    AiGenCard.CardImage = new CardImage() { ImagePath = localFilePath, Card = AiGenCard };

                    this._cardListViewModel.AddCardWithModel(AiGenCard);

                    await Shell.Current.GoToAsync("//Main");
                }
                else
                {
                    return;
                }
            }
        }
        catch (FormatException) 
        {
            await DisplayAlertAsync("ALERT", "AI failed to recognize card, Please choose/take a better picture", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("ALERT", $"{ex.Message}", "OK");
        }
    }
}