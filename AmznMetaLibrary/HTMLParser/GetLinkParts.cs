using AmznMetaLibrary.Models;
using AmznMetaLibrary.Repo;

namespace AmznMetaLibrary.HTMLParser;

public class GetLinkParts
{
    private readonly IAmznMetaRepo repo;

    public GetLinkParts(IAmznMetaRepo repo)
    {
        this.repo = repo;
    }

    public async Task<PageLinkModel> NewPageLinkModel(string url, int numberOfComments)
    {
        List<string> darabolt2 = new List<string>();

        string asyncDivs = await repo.getHtml(url);

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

            string divAsync = await repo.getHtml(nexturl);

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
