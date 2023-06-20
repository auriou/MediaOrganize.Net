
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Web;

Console.WriteLine("Hello, World!");

var filePath = "C:\\Temp\\media";
var outputPath = "C:\\Temp\\media\\out";
var filterMedia = ".*\\.(mkv|mp4)$";
var filterSerie = "(?<name>.*)[\\.-_]S(?<season>\\d+)\\s*E(?<episode>\\d+)";

if(!Directory.Exists(filePath))
{
    Console.WriteLine("media path not exist !");
    return;
}

var outDirectory = new DirectoryInfo(outputPath);

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

            //http://www.google.com/search?btnI=I%27m%20Feeling%20Lucky&q=site:imdb.com From
            var nameEncode = HttpUtility.UrlEncode($" {name}");
            using HttpClient client= new HttpClient();
            var result = client.GetAsync($"http://www.google.com/search?btnI=I%27m%20Feeling%20Lucky&q=site:imdb.com{nameEncode}");
            var res = result.Result;

            var query = res.RequestMessage.RequestUri.Query.Remove(0,3);

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



