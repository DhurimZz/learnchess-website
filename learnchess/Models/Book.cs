namespace learnchess.Models
{
    public class Book
    {
        public string? BookId { get; set; }
        public string? Title { get; set; }
        public int? PublicationYear { get; set; }
        public string? AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
