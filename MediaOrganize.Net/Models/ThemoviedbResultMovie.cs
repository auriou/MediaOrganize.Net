using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MediaOrganize.Models
{
    public class ThemoviedbResultMovie
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("release_date")]
        public string? Date { get; set; }
        [JsonPropertyName("title")]
        public string? Title { get; set; }
    }
}
