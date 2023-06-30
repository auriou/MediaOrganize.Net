using MediaOrganize.Models;
using System.Text.Json;
using System.Web;

namespace MediaOrganize.Service
{
    public class ThemoviedbSearch
    {
        public string Language { get; }
        public string Token { get; }

        public ThemoviedbSearch(string language, string token)
        {
            Language = language;
            Token = token;
        }

        public async Task<Movie> GetMovieAsync(string search, int year)
        {
            var data = await GetAsync<ThemoviedbResult<ThemoviedbResultMovie>>("search/movie?query={0}&include_adult=false&language={1}&page=1&year={2}", search, Language, year);
            var find = data?.results?.FirstOrDefault();
            if (find != null)
            {
                int.TryParse(find?.Date?.Substring(0, 4), out var yearMovie);
                var movie = new Movie { Title = find?.Title, Year = yearMovie };
                return movie;
            }
            return new();
        }

        public async Task<Serie> GetSerieAsync(string search, int season)
        {
            var data = await GetAsync<ThemoviedbResult<ThemoviedbResultSerie>>("/search/tv?query={0}&include_adult=false&language={1}&page=1", search, Language);
            var find = data?.results?.FirstOrDefault();
            if (find != null)
            {
                var seasonDetail = await GetAsync<ThemoviedbResultSeason>("/tv/{0}/season/{1}?language={2}", find.Id, season, Language);
                int.TryParse(seasonDetail?.Date?.Substring(0, 4), out var year);
                var serie = new Serie { Title = find?.Title, Year = year, Season = season };
                return serie;
            }
            return new();
        }

        private async Task<T> GetAsync<T>(string url, params object[] args) where T : class, new()
        {
            HttpClient client = new HttpClient();
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = HttpUtility.UrlPathEncode($"{args[i]}");
            }
            url = string.Format(url, args);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
            client.DefaultRequestHeaders.Add("accept", "application/json");
            var result = await client.GetAsync($"https://api.themoviedb.org/3/{url}");
            var stream = await result.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<T>(stream);
            return data ?? new();
        }
    }
}
