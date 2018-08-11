using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace VisaParser
{
    class Parser
    {
        public static bool HasMskPlaces(string url, bool withInterview, out string parcedValue)
        {
            byte[] response;
            using (HttpClient http = new HttpClient())
            {
                response = http.GetByteArrayAsync(url).Result;
            }
            string source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);

            //string source = System.IO.File.ReadAllText(@"C:\Users\anton\source\repos\VisaParser\VisaParser\test.html");

            source = WebUtility.HtmlDecode(source);
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(source);

            string aHrefContains;
            if (withInterview)
                aHrefContains = "moscow-wait-time-large";
            else
                aHrefContains = "msk-dropbox-wait-time-large";

            HtmlNode badgeParentNode = document.DocumentNode
                .Descendants("a").Where(d =>
                    d.Attributes.Contains("data-uk-lightbox")
                    &&
                    d.Attributes.Contains("href")
                    &&
                    d.Attributes["href"].Value.Contains(aHrefContains)
                ).First().ParentNode;

            HtmlNode badgeNode = badgeParentNode.Descendants("span").First();

            string innetText = badgeNode.InnerText.ToLower();

            parcedValue = innetText;

            bool hasPlaces = !innetText.Contains("нет") || innetText.Contains("есть");

            return hasPlaces;
        }
    }
}
