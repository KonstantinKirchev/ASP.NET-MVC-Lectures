namespace VGGLinkedIn.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Endorcement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int UserSkillId { get; set; }

        public virtual UserSkill UserSkill { get; set; }
    }
}
