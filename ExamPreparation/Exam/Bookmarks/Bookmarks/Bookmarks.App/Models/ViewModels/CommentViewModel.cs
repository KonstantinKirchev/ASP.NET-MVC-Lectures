﻿namespace Bookmarks.App.Models.ViewModels
{
    using AutoMapper;

    using Bookmarks.Common.Mappings;
    using Bookmarks.Models;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string User { get; set; }

        public string Content { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(x => x.User, cnf => cnf.MapFrom(m => m.User.UserName));
        }
    }
}