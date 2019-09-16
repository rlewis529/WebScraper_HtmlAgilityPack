using System;
using System.Linq;

//Misc notes...
//https://www.c-sharpcorner.com/article/web-scraping-in-c-sharp/

namespace WebScraper_HtmlAgilityPack
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            //HtmlAgilityPack.HtmlDocument doc = web.Load("http://www.yellowpages.com/search?search_terms=Software&geo_location_terms=Sydney2C+ND");
            HtmlAgilityPack.HtmlDocument doc = web.Load("https://www.yelp.com/search?find_desc=handyman&find_loc=Morrisville%2C+NC+27560&ns=1");

            //var HeaderNames = doc.DocumentNode.SelectNodes("//a[@class='business-name']").ToList();
            var HeaderNames = doc.DocumentNode.SelectNodes("//a[@class='lemon--a__373c0__IEZFH link__373c0__29943 link-color--blue-dark__373c0__1mhJo link-size--inherit__373c0__2JXk5']").ToList();

            foreach (var item in HeaderNames)
            {
                Console.WriteLine(item.InnerText);
            }
        }
    }
}
