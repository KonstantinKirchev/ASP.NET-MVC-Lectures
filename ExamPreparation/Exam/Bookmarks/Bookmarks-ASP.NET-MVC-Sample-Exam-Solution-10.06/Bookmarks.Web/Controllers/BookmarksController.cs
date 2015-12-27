namespace Bookmarks.Web.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common;
    using Data;
    using PagedList;
    using ViewModels;
    using Bookmarks.Web.InputModels;
    using Bookmarks.Models;
    using System.Web.Mvc.Expressions;
    using Microsoft.AspNet.Identity;

    [Authorize]
    public class BookmarksController : BaseController
    {
        public BookmarksController(IBookmarksData data)
            : base(data)
        {
        }

        [AllowAnonymous]
        public ActionResult Index(int? page)
        {
            var bookmarks = this.Data.Bookmarks
                .All()
                .OrderBy(x => x.Title)
                .Project()
                .To<BookmarkViewModel>()
                .ToPagedList(page ?? GlobalConstants.DefaultStartPage, GlobalConstants.DefaultPageSize);
            return this.View(bookmarks);
        }

        public ActionResult Details(int id)
        {
            var bookmark = this.Data.Bookmarks
                .All()
                .Include(x => x.Votes)
                .FirstOrDefault(x => x.Id == id);
            var bookmarkViewModel = Mapper.Map<BookmarkDetailsViewModel>(bookmark);

            var userId = this.User.Identity.GetUserId();
            bookmarkViewModel.UserHasVoted = bookmark.Votes.Any(x => x.UserId == userId);
            return this.View(bookmarkViewModel);
        }

        public ActionResult Create()
        {
            this.LoadCategories();
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookmarkInputModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var bookmark = Mapper.Map<Bookmark>(model);
                this.Data.Bookmarks.Add(bookmark);
                this.Data.SaveChanges();

                return this.RedirectToAction(x => x.Details(bookmark.Id));
            }

            this.LoadCategories();
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(CommentInputModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                model.UserId = this.User.Identity.GetUserId();
                var comment = Mapper.Map<Comment>(model);
                this.Data.Comments.Add(comment);
                this.Data.SaveChanges();

                var commentDb = this.Data.Comments
                    .All()
                    .Where(x => x.Id == comment.Id)
                    .Project()
                    .To<CommentViewModel>()
                    .FirstOrDefault();

                return this.PartialView("DisplayTemplates/CommentViewModel", commentDb);
            }

            return this.Json("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComment(int commentId)
        {
            var comment = this.Data.Comments
                .All()
                .FirstOrDefault(x => x.Id == commentId);
            if (comment != null && comment.UserId == this.User.Identity.GetUserId())
            {
                this.Data.Comments.Delete(comment);
                this.Data.SaveChanges();

                return this.Content(string.Empty);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Can not delete the comment");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Vote(int bookmarkId)
        {
            var bookmark = this.Data.Bookmarks
                .All()
                .FirstOrDefault(x => x.Id == bookmarkId);
            if (bookmark != null)
            {
                var userHasVoted = bookmark.Votes.Any(x => x.UserId == this.User.Identity.GetUserId());
                if (!userHasVoted)
                {
                    this.Data.Votes.Add(new Vote
                    {
                        BookmarkId = bookmark.Id,
                        UserId = this.User.Identity.GetUserId(),
                        Value = 1
                    });
                    this.Data.SaveChanges();
                }
                
                var votesCount = bookmark.Votes.Sum(x => x.Value);
                return this.Content(votesCount.ToString());
            }

            return new EmptyResult();
        }

        private void LoadCategories()
        {
            this.ViewBag.Categories = this.Data.Categories
                .All()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                });
        }
    }
}