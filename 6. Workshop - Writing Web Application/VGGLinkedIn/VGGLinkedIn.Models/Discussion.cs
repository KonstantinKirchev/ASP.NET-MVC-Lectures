namespace VGGLinkedIn.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Discussion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Details { get; set; }

        public int GroupId { get; set; }

        public virtual Group Group { get; set; }
    }
}