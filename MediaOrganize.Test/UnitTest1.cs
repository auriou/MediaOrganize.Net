
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
            token = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJhNDk1NGI4MzMxNDZhNzFlYWRmYWQ2NzU2NDcwYzJhMiIsInN1YiI6IjY0OGQzMjhhYzNjODkxMDE0ZWJkMzI1OCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.YRERc6LzV34_SZwDSYayuvfQNrTcChjf6tdTEufvLFU";

            var dbSearch = new ThemoviedbSearch("fr-FR", token);

            var directoryScan = new DirectoryScan(dbSearch, filePath, $"{outputPath}movies", $"{outputPath}series");

            await directoryScan.ScanAsync();
        }
    }
}