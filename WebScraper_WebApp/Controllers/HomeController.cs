using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebScraper_WebApp.Models;

namespace WebScraper_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WebScraperContext dbContext;

        public HomeController(ILogger<HomeController> logger, WebScraperContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;
        } 

        [HttpPost]
        public IActionResult Index(int statSelect)
        {            
            var q = from a in dbContext.CleanedData
                    where a.StatisticNumber == statSelect
                    orderby a.ValueValue descending
                    select new ResultListItem
                    {
                        teamName = a.TeamName,
                        statValue = (decimal)a.ValueValue,
                        statName = a.Statistic
                    };
            List<ResultListItem> resultList = q.ToList();

            var statValueCategories = from a in dbContext.CleanedData
                                      where a.TeamName == "Air Force Falcons"
                                      && a.ValueType == "Value"
                                      orderby a.StatisticNumber                                      
                                      select new StatListItem
                                      {
                                          statName = a.Statistic,
                                          statNumber = (int)a.StatisticNumber
                                      };
            List<StatListItem> statList = statValueCategories.ToList();


            ResultViewModel resultViewModel = new ResultViewModel();
            resultViewModel.resultList = resultList;
            resultViewModel.statList = statList;                

            return View(resultViewModel);
        }


        public IActionResult Index()
        {
            var q = from a in dbContext.CleanedData
                    where a.StatisticNumber == 0
                    orderby a.ValueValue descending                    
                    select new ResultListItem 
                    { 
                        teamName = a.TeamName,
                        statValue = (decimal)a.ValueValue,
                        statName = a.Statistic
                    };                        
            List<ResultListItem> resultList = q.ToList();

            var statValueCategories = from a in dbContext.CleanedData
                                      where a.TeamName == "Air Force Falcons"
                                      && a.ValueType == "Value"
                                      orderby a.StatisticNumber                                      
                                      select new StatListItem
                                      {
                                          statName = a.Statistic,
                                          statNumber = (int)a.StatisticNumber
                                      };
            List<StatListItem> statList = statValueCategories.ToList();


            ResultViewModel resultViewModel = new ResultViewModel();
            resultViewModel.resultList = resultList;
            resultViewModel.statList = statList;

            return View(resultViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
