using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmznMetaLibrary.HTMLParser
{
    public class GetHtml
    {
        public async static Task<string> getHtml(string url)
        {
            HttpClient client = new HttpClient();

            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        string result = await content.ReadAsStringAsync();
                        return result;
                    }
                }

                Debug.WriteLine(response.StatusCode.ToString());
                return response.StatusCode.ToString();
            };
        }
    }
}
