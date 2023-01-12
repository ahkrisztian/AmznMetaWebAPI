using AmznMetaLibrary.HTMLParser.Htmls.CleanHtmls;
using AmznMetaLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmznMetaLibrary.HTMLParser.Htmls
{
    public class ReadHtmlTextParallel
    {
        public static async Task<List<ReviewModel>> getHtml2(string x)
        {
            string responseFromServer = await GetHtml.getHtml(x);

            return CleanData.Middle(responseFromServer);
        }
    }
}
