using System;
using System.Collections.Generic;

namespace WebScraper_WebApp.Models
{
    public partial class RawData
    {
        public string TeamName { get; set; }
        public int? StatisticNumber { get; set; }
        public string Statistic { get; set; }
        public string RawValue { get; set; }
    }
}
