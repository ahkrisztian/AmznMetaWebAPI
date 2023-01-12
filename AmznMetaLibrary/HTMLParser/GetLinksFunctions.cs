using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmznMetaLibrary.HTMLParser
{
    public class GetLinksFunctions
    {
        public static string linkfoot(string[] vs, string x)
        {
            string temp = "";

            foreach (var v in vs)
            {
                if (v.Contains(x))
                {
                    temp = v;
                }
            }

            return temp;
        }

        public static string link(string x)
        {
            string allreviewlink = "";

            string[] link = x.Split('"');
            foreach (var href in link)
            {
                if (href.Contains("/"))
                {
                    allreviewlink = $"https://www.amazon.de{href}";
                }
            }

            return allreviewlink;
        }

        public static string[] nextpageurl(string[] vs)
        {
            string[] temp = new string[] { };

            foreach (var href in vs)
            {
                if (href.Contains("href"))
                {
                    if (href.Contains("paging_btm"))
                    {
                        temp = href.Split('"');
                    }
                }
            }

            return temp;
        }
    }
}
