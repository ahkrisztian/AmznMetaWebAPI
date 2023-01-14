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
        private readonly IReadHtmlTextParallel read;

        public Htmls(IReadHtmlTextParallel read)
        {
            this.read = read;
        }
        public async Task<List<ReviewModel>> asynchtml(List<PageLinkModel> htmls)
        {
            //Returns ReviewModels

            List<ReviewModel> websites = new List<ReviewModel>();

            List<Task<List<ReviewModel>>> tasks = new List<Task<List<ReviewModel>>>();

            foreach (var html in htmls)
            {
                tasks.Add(Task.Run(async () => await read.getHtml2(html.LinkForth)));
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
