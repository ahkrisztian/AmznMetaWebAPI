using AmznMetaLibrary.Models.Comment;
using AmznMetaLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmznMetaLibrary.CreateLinks
{
    public class CreateLinkList
    {
        public static List<CommentModel> AddLinkModel(List<ReviewModel> commentandtitle)
        {
            List<CommentModel> models = new List<CommentModel>();

            for (int i = 0; i < commentandtitle.Count; i++)
            {
                string title = commentandtitle[i].title;
                string comment = commentandtitle[i].comment;
                string stars = commentandtitle[i].stars;
                string date = commentandtitle[i].date;
                string link = commentandtitle[i].linkToCostumer;
                string formed =
                    $"{i + 1}." +
                    $"Stars: {stars} - {date}{Environment.NewLine}" +
                    $"Title: {title}{Environment.NewLine}" +
                    $"Comment:{comment}{Environment.NewLine}" +
                    $"https://www.amazon.de{link}{Environment.NewLine}" +
                    $"------------------------------------------";

                models.Add(new CommentModel(title, comment, formed));

            }

            return models;
        }
    }
}
