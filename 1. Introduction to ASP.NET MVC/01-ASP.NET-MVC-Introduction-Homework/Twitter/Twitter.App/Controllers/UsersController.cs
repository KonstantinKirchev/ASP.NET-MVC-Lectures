namespace Twitter.App.Controllers
{
    using Twitter.Data.Contracts;

    public class UsersController : BaseController
    {
        public UsersController()
        {
        }

        public UsersController(ITwitterData data)
            : base(data)
        {
        }
    }
}