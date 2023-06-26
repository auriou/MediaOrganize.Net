
using MediaOrganize.Service;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;

Console.WriteLine("Hello, World!");

var filePath = "C:\\Temp\\media";
var outputPath = "C:\\Temp\\media\\out";
var filterMedia = ".*\\.(mkv|mp4)$";
var filterSerie = "(?<name>.*)[\\.-_]S(?<season>\\d+)\\s*E(?<episode>\\d+)";

var dbSearch = new ThemoviedbSearch("fr-FR", Environment.GetEnvironmentVariable("token"));

var res = await dbSearch.GetSerie("From", 2);

var res2 = await dbSearch.GetMovie("Avatar: la voie de l'eau", 2022);

if (!Directory.Exists(filePath))
{
    Console.WriteLine("media path not exist !");
    return;
}

if(!Directory.Exists(outputPath))
{
    Directory.CreateDirectory(outputPath);
}
foreach(var file in Directory.GetFiles(filePath))
{
    var fileName = Path.GetFileName(file);
    if(Regex.IsMatch(fileName, filterMedia))
    {
        var serieFilter = Regex.Match(fileName, filterSerie);
        if (serieFilter.Success)
        {
            var season = int.Parse(serieFilter.Groups["season"].Value);
            var episode = int.Parse(serieFilter.Groups["episode"].Value);
            var name = serieFilter.Groups["name"].Value.Trim('.').Replace('.', ' ');
            var newFileName = $"{name} S{season:D2}E{episode:D2}" + Path.GetExtension(file);

            var google = new GoogleSearch();
            var id = await google.GetTitleId(name);

            var res3 = await dbSearch.GetSerie(name, season);


            //ttps://api.themoviedb.org/3/find/9813792?external_source=imdb_id&language=fr

            var directorySerie = Path.Combine(outputPath, name, $"Season {season}");
            if (!Directory.Exists(directorySerie))
            {
                Directory.CreateDirectory(directorySerie);
            }
            File.Copy(file, Path.Combine(directorySerie, newFileName), true);
        }
    }
}



