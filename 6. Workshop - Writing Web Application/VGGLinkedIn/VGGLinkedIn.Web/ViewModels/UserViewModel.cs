namespace VGGLinkedIn.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;

    using VGGLinkedIn.Models;

    public class UserViewModel
    {
        public static Expression<Func<User, UserViewModel>> ViewModel
        {
            get
            {
                return x => new UserViewModel
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    AvatarUrl = x.AvatarUrl,
                    Email = x.Email,
                    Summary = x.Summary,
                    ContactInfo = x.ContactInfo,
                    FullName = x.FullName,
                    Certifications = x.Certifications.AsQueryable().Select(CertificationViewModel.ViewModel),
                    Skills = x.Skills.AsQueryable().Select(SkillViewModel.ViewModel)
                };
            }
        }

        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        [Display(Name = "Име")]
        public string FullName { get; set; }

        public string AvatarUrl { get; set; }

        public string Summary { get; set; }

        public ContactInfo ContactInfo { get; set; }

        public IEnumerable<CertificationViewModel> Certifications { get; set; }

        public IEnumerable<SkillViewModel> Skills { get; set; }

        //public static object FromModel(User user)
        //{
        //    return new UserViewModel()
        //    {
        //        Id = user.Id,
        //        UserName = user.UserName,
        //        Email = user.Email,
        //        ContactInfo = user.ContactInfo,
        //        AvatarUrl = user.AvatarUrl,
        //        FullName = user.FullName,
        //        Summary = user.Summary
        //    };
        //}
    }
}