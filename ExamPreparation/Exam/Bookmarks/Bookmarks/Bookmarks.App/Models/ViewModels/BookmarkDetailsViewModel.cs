﻿namespace Bookmarks.App.Models.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using AutoMapper;

    using Common.Mappings;
    using Bookmarks.Models;

    using Microsoft.AspNet.Identity;

    public class BookmarkDetailsViewModel : IMapFrom<Bookmark>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public int Votes { get; set; }

        public bool UserHasVoted { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Bookmark, BookmarkDetailsViewModel>()
                .ForMember(x => x.Votes, cnf => cnf.MapFrom(m => m.Votes.Any() ? m.Votes.Sum(v => v.Value) : 0));
        }
    }
}