using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebScraper_WebApp.Models
{
    public class ResultViewModel
    {
        public List<ResultListItem> resultList { get; set; }
        
        //public List<string> statNames { get; set; }
        public List<StatListItem> statList { get; set; }

    }
}
