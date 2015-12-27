namespace Bookmarks.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Vote
    {
        [Key]
        public int Id { get; set; }

        public int Value { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int BookmarkId { get; set; }

        public Bookmark Bookmark { get; set; }
    }
}
