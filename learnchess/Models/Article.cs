namespace learnchess.Models
{
    public class Article
    {
        public string? ArticleId { get; set; }
        public byte[]? Photo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string url { get; set; }
        public string? AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
