using AmznMetaLibrary.HTMLParser.Htmls.CleanHtmls;
using AmznMetaLibrary.Models;
using AmznMetaLibrary.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmznMetaLibrary.HTMLParser.Htmls
{
    public class ReadHtmlTextParallel : IReadHtmlTextParallel
    {
        private readonly IAmznMetaRepo repo;

        public ReadHtmlTextParallel(IAmznMetaRepo repo)
        {
            this.repo = repo;
        }

        public async Task<List<ReviewModel>> getHtml2(string url)
        {
            string responseFromServer = await repo.getHtml(url);

            return CleanData.Middle(responseFromServer);
        }
    }
}
