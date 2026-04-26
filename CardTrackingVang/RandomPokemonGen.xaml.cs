using CardTrackingVang.DataServices;
using CardTrackingVang.DTOs;
using CardTrackingVang.Models;
using CardTrackingVang.Services;
using CardTrackingVang.ViewModel;
using System.Text;

namespace CardTrackingVang;

public partial class RandomPokemonGen : ContentPage
{
    private readonly PokeApiService _pokeApiService = new();
    private readonly CardsListViewModel _cardListViewModel;
    private readonly DataService _dataService;

    public RandomPokemonGen(CardsListViewModel clvm, DataService ds)
    {
        this._cardListViewModel = clvm;
        this._dataService = ds;

        InitializeComponent();
    }

    private async void GenPokeBTN_Clicked(object sender, EventArgs e)
    {
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;

        if(accessType != NetworkAccess.Internet) 
        {
            await DisplayAlertAsync("ALERT", "Please connect to a Wi-Fi source to catch a pokemon!", "OK");
            return;
        }

        PokemonDTO pokemonDTO = await _pokeApiService.GetRandomPokemon();
        if(pokemonDTO != null) 
        {
            if (!string.IsNullOrEmpty(pokemonDTO.Sprites.FrontDefault))
            {
                if(DeviceInfo.Platform != DevicePlatform.WinUI) // For some reason the sprite doenst load right on windows but fine on phone?
                {
                    this.pokmeonSprite.Source = string.Empty;
                    this.pokmeonSprite.Source = new Uri(pokemonDTO.Sprites.FrontDefault); // GPT
                }
            }
            //await DisplayAlertAsync("Caught!", $"{pokemonDTO.Name}\n{pokemonDTO.Types}\n{pokemonDTO.Weight}\n\nWould you like to add {pokemonDTO.Name}?", "OK", );
            StringBuilder descBuilder = new();
            foreach (TypeDTO typeName in pokemonDTO.Types) 
            {
                descBuilder.AppendLine(typeName.Type.Name);
            }

            bool answer = await DisplayAlertAsync("CAUGHT!", $"Caught a: {pokemonDTO.Name}\nType: {descBuilder.ToString()}\nWeight: {pokemonDTO.Weight}\nWould you like to add {pokemonDTO.Name}?", "Yes", "No");

            if (answer)
            {
                try
                {
                    CardType ct = this._dataService.GetCardType("Pokemon");
                    if (ct == null)
                    {
                        ct = this._dataService.GetCardType(1);
                    }

                    string spriteURL = pokemonDTO.Sprites.FrontDefault;
                    string FileName = pokemonDTO.Name;
                    string imagePath = await this.DownloadSprite(spriteURL, FileName); // Adds GUID in method.

                    Card newPokemonCard = new Card { Title = pokemonDTO.Name, CardTypeID = ct.Id, CardType = ct };

                    CardImage newPokeImage = new CardImage()
                    {
                        ImagePath = imagePath,
                        Card = newPokemonCard,
                    };

                    newPokemonCard.CardImage = newPokeImage;

                    this._cardListViewModel.AddCardWithModel(newPokemonCard);

                    await Shell.Current.GoToAsync("//MainPage");
                } catch
                {
                    await DisplayAlertAsync("ALERT", "Failed to add card\nPlease contact Evan Vang of error", "OK");
                }
            }
        }
    }

    private async Task<string> DownloadSprite(string URL, string FileName) 
    {
        // Use AppDataDirectory so the OS doesn't delete the image randomly
        string fileName = $"{Guid.NewGuid()}_{FileName}";
        string localFilePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

        using var client = new HttpClient();
        var bytes = await client.GetByteArrayAsync(URL);
        await File.WriteAllBytesAsync(localFilePath, bytes);

        return localFilePath;
    }
}