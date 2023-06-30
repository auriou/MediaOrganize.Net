namespace MediaOrganize.Models
{
    public class Config
    {
        public string ScanPath { get; set; } = string.Empty;
        public string MoviePath { get; set; } = string.Empty;
        public string SeriePath { get; set; } = string.Empty;
        public ConfigTmdb Tmdb { get; set; } = new ConfigTmdb();
    }

}
