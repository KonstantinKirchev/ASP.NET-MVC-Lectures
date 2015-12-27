namespace Caching.App.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Caching.App.Infrastructure;
    using Caching.App.Models.ViewModels;
    using Caching.Data.UnitOfWork;

    using PagedList;

    public class UsersController : BaseController
    {
        public UsersController(IApplicationData data)
            : base(data)
        {
        }

        // GET: Users/page
        [OutputCache(Duration = 3600, SqlDependency = "DefaultConnection:AspNetUsers", VaryByParam = "none")]
        public ActionResult Index(int? page)
        {
            var users = this.ApplicationData.Users.All()
                            .OrderBy(u => u.UserName)
                            .ProjectTo<UserViewModel>()
                            .ToPagedList(pageNumber: page ?? 1, pageSize: AppConfig.PageSize);

            return this.View(users);
        }
    }
}