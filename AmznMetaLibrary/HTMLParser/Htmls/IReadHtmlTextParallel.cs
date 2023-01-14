using AmznMetaLibrary.Models;

namespace AmznMetaLibrary.HTMLParser.Htmls
{
    public interface IReadHtmlTextParallel
    {
        Task<List<ReviewModel>> getHtml2(string url);
    }
}