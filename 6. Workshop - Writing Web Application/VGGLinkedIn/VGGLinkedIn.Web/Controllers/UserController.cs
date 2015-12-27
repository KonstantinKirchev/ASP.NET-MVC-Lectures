namespace VGGLinkedIn.Web.Controllers
{
    using System.Data.Entity;
    using System.Linq;

    using VGGLinkedIn.Data;
    using VGGLinkedIn.Models;
    using System.Web.Mvc;

    using VGGLinkedIn.Web.ViewModels;

    [Authorize]
    public class UserController : BaseController
    {
        public UserController(IVGGLinkedInData data)
            : base(data)
        {
        }

        // GET: Users
        public ActionResult Index(string username)
        {
            //var user = this.Data.Users.All().FirstOrDefault(x => x.UserName == username);

            var user =
                this.Data.Users
                    .All()
                    .Include(x => x.Certifications)
                    .Include(x => x.Skills)
                    .Include("Skills.Skill")
                    .Include("Skills.Skill.User")
                    .Where(x => x.UserName == username)
                    .Select(UserViewModel.ViewModel)
                    .FirstOrDefault();

            if (user == null)
            {
                return this.HttpNotFound("User does not exist!");
            }

            //var userViewModel = UserViewModel.FromModel(user);
                      
            return this.View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EndorseUserForSkill(int id)
        {
            return null;
        }
    }
}