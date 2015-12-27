namespace Events.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Event
    {
        private ICollection<Comment> comments;
         
        public Event()
        {
            this.IsPublic = true;
            this.StartDateTime = DateTime.Now;
            this.comments = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public DateTime StartDateTime { get; set; }

        public TimeSpan? Duration { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public string Description { get; set; }

        [MaxLength(200)]
        public string Location { get; set; }

        public bool IsPublic { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } 
    }
}
