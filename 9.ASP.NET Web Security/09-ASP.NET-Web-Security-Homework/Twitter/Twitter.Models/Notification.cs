namespace Twitter.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Twitter.Models.Enumerations;

    public class Notification
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string Content { get; set; }

        public NotificationType NotificationType { get; set; }

        public string ReceiverId { get; set; }

        public virtual User Receiver { get; set; }
    }
}