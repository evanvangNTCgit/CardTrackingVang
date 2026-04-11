using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CardTrackingVang.DTOs
{
    public class PokemonDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sprites")]
        public SpriteDTO Sprites { get; set; }

        [JsonProperty("weight")]
        public decimal Weight { get; set; }

        // https://stackoverflow.com/questions/39468096/how-can-i-parse-json-string-from-httpclient
        [JsonProperty("types")]
        public List<TypeDTO> Types { get; set; }
    }

    public class SpriteDTO
    {
        [JsonPropertyName("front_default")]
        public string FrontDefault { get; set; }

        [JsonPropertyName("back_default")]
        public string BackDefault { get; set; }
    }

    public class TypeDTO
    {
        [JsonProperty("slot")]
        public int Slot { get; set; }

        [JsonProperty("type")]
        public TypeInfoDTO Type { get; set; }
    }

    public class TypeInfoDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
