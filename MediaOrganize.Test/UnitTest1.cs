
using MediaOrganize.Service;

namespace MediaOrganize.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            

            var filePath = "C:\\Temp\\media";
            var outputPath = "C:\\Temp\\media\\out\\";
            var token = Environment.GetEnvironmentVariable("token");

            var dbSearch = new ThemoviedbSearch("fr-FR", token);

            var directoryScan = new DirectoryScan(dbSearch, filePath, $"{outputPath}movies", $"{outputPath}series");

            await directoryScan.ScanAsync();
        }
    }
}