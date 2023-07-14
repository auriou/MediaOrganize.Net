using System.Text.RegularExpressions;

namespace MediaOrganize.Service
{
    public class DirectoryScan
    {
        private readonly ThemoviedbSearch _tmdbSearch;
        private readonly string _mediaDirectory;
        private readonly string _movieDirectory;
        private readonly string _serieDirectory;
        private readonly string _filterMedia;

        const string _filterSerie = "(?<name>.*)[\\.-_]S(?<season>\\d+)\\s*E(?<episode>\\d+)";
        const string _filterMovie = "(?<name>.*)(?<date>\\d{4})\\..*";

        public DirectoryScan(ThemoviedbSearch tmdbSearch, 
            string mediaDirectory, string movieDirectory, 
            string serieDirectory, string scanFilter = "mkv|mp4|avi")
        {
            _filterMedia = $".*\\.({scanFilter})$";
            _tmdbSearch = tmdbSearch;
            _mediaDirectory = mediaDirectory;
            _movieDirectory = movieDirectory;
            _serieDirectory = serieDirectory;

            if (!Directory.Exists(_mediaDirectory))
            {
                Console.WriteLine("media path not exist !");
                return;
            }

            if (!Directory.Exists(_movieDirectory))
            {
                Directory.CreateDirectory(_movieDirectory);
            }

            if (!Directory.Exists(_serieDirectory))
            {
                Directory.CreateDirectory(_serieDirectory);
            }
        }

        private string Normalize(string fileName)
        {
            var invalids = Path.GetInvalidFileNameChars();
            var newName = string.Join("-", fileName.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');
            return newName;
        }

        public async Task ScanAsync()
        {
            foreach (var file in Directory.GetFiles(_mediaDirectory))
            {
                var fileName = Path.GetFileName(file);
                if (Regex.IsMatch(fileName, _filterMedia))
                {
                    var serieFilter = Regex.Match(fileName, _filterSerie);
                    if (serieFilter.Success) // series
                    {
                        var season = int.Parse(serieFilter.Groups["season"].Value);
                        var episode = int.Parse(serieFilter.Groups["episode"].Value);
                        var name = serieFilter.Groups["name"].Value.Trim('.').Replace('.', ' ');

                        var serie = await _tmdbSearch.GetSerieAsync(name, season);
                        if (serie.HasValue)
                        {
                            var nameSerie = Normalize(serie.Title);
                            var filename = $"{nameSerie} S{serie.Season:D2}E{episode:D2}" + Path.GetExtension(file);

                            var directorySerie = Path.Combine(_serieDirectory, nameSerie, $"Season {season:D2}{(serie.Year > 0 ? $" ({serie.Year})" : string.Empty)}");
                            if (!Directory.Exists(directorySerie))
                            {
                                Directory.CreateDirectory(directorySerie);
                            }
                            File.Copy(file, Path.Combine(directorySerie, filename), true);
                        }
                    }
                    else // movies
                    {
                        var movieFilter = Regex.Match(fileName, _filterMovie);
                        if (movieFilter.Success) // movies
                        {
                            var name = movieFilter.Groups["name"].Value.Trim('.').Replace('.', ' ');
                            var date = movieFilter.Groups["date"].Value;
                            int.TryParse(date, out var year);

                            var movie = await _tmdbSearch.GetMovieAsync(name, year);
                            if (movie.HasValue)
                            {
                                var nameMovie = Normalize(movie.Title);
                                var filename = $"{nameMovie}{(movie.Year > 0 ? $" ({movie.Year})" : string.Empty)}" + Path.GetExtension(file);

                                File.Copy(file, Path.Combine(_movieDirectory, filename), true);
                            }
                        }
                    }
                }
            }
            await Task.CompletedTask;
        }
    }
}
