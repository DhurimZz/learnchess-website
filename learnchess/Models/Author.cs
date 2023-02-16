namespace learnchess.Models
{
    public class Author
    {
        public string? AuthorId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public  ICollection<Article>? Article { get; set; }
        public ICollection<Videos>? Videos { get; set; }

    }
}
