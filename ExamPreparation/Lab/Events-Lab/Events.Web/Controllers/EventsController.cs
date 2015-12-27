using System.Web.Mvc;
using Events.Model;
using Events.Web.Models;

namespace Events.Web.Controllers
{
    public class EventsController : BaseController
    {
        // GET: Events
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventInputModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                
            }
            return View();
        }
    }
}