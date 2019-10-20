using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

//Misc notes...
//https://www.c-sharpcorner.com/article/web-scraping-in-c-sharp/
//Example link:  https://www.teamrankings.com/college-football/team/louisiana-state-tigers/stats

//Next steps:  
// Import text file into SQL table(s)?

namespace WebScraper_HtmlAgilityPack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Accessing source webpages and scraping data...");
            Console.WriteLine();
            Console.WriteLine("Data collected from...");
            var teamList = new List<string>();                       
            //teamList.Add("alabama-crimson-tide");
            //teamList.Add("louisiana-state-tigers");
            var teamListImport = new List<string>();
            teamListImport = File.ReadLines("TeamNames.csv").Skip(1).ToList();        

            
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            var rawList = new List<DataEntry>();
            var consolidatedList = new List<DataEntry>();
            var sb = new StringBuilder();

            for (int teamNumber = 0; teamNumber < teamListImport.Count; teamNumber++)            
            {
                rawList.Clear();               
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
                        rawList[i / 2].StatisticNumber = i / 2;
                    }
                    else
                    {
                        rawList[i / 2].Value = dataValues[i].InnerText;
                    }
                }
                consolidatedList.AddRange(rawList);  
                foreach (var data in rawList)
                {
                    sb.AppendLine(data.TeamName + "," + data.StatisticNumber + "," + data.Statistic + "," + data.Value);
                }
                Console.WriteLine(teamListImport[teamNumber]);
            }
            File.WriteAllText("testOutput.csv", sb.ToString());
            Console.WriteLine("Data collection complete.");
        }
    }
}
