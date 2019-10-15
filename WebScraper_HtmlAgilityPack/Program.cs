using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

            
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            var rawList = new List<DataEntry>();
            var consolidatedList = new List<DataEntry>();

            for (int teamNumber = 0; teamNumber < 5; teamNumber++)
            //for (int teamNumber = 0; teamNumber < teamListImport.Count; teamNumber++)            
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
            }        

            var sb = new StringBuilder();
            foreach (var data in consolidatedList)
            {
                sb.AppendLine(data.TeamName + "," + data.StatisticNumber + "," + data.Statistic + "," + data.Value);
            }

            File.WriteAllText("testOutput.csv", sb.ToString());
        }
    }
}
