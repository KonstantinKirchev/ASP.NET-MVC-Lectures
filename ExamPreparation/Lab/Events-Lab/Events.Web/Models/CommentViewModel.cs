using System;
using System.Linq;
using System.Linq.Expressions;
using Events.Model;

namespace Events.Web.Models
{
    public class CommentViewModel
    {
        public string Text { get; set; }

        public string Author { get; set; }

        public static Expression<Func<Comment, CommentViewModel>> ViewModel
        {
            get
            {
                return e => new CommentViewModel
                {
                    Text = e.Text,
                    Author = e.Author.FullName
                };
            }
        }
    }
}