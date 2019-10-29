using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

//Misc notes...
//https://www.c-sharpcorner.com/article/web-scraping-in-c-sharp/
//Example link:  https://www.teamrankings.com/college-football/team/louisiana-state-tigers/stats

namespace WebScraper_HtmlAgilityPack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Accessing source webpages and scraping data...");            
            Console.WriteLine();
            Console.WriteLine("In-game statistics collected from...");
            var teamList = new List<string>();                       
            //teamList.Add("alabama-crimson-tide");
            //teamList.Add("louisiana-state-tigers");
            var teamListImport = new List<string>();
            teamListImport = File.ReadLines("TeamNames.csv").Skip(1).ToList();        

            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            var rawList = new List<DataEntry>();                        
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
                
                foreach (var data in rawList)
                {
                    sb.AppendLine(data.TeamName + "," + data.StatisticNumber + "," + data.Statistic + "," + data.Value);
                }
                Console.WriteLine(teamListImport[teamNumber]);
            }

            File.WriteAllText("testOutput.csv", sb.ToString());
            Console.WriteLine();
            Console.WriteLine("*** In-game statistic data collection complete! ***");
            Console.WriteLine();
            Console.WriteLine("Scraping data for game results...");
            Console.WriteLine();
            Console.WriteLine("Game results collected from...");

            var rawList2 = new List<DataEntry2>();            
            var sb2 = new StringBuilder();

            for (int teamNumber2 = 0; teamNumber2 < teamListImport.Count; teamNumber2++)            
            {
                rawList2.Clear();
                string scrapeLink2 = "https://www.teamrankings.com/college-football/team/" + teamListImport[teamNumber2] + "/game-log";
                HtmlAgilityPack.HtmlDocument doc2 = web.Load(scrapeLink2);
                var headerText2 = doc2.DocumentNode.SelectSingleNode("//h1[@id='h1-title']").InnerHtml;
                headerText2 = headerText2.Replace(" Performance", "");
                var dataValues2 = doc2.DocumentNode.SelectNodes("//table//tr//td");
                                
                for (int j = 0; j < dataValues2.Count; j++)
                {
                    if (j % 5 == 0)
                    {
                        rawList2.Add(new DataEntry2 { TeamName = headerText2 });
                        rawList2[j/5].WeekName = dataValues2[j].InnerText;
                    }
                    if (j % 5 == 1)
                    {                        
                        rawList2[j / 5].Opponent = dataValues2[j].InnerText;
                    }
                    if (j % 5 == 2)
                    {
                        rawList2[j / 5].OpponentRank = dataValues2[j].InnerText;
                    }
                    if (j % 5 == 3)
                    {
                        rawList2[j / 5].GameLocation = dataValues2[j].InnerText;
                    }
                    if (j % 5 == 4)
                    {
                        rawList2[j / 5].GameResult = dataValues2[j].InnerText;
                    }
                }
                
                foreach (var data2 in rawList2)
                {
                    sb2.AppendLine(data2.TeamName + "," + data2.WeekName + "," + data2.Opponent + "," + data2.OpponentRank + "," + data2.GameLocation + "," + data2.GameResult);
                }
                Console.WriteLine(teamListImport[teamNumber2]);               
            }            
            File.WriteAllText("testOutput2.csv", sb2.ToString());
            Console.WriteLine();
            Console.WriteLine("*** Game result data collection complete! ***");
        }
    }
}
