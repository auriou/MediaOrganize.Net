namespace MediaOrganize.Models
{
    public class Movie
    {
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; }
        public bool HasValue
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Title);
            }
        }
    }

}
