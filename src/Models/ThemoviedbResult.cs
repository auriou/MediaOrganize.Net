namespace MediaOrganize.Models
{
    public class ThemoviedbResult<T> where T : class,new()
    {
        public T[] results { get; set; }
    }


}
