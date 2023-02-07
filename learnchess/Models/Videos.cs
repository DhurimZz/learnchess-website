namespace learnchess.Models
{
    public class Videos
    {
        public int VideosId { get; set; }
        public byte[]? Video { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }
}
