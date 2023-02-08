namespace learnchess.Models
{
    public class Videos
    {
        public string VideosId { get; set; }
        public byte[]? Video { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string? AuthorId { get; set; }
        public Author? Author { get; set; }
        public string? LanguageId { get; set; }
        public Languages? Language { get; set; }
        public string? LevelId { get; set; }
        public Levels? Level { get; set; }
    }
}
