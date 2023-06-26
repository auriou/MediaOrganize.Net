using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using System.Text.Json;
using MediaOrganize.Models;
using System.Reflection.Metadata.Ecma335;

namespace MediaOrganize.Service
{
    internal class ThemoviedbSearch
    {
        public string Language { get; }
        public string Token { get; }

        public ThemoviedbSearch(string language, string token)
        {
            Language = language;
            Token = token;
        }

        public async Task<Movie> GetMovie(string search, int year)
        {
            var nameEncode = HttpUtility.UrlPathEncode(search);
            var data = await GetAsync<ThemoviedbResult<ThemoviedbResultMovie>>($"search/movie?query={nameEncode}&include_adult=false&language={Language}&page=1&year={year}");

            var find = data?.results?.FirstOrDefault();
            if (find != null)
            {
                int.TryParse(find?.Date?.Substring(0, 4), out var yearMovie);
                var movie = new Movie { Title = find?.Title, Year = yearMovie };
                return movie;
            }
            return new();
        }
        
        public async Task<Serie> GetSerie(string search, int season)
        {
            var nameEncode = HttpUtility.UrlPathEncode(search);
            var data = await GetAsync<ThemoviedbResult<ThemoviedbResultSerie>>($"/search/tv?query={nameEncode}&include_adult=false&language={Language}&page=1");          

            var find = data?.results?.FirstOrDefault();
            if (find != null)
            {
                var seasonDetail = await GetAsync<ThemoviedbResultSeason>($"/tv/{find.Id}/season/{season}?language={Language}");

                int.TryParse(seasonDetail?.Date?.Substring(0, 4), out var year);
                var serie = new Serie { Title = find?.Title, Year = year, Season = season };
                return serie;
            }
            return new();        
        }

        private async Task<T> GetAsync<T>(string url) where T : class, new()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
            client.DefaultRequestHeaders.Add("accept", "application/json");
            var result = await client.GetAsync($"https://api.themoviedb.org/3/{url}");
            var stream = await result.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<T>(stream);
            return data ?? new();
        }
    }
}
