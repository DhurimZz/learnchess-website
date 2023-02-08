namespace learnchess.Models
{
	public class Games
	{
		public int GamesId { get; set; }
		public byte[]? Thumbnail { get; set; }
		public string Title { get; set; }
		public string Url { get; set; }
	}
}
