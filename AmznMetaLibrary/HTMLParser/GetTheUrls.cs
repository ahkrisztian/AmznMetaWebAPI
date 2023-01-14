using AmznMetaLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmznMetaLibrary.HTMLParser
{
    public class GetTheUrls
    {
        public static List<PageLinkModel> urls(decimal comments, PageLinkModel pageModel)
        {

            decimal Comments = Math.Ceiling(comments / 10);

            //var pageModel = GetLinkParts.NewPageLinkModel(url);

            List<PageLinkModel> models = new List<PageLinkModel>(Convert.ToInt32(Comments));

            for (int i = 1; i <= Comments; i++)
            {
                models.Add(new PageLinkModel
                {
                    LinkFirst = pageModel.LinkFirst,
                    LinkSecond = pageModel.LinkSecond,
                    LinkThird = pageModel.LinkThird,
                    PageNumber = i,
                    LinkForth = $"https://www.amazon.de/{pageModel.LinkFirst}/{pageModel.LinkSecond}/{pageModel.LinkThird}" +
                                $"/ref=cm_cr_getr_d_paging_btm_next_{i}?ie=UTF8&reviewerType=all_reviews&pageNumber={i}"

                });

            }

            return models;
        }
    }
}
