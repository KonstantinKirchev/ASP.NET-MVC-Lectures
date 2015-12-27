namespace Exam.App.Controllers
{
    using System.Web.Mvc;
    using Exam.Data.UnitOfWork;
    using Exam.Models;

    public class HomeController : BaseController
    {
        public HomeController(IExamData data) : base(data)
        {
        }

        public HomeController(IExamData data, User user) : base(data, user)
        {
        }

        public ActionResult Index()
        {
            return View();
        }      
    }
}