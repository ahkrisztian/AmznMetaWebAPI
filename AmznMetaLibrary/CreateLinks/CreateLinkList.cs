using AmznMetaLibrary.Models.Comment;
using AmznMetaLibrary.Models;
using BenchmarkDotNet.Attributes;
using System.Text;

namespace AmznMetaLibrary.CreateLinks
{
    [MemoryDiagnoser]
    public class CreateLinkList
    {
        [Benchmark]
        public static List<CommentModel> AddLinkModel(List<ReviewModel> commentandtitle)
        {
            List<CommentModel> models = new List<CommentModel>();

            for (int i = 0; i < commentandtitle.Count; i++)
            {
                StringBuilder output = new StringBuilder();

                string formed =
                    $"{i + 1}." +
                    $"Stars: {commentandtitle[i].stars} - {commentandtitle[i].date}{Environment.NewLine}" +
                    $"Title: {commentandtitle[i].title}{Environment.NewLine}" +
                    $"Comment:{commentandtitle[i].comment}{Environment.NewLine}" +
                    $"https://www.amazon.de{commentandtitle[i].linkToCostumer}{Environment.NewLine}" +
                    $"------------------------------------------";

                models.Add(new CommentModel(commentandtitle[i].title, commentandtitle[i].comment, formed));

            }

            return models;
        }
    }
}
