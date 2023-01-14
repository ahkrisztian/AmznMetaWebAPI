using AmznMetaLibrary.HTMLParser;
using AmznMetaLibrary.Repo;

namespace AmznMetaLibrary.Comments;

public class NumberOfComments
{
    private readonly IAmznMetaRepo repo;

    public NumberOfComments(IAmznMetaRepo repo)
    {
        this.repo = repo;
    }
    
    public async Task<List<int>> GetNumberOfComments(string url)
    {
        var asyncHtml = await repo.getHtml(url);

        string[] divs = asyncHtml.Split('<');

        string reviewlinkfoot = GetLinksFunctions.linkfoot(divs, "see-all-reviews-link-foot");


        //Get a reviews page
        //

        string nexturl = GetLinksFunctions.link(reviewlinkfoot);

        var nextUrlAsync = await repo.getHtml(nexturl);

        string[] div = nextUrlAsync.Split('\n');


        //Get the number of comments and reviews
        //

        List<int> temp = new List<int>();

        foreach (string s in div)
        {

            if (s.Contains("Gesamtbewertungen"))
            {
                string[] splittedS = s.Split(' ');

                foreach (string splitted in splittedS)
                {
                    if (splitted != "")
                    {
                        if (int.TryParse(splitted.Replace(".", ""), out int result))
                        {
                            temp.Add(result);
                        }
                    }
                }
            }

        }

        return temp;

    }
}