namespace learnchess.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public ICollection<Article> Article { get; set; }

    }
}
