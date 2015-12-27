using System;
using System.Linq;
using System.Web.Mvc;
using Events.Web.Models;
using Microsoft.AspNet.Identity;

namespace Events.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var events = db.Events
                .OrderBy(e => e.StartDateTime)
                .Where(e => e.IsPublic)
                .Select(EventViewModel.ViewModel);

            var upcomingEvents = events.Where(e => e.StartDateTime > DateTime.Now);
            var passedEvents = events.Where(e => e.StartDateTime <= DateTime.Now);

            return View(new UpcomingPassedEventsViewModel()
            {
                UpcomingEvents = upcomingEvents,
                PassedEvents = passedEvents
            });
        }

        public ActionResult EventDetailsById(int id)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var isAdmin = this.isAdmin();
            var eventDetails = db.Events
                .Where(e => e.Id == id)
                .Where(e => e.IsPublic || isAdmin || (e.AuthorId != null && e.AuthorId == currentUserId))
                .Select(EventDetailsViewModel.ViewModel)
                .FirstOrDefault();

            var isOwner = (eventDetails != null && eventDetails.AuthorId != null &&
                           eventDetails.AuthorId == currentUserId);
            ViewBag.CanEdit = isOwner || isAdmin;

            return this.PartialView("_EventDetails", eventDetails);
        }
    }
}