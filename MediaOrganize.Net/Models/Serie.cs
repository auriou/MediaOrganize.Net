namespace MediaOrganize.Models
{
    public class Serie
    {
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; }
        public int Season { get; set; }
        public bool HasValue 
        { 
            get
            {
                return !string.IsNullOrWhiteSpace(Title);
            } 
        }
    }
}
