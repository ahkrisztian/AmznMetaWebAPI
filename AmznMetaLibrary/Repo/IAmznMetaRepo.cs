using AmznMetaLibrary.Comments;
using AmznMetaLibrary.Models;
using AmznMetaLibrary.Models.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmznMetaLibrary.Repo;

public interface IAmznMetaRepo
{
    Task<List<int>> NumberOfCommentsandReviews(string url);

    Task<PageLinkModel> PageLinkModel(string url, int numberofcomments);

    Task<List<PageLinkModel>> PageLinkModels(decimal comments, PageLinkModel pageModel);

    Task<List<ReviewModel>> ReviewModels(List<PageLinkModel> htmls);

    Task<List<CommentModel>> commentModels(List<ReviewModel> commentandtitle);

    Task<List<ReviewModel>> WhenAllReady(string url);

    Task<string> getHtml(string url);
}
