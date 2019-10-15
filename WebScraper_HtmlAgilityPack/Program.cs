using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

//Misc notes...
//https://www.c-sharpcorner.com/article/web-scraping-in-c-sharp/

//Next step:  make file output work

namespace WebScraper_HtmlAgilityPack
{
    class Program
    {
        static void Main(string[] args)
        {
            var teamList = new List<string>();                       
            teamList.Add("alabama-crimson-tide");
            teamList.Add("louisiana-state-tigers");
            var teamListImport = new List<string>();
            teamListImport = File.ReadLines("TeamNames.csv").Skip(1).ToList();

            //foreach (var x in teamListImport)
            //{
            //    Console.WriteLine(x);
            //}
            //Console.WriteLine(teamListImport.Count);

            //string scrapeLink = "https://www.teamrankings.com/college-football/team/alabama-crimson-tide/stats";
            //string scrapeLink = "https://www.teamrankings.com/college-football/team/" + teamList[0] + "/stats";
            //HtmlAgilityPack.HtmlDocument doc = web.Load("http://www.yellowpages.com/search?search_terms=Software&geo_location_terms=Sydney2C+ND");
            //HtmlAgilityPack.HtmlDocument doc = web.Load("https://www.yelp.com/search?find_desc=handyman&find_loc=Morrisville%2C+NC+27560&ns=1");

            //These are the important parts..
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            var rawList = new List<DataEntry>();
            var consolidatedList = new List<DataEntry>();

            //for (int teamNumber = 0; teamNumber < teamList.Count; teamNumber++)            
            for (int teamNumber = 0; teamNumber < 10; teamNumber++)
            //for (int teamNumber = 0; teamNumber < teamListImport.Count; teamNumber++)            
            {
                rawList.Clear();
                //string scrapeLink = "https://www.teamrankings.com/college-football/team/" + teamList[teamNumber] + "/stats";
                string scrapeLink = "https://www.teamrankings.com/college-football/team/" + teamListImport[teamNumber] + "/stats";
                HtmlAgilityPack.HtmlDocument doc = web.Load(scrapeLink);
                var headerText = doc.DocumentNode.SelectSingleNode("//h1[@id='h1-title']").InnerHtml;
                headerText = headerText.Replace(" Stats", "");
                var dataValues = doc.DocumentNode.SelectNodes("//table//tr//td");

                for (int i = 0; i < dataValues.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        rawList.Add(new DataEntry { TeamName = headerText });
                        rawList[i / 2].Statistic = dataValues[i].InnerText;
                    }
                    else
                    {
                        rawList[i / 2].Value = dataValues[i].InnerText;
                    }
                }
                consolidatedList.AddRange(rawList);              
            }

            //for (int i = 0; i < consolidatedList.Count; i++)
            //{
            //    Console.WriteLine(i + "    " + consolidatedList[i].TeamName + "   " + consolidatedList[i].Statistic + "     " + consolidatedList[i].Value);
            //}

            File.WriteAllLines("testOutput.csv", consolidatedList.Select(x => string.Join(",", x)));
        }
    }
}
