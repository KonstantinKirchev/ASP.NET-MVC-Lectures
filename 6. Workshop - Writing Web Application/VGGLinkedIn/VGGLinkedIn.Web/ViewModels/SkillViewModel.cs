namespace VGGLinkedIn.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using VGGLinkedIn.Models;

    public class SkillViewModel
    {
        public static Expression<Func<UserSkill, SkillViewModel>> ViewModel
        {
            get
            {
                return x => new SkillViewModel()
                {
                    Id = x.Id,
                    Name = x.Skill.Name,
                    EndorsementsCount = x.Endorcements.Count,
                    Endorsers = x.Endorcements.Select(e => e.User.UserName)
                };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int EndorsementsCount { get; set; }

        public IEnumerable<string> Endorsers { get; set; }
    }
}