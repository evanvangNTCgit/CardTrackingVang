using CardTrackingVang.DTOs;
using System.Text.Json;

namespace CardTrackingVang.Services
{
    public class PokeApiService
    {
        private Random rnd = new();

        HttpClient _client;
        JsonSerializerOptions _serializeOptions;

        public PokeApiService()
        {
            this._client = new();
            this._serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<PokemonDTO> GetRandomPokemon()
        {
                List<PokemonDTO> pokemon = new();

                Uri uri = new Uri($"https://pokeapi.co/api/v2/pokemon/{rnd.Next(1, 1000)}");
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    PokemonDTO pDTO = JsonSerializer.Deserialize<PokemonDTO>(content, _serializeOptions)!;

                    if (pDTO != null)
                    {
                        return pDTO;
                    }
                    else
                    {
                        return null!;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
    }
