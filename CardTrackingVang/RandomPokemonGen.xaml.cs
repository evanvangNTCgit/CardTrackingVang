using CardTrackingVang.DataServices;
using CardTrackingVang.DTOs;
using CardTrackingVang.Models;
using CardTrackingVang.Services;
using CardTrackingVang.ViewModel;
using Plugin.Maui.Audio;
using System.Text;

namespace CardTrackingVang;

public partial class RandomPokemonGen : ContentPage
{
    private readonly PokeApiService _pokeApiService = new();
    private readonly CardsListViewModel _cardListViewModel;
    private readonly DataService _dataService;
    private readonly IAudioManager _audioManager;
    private readonly HttpClient _httpClient = new();
    private PokemonDTO pokemonSpotted = null;


    public RandomPokemonGen(CardsListViewModel clvm, DataService ds, IAudioManager audioManager)
    {
        this._cardListViewModel = clvm;
        this._dataService = ds;
        this._audioManager = audioManager;

        InitializeComponent();
        this.pokmeonSprite.IsVisible = true;
    }

    private async void GenPokeBTN_Clicked(object sender, EventArgs e)
    {
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;

        if (accessType != NetworkAccess.Internet)
        {
            await DisplayAlertAsync("ALERT", "Please connect to a Wi-Fi source to catch a pokemon!", "OK");
            return;
        }

        PokemonDTO pokemonDTO = await _pokeApiService.GetRandomPokemon();
        if (pokemonDTO != null)
        {
            this.pokemonSpotted = pokemonDTO;

            if (!string.IsNullOrEmpty(this.pokemonSpotted.Sprites.FrontDefault))
            {
                this.pokmeonSprite.Source = string.Empty;
                this.pokmeonSprite.Source = new Uri(this.pokemonSpotted.Sprites.FrontDefault);
            }
            //await DisplayAlertAsync("Caught!", $"{pokemonDTO.Name}\n{pokemonDTO.Types}\n{pokemonDTO.Weight}\n\nWould you like to add {pokemonDTO.Name}?", "OK", );
            StringBuilder descBuilder = new();
            descBuilder.Append($" {this.pokemonSpotted.Name}\n");
            foreach (TypeDTO typeName in this.pokemonSpotted.Types)
            {
                descBuilder.Append($" {typeName.Type.Name}");
            }

            this.PokemonInfoText.Text = descBuilder.ToString();

            await this.PlayPokemonCry(this.pokemonSpotted);
        }
    }

    private async Task PlayPokemonCry(PokemonDTO pokemonTalking)
    {
        try
        {
            var audioBytes = await this._httpClient.GetByteArrayAsync(pokemonTalking.Cries.CrySrc);
            using (MemoryStream memoryStream = new MemoryStream(audioBytes))
            {
                using (IAudioPlayer player = this._audioManager.CreatePlayer(memoryStream))
                {
                    // Takes about half a second for image to load.
                    await Task.Delay(500);
                    player.Play();

                    // Player.duration returns seconds... So convert to milliseconds.
                    await Task.Delay((int)Math.Ceiling(player.Duration * 1000));
                }
            }
        }
        catch
        {
            // Just dont play the audio.
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

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (this.pokemonSpotted != null)
        {
            var test = VangAnimations.ThrowPokeBallAnimation(this.pokeballSprite, this.pokeballSprite);

            if (LoadingUserPreferences.GetUserAnimationPreference())
            {
                test.Item1.Commit(this, "PokeballThrow", length: 2500);


                await Task.Delay(2500);
            }

            if (test.Item2)
            {
                try
                {
                    CardType ct = this._dataService.GetCardType("Pokemon");
                    // If ct null originally...
                    ct ??= this._dataService.GetCardType(1);

                    string spriteURL = this.pokemonSpotted.Sprites.FrontDefault;
                    string FileName = this.pokemonSpotted.Name;
                    string imagePath = await this.DownloadSprite(spriteURL, FileName); // Adds GUID in method.

                    Card newPokemonCard = new Card { Title = this.pokemonSpotted.Name, CardTypeID = ct.Id, CardType = ct };

                    CardImage newPokeImage = new CardImage()
                    {
                        ImagePath = imagePath,
                        Card = newPokemonCard,
                    };

                    newPokemonCard.CardImage = newPokeImage;

                    this._cardListViewModel.AddCardWithModel(newPokemonCard);

                    await DisplayAlertAsync("SUCCESS", $"Congrats you caught at {this.pokemonSpotted.Name}", "OK");

                    await Shell.Current.GoToAsync("//MainPage");
                }
                catch (Exception ex)
                {
                    await DisplayAlertAsync("ALERT", $"Failed to add card\nPlease contact Evan Vang of error\n{ex.Message}", "OK");
                }
            }
        } else
        {
            await DisplayAlertAsync("ALERT", "You seem to have not caught a pokemon yet...\nClick select a random pokemon!", "OK");
        }
    }
}