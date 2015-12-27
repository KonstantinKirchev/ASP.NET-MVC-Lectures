namespace Twitter.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;

    using PagedList;

    using Twitter.App.Hubs;
    using Twitter.App.Models.BindingModels;
    using Twitter.App.Models.ViewModels;
    using Twitter.App.Utilities;
    using Twitter.Data.Contracts;
    using Twitter.Models;
    using Twitter.Models.Enumerations;

    [Authorize]
    public class UsersController : BaseController
    {
        private ApplicationSignInManager signInManager;

        private ApplicationUserManager userManager;

        public UsersController()
        {
        }

        public UsersController(
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager,
            ITwitterData data)
            : base(data)

        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return this.signInManager ?? this.HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }

            private set
            {
                this.signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this.userManager ?? this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

            private set
            {
                this.userManager = value;
            }
        }

        // GET: {username}/Users/Following
        public ActionResult Following(string username, int? page)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var currentUser =
                this.Data.Users.GetAll()
                    .Where(u => u.Id == currentUserId)
                    .Select(UserViewModel.Create(currentUserId))
                    .FirstOrDefault();

            int pageSize = DefaultValues.DefaultPageSize;
            int pageNumber = page ?? DefaultValues.DefaultStartPage;

            IPagedList<UserViewModel> following =
                this.Data.Users.GetAll()
                    .Where(u => u.Following.Any(fu => fu.UserName == username))
                    .OrderBy(u => u.UserName)
                    .Select(UserViewModel.Create(currentUserId))
                    .ToPagedList(pageNumber, pageSize);

            if (following == null)
            {
                return this.RedirectToAction("Following");
            }

            var model = new UserAndFollowersViewModel() { User = currentUser, Followers = following };

            return this.View(model);
        }

        // GET: {username}/Users/Followers
        public ActionResult Followers(string username, int? page)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var currentUser =
                this.Data.Users.GetAll()
                    .Where(u => u.Id == currentUserId)
                    .Select(UserViewModel.Create(currentUserId))
                    .FirstOrDefault();

            if (currentUser == null)
            {
                return this.RedirectToAction("Followers");
            }

            int pageSize = DefaultValues.DefaultPageSize;
            int pageNumber = page ?? DefaultValues.DefaultStartPage;

            IPagedList<UserViewModel> following =
                this.Data.Users.GetAll()
                    .Where(u => u.Followers.Any(fu => fu.UserName == username))
                    .OrderBy(u => u.UserName)
                    .Select(UserViewModel.Create(currentUserId))
                    .ToPagedList(pageNumber, pageSize);

            if (following == null)
            {
                return this.RedirectToAction("Followers");
            }

            var model = new UserAndFollowersViewModel() { User = currentUser, Followers = following };

            return this.View(model);
        }

        // POST: {username}/Users/Search
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string search)
        {
            var keyword = search.ToLower();

            var foundUsers =
                this.Data.Users.GetAll()
                    .Where(u => u.UserName.ToLower().Contains(keyword))
                    .Select(UserSearchViewModel.Create());

            return this.View(foundUsers);
        }

        // POST: {username}/Users/Follow
        public ActionResult Follow(string followUserId)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var currentUser = this.Data.Users.GetAll().FirstOrDefault(u => u.Id == currentUserId);
            var followUser = this.Data.Users.GetAll().FirstOrDefault(u => u.Id == followUserId);

            if (currentUser == null || followUser == null)
            {
                return this.RedirectToAction("Following");
            }

            followUser.Following.Add(currentUser);

            var notification = new Notification()
                {
                    Content = string.Format("You have been followed by {0}", currentUser.UserName),
                    Date = DateTime.Now,
                    ReceiverId = followUserId,
                    NotificationType = NotificationType.NewFollower
                };

            this.Data.Notifications.Add(notification);
            this.Data.SaveChanges();

            TwitterHub hub = new TwitterHub();

            hub.Notification(followUserId);

            return this.RedirectToAction("Following");
        }

        // GET: {username}/Users/Profile
        [OutputCache(Duration = 10, VaryByParam = "none")]
        public async Task<ActionResult> Profile(ManageMessageId? message)
        {
            this.ViewBag.StatusMessage = message == ManageMessageId.ChangePasswordSuccess
                                             ? "Your password has been changed."
                                             : message == ManageMessageId.SetPasswordSuccess
                                                   ? "Your password has been set."
                                                   : message == ManageMessageId.EditProfileSucess
                                                         ? "Your profile has been changed."
                                                         : message == ManageMessageId.Error
                                                               ? "Incorrect data. Please try again."
                                                               : "";

            var userId = this.User.Identity.GetUserId();
            var model = new ProfileViewModel
                {
                    HasPassword = this.HasPassword(),
                    TwoFactor = await this.UserManager.GetTwoFactorEnabledAsync(userId),
                    Logins = await this.UserManager.GetLoginsAsync(userId),
                    BrowserRemembered = await this.AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
                };
            return this.View(model);
        }

        // GET: {username}/Users/EditProfile
        [OutputCache(Duration = 10, VaryByParam = "none")]
        public async Task<ActionResult> EditProfile(ManageMessageId? message)
        {
            this.ViewBag.StatusMessage = message == ManageMessageId.Error ? "Incorrect data. Please try again." : "";

            var userId = this.User.Identity.GetUserId();

            var model = new EditProfileViewModel()
                {
                    Username = this.User.Identity.GetUserName(),
                    Email = await this.UserManager.GetEmailAsync(userId),
                };

            return this.View(model);
        }

        // POST: {username}/Users/EditProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(EditProfileBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("EditProfile", new { Message = ManageMessageId.Error });
            }

            var userId = this.User.Identity.GetUserId();
            var user = this.Data.Users.Find(userId);

            if (user == null)
            {
                return this.RedirectToAction("EditProfile", new { Message = ManageMessageId.Error });
            }

            string profileImage = user.ProfileImage;
            if (model.ProfileImage != null)
            {
                profileImage = Helpers.ConvertImage(model.ProfileImage);
            }

            string coverImage = user.CoverImage;
            if (model.CoverImage != null)
            {
                coverImage = Helpers.ConvertImage(model.CoverImage);
            }

            user.UserName = model.Username;
            user.Email = model.Email;
            user.ProfileImage = profileImage;
            user.CoverImage = coverImage;

            this.Data.SaveChanges();

            this.HttpContext.GetOwinContext().Authentication.SignOut();

            return this.RedirectToAction("Login", "Account");
        }

        // GET: {username}/Users/ChangePassword
        [OutputCache(Duration = 10, VaryByParam = "none")]
        public ActionResult ChangePassword()
        {
            return this.View();
        }

        // POST: {username}/Users/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var result =
                await
                this.UserManager.ChangePasswordAsync(
                    this.User.Identity.GetUserId(),
                    model.OldPassword,
                    model.NewPassword);
            if (result.Succeeded)
            {
                var user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());
                if (user != null)
                {
                    await this.SignInManager.SignInAsync(user, false, false);
                }

                return this.RedirectToAction("Profile", new { Message = ManageMessageId.ChangePasswordSuccess });
            }

            this.AddErrors(result);
            return this.View(model);
        }

        // GET: {username}/Users/SetPassword
        public ActionResult SetPassword()
        {
            return this.View();
        }

        // POST: {username}/Users/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this.UserManager.AddPasswordAsync(this.User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());
                    if (user != null)
                    {
                        await this.SignInManager.SignInAsync(user, false, false);
                    }
                    return this.RedirectToAction("Profile", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                this.AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.userManager != null)
            {
                this.userManager.Dispose();
                this.userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return this.HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = this.UserManager.FindById(this.User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            EditProfileSucess,

            ChangePasswordSuccess,

            SetPasswordSuccess,

            Error
        }

        #endregion
    }
}