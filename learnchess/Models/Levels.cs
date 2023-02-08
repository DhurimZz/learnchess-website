using System.ComponentModel.DataAnnotations;

namespace learnchess.Models
{
    public class Levels
    {
        [Key]
        public string? LevelId { get; set; }
        public string Level { get; set; }
        public ICollection<Article>? Article { get; set; }
        public ICollection<Videos>? Videos { get; set; }

    }
}
