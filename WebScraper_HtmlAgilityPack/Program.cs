using System;
using System.Collections.Generic;
using System.Linq;

//Misc notes...
//https://www.c-sharpcorner.com/article/web-scraping-in-c-sharp/

namespace WebScraper_HtmlAgilityPack
{
    class Program
    {
        static void Main(string[] args)
        {
            var teamList = new List<string>();
                        
            teamList.Add("alabama-crimson-tide");
            teamList.Add("louisiana-state-tigers");

            //string scrapeLink = "https://www.teamrankings.com/college-football/team/alabama-crimson-tide/stats";
            string scrapeLink = "https://www.teamrankings.com/college-football/team/" + teamList[0] + "/stats";
            

            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            //HtmlAgilityPack.HtmlDocument doc = web.Load("http://www.yellowpages.com/search?search_terms=Software&geo_location_terms=Sydney2C+ND");
            //HtmlAgilityPack.HtmlDocument doc = web.Load("https://www.yelp.com/search?find_desc=handyman&find_loc=Morrisville%2C+NC+27560&ns=1");
            HtmlAgilityPack.HtmlDocument doc = web.Load(scrapeLink);

            //var HeaderNames = doc.DocumentNode.SelectNodes("//a[@class='business-name']").ToList();
            //var HeaderNames = doc.DocumentNode.SelectNodes("//a[@class='lemon--a__373c0__IEZFH link__373c0__29943 link-color--blue-dark__373c0__1mhJo link-size--inherit__373c0__2JXk5']").ToList();
            //var dataTables = doc.DocumentNode.SelectNodes("//table[@class='tr-table-scrollable']").ToList(); //doesn't work
            var headerText = doc.DocumentNode.SelectSingleNode("//h1[@id='h1-title']").InnerHtml;
            headerText = headerText.Replace(" Stats", "");
            var dataTables = doc.DocumentNode.SelectNodes("//table").ToList();
            var dataValues = doc.DocumentNode.SelectNodes("//table//tr//td");
            //var firstTable = dataTables[0];
            var rawList = new List<DataEntry>();
            //Console.WriteLine(rawList.GetType());
            //var dataTable = dataTables[0];

            //Console.WriteLine(headerText);
            //Console.WriteLine(dataTables.Count);
            //Console.WriteLine(dataTables[0].InnerText);

            //dataList.Add(new KeyValuePair<string, double>("Test",1.0));

            //Console.WriteLine(dataColumns.GetType());
            int counter = 0;
            for (int i = 0; i < dataValues.Count; i++)
            {                
                if (i%2 == 0)
                {
                    rawList.Add(new DataEntry { TeamName = headerText });
                    rawList[i/2].Statistic = dataValues[i].InnerText;
                }
                else
                {
                    rawList[i/2].Value = dataValues[i].InnerText;
                }
            }

            counter = 0;
            for (int i = 0; i < rawList.Count; i++)
            {
                Console.WriteLine(i + "    " + rawList[i].TeamName + "   " + rawList[i].Statistic + "     " + rawList[i].Value);
            }


            //foreach (var item in dataTables)
            //{
            //    Console.WriteLine(item.InnerText);
            //}
        }
    }
}
