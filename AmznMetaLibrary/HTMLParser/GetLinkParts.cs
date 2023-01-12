using AmznMetaLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmznMetaLibrary.HTMLParser
{
    public class GetLinkParts
    {
        public static async Task<PageLinkModel> NewPageLinkModel(string url, int numberOfComments)
        {
            List<string> darabolt2 = new List<string>();

            string asyncDivs = await GetHtml.getHtml(url);

            string[] divs = asyncDivs.Split('<');

            string reviewlinkfoot = GetLinksFunctions.linkfoot(divs, "see-all-reviews-link-foot");

            //Get a reviews page*
            string nexturl = GetLinksFunctions.link(reviewlinkfoot);

            if (numberOfComments <= 10)
            {
                //Split next page link
                string[] onePageReview = nexturl.Split('/');

                //Next page link pieces

                foreach (var v in onePageReview)
                {
                    if (!String.IsNullOrEmpty(v))
                    {
                        darabolt2.Add(v);
                    }
                }

                return new PageLinkModel { LinkFirst = darabolt2[2], LinkSecond = darabolt2[3], LinkThird = darabolt2[4], PageNumber = 1 };

            }
            else
            {
                //Next page buttom link

                string divAsync = await GetHtml.getHtml(nexturl);

                string[] div = divAsync.Split(' ');

                string[] nexturlarray = GetLinksFunctions.nextpageurl(div);

                //Split next page link
                string[] darabolt = nexturlarray[1].Split('/');

                //Next page link pieces

                foreach (var v in darabolt)
                {
                    if (!String.IsNullOrEmpty(v))
                    {
                        darabolt2.Add(v);
                    }
                }

                darabolt2.ToArray();

                return new PageLinkModel { LinkFirst = darabolt2[0], LinkSecond = darabolt2[1], LinkThird = darabolt2[2] };
            }

        }
    }
}
