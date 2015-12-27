namespace Twitter.App.Controllers
{
    using Twitter.Data.Contracts;

    public class TweetsController : BaseController
    {
        public TweetsController()
        {
        }

        public TweetsController(ITwitterData data)
            : base(data)
        {
        }
    }
}