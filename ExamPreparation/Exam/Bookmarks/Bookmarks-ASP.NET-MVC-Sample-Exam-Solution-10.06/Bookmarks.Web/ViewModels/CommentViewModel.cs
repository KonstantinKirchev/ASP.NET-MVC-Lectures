namespace Bookmarks.Web.ViewModels
{
    using Bookmarks.Models;
    using Common.Mappings;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string User { get; set; }

        public string Content { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(x => x.User, cnf => cnf.MapFrom(m => m.User.UserName));
        }
    }
}
