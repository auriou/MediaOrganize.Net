
using MediaOrganize.Service;

var filePath = "C:\\Temp\\media";
var outputPath = "C:\\Temp\\media\\out\\";
var token = Environment.GetEnvironmentVariable("token");

var dbSearch = new ThemoviedbSearch("fr-FR", token);

var directoryScan = new DirectoryScan(dbSearch, filePath, $"{outputPath}movies", $"{outputPath}series");

await directoryScan.ScanAsync();