using System.ComponentModel.DataAnnotations;

namespace learnchess.Models
{
    public class Languages
    {
        [Key]
        public string? LanguageId { get; set; }

        public string Language { get; set; }
        public ICollection<Article>? Article { get; set; }

    }
}
