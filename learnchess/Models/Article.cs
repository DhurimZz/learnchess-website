namespace learnchess.Models
{
    public class Article
    {
        public int ArticleId { get; set; }
        public byte[]? Photo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string url { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
