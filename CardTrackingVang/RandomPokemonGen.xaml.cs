using CardTrackingVang.DTOs;
using CardTrackingVang.Services;

namespace CardTrackingVang;

public partial class RandomPokemonGen : ContentPage
{
    private readonly PokeApiService _pokeApiService = new();

    public RandomPokemonGen()
    {
        InitializeComponent();
    }

    private async void GenPokeBTN_Clicked(object sender, EventArgs e)
    {
        PokemonDTO pokemonDTO = await _pokeApiService.GetRandomPokemon();
        if(pokemonDTO != null) 
        {
            await DisplayAlertAsync("Caught!", $"{pokemonDTO.Name}\n{pokemonDTO.Types}\n{pokemonDTO.Weight}", "OK");
        }
    }
}