using AmznMetaLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmznMetaLibrary.HTMLParser.Htmls
{
    public class Htmls
    {
        public static async Task<List<ReviewModel>> asynchtml(List<PageLinkModel> htmls, IProgress<string> progress)
        {
            //Returns ReviewModels

            List<ReviewModel> websites = new List<ReviewModel>();

            List<Task<List<ReviewModel>>> tasks = new List<Task<List<ReviewModel>>>();

            foreach (var html in htmls)
            {
                tasks.Add(Task.Run(async () => await ReadHtmlTextParallel.getHtml2(html.LinkForth)));
                progress.Report($"Collecting Data");
            }

            var result = await Task.WhenAll(tasks);

            foreach (var item in result)
            {
                foreach (var v in item)
                {
                    websites.Add(v);
                }
            }

            return websites;
        }
    }
}
