using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MediaOrganize.Models
{
    public class ThemoviedbResultSerie
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string? Title { get; set; }
        [JsonPropertyName("first_air_date")]
        public string? Date { get; set; }
    }
}
