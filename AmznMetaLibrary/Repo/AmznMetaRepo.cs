using AmznMetaLibrary.Comments;
using AmznMetaLibrary.CreateLinks;
using AmznMetaLibrary.HTMLParser;
using AmznMetaLibrary.HTMLParser.Htmls;
using AmznMetaLibrary.HTMLParser.Htmls.CleanHtmls;
using AmznMetaLibrary.Models;
using AmznMetaLibrary.Models.Comment;
using AmznMetaLibrary.Policies;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AmznMetaLibrary.Repo
{
    public class AmznMetaRepo : IAmznMetaRepo
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ClientPolicies policies;

        public AmznMetaRepo(IHttpClientFactory httpClientFactory, ClientPolicies policies)
        {
            this.httpClientFactory = httpClientFactory;
            this.policies = policies;
        }

        public async Task<List<CommentModel>> commentModels(List<ReviewModel> commentandtitle)
        {
            Log.Information("commentModesl was callad");
            return CreateLinkList.AddLinkModel(commentandtitle);
        }

        public async Task<List<int>> NumberOfCommentsandReviews(string url)
        {
            var asyncHtml = await getHtml(url);

            string[] divs = asyncHtml.Split('<');

            string reviewlinkfoot = GetLinksFunctions.linkfoot(divs, "see-all-reviews-link-foot");


            //Get a reviews page
            //

            string nexturl = GetLinksFunctions.link(reviewlinkfoot);

            var nextUrlAsync = await getHtml(nexturl);

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

        public async Task<PageLinkModel> PageLinkModel(string url, int numberofcomments)
        {
            List<string> darabolt2 = new List<string>();

            string asyncDivs = await getHtml(url);

            string[] divs = asyncDivs.Split('<');

            string reviewlinkfoot = GetLinksFunctions.linkfoot(divs, "see-all-reviews-link-foot");

            //Get a reviews page*
            string nexturl = GetLinksFunctions.link(reviewlinkfoot);

            if (numberofcomments <= 10)
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

                string divAsync = await getHtml(nexturl);

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

        public async Task<List<PageLinkModel>> PageLinkModels(decimal comments, PageLinkModel pageModel)
        {
            return GetTheUrls.urls(comments, pageModel);
        }

        public async Task<List<ReviewModel>> ReviewModels(List<PageLinkModel> htmls)
        {
            //Returns ReviewModels

            List<ReviewModel> websites = new List<ReviewModel>();

            List<Task<List<ReviewModel>>> tasks = new List<Task<List<ReviewModel>>>();

            foreach (var html in htmls)
            {
                tasks.Add(Task.Run(async () => await CleanDataHtml(html.LinkForth)));
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

        private async Task<string> getHtml(string url)
        {
            var response = await policies.ImmediateHttpRetry.ExecuteAsync(
                () => httpClientFactory.CreateClient().GetAsync(url));

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();

            }

            Debug.WriteLine(response.StatusCode.ToString());
            return response.StatusCode.ToString();
        }

        private async Task<List<ReviewModel>> CleanDataHtml(string url)
        {
            string responseFromServer = await getHtml(url);

            return CleanData.Middle(responseFromServer);
        }

        public async Task<List<ReviewModel>> WhenAllReady(string url)
        {
            var comrew = await NumberOfCommentsandReviews(url);

            int numberofcommentreview = comrew[1];

            PageLinkModel pagelinkmodel = await PageLinkModel(url, numberofcommentreview);

            List<PageLinkModel> pagelinkmodels = await PageLinkModels(numberofcommentreview, pagelinkmodel);

            return await ReviewModels(pagelinkmodels);
        }
    }
}
