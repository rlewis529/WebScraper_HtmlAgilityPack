using System;
using System.Collections.Generic;

namespace WebScraper_WebApp.Models
{
    public partial class CleanedData
    {
        public string TeamName { get; set; }
        public int? StatisticNumber { get; set; }
        public string Statistic { get; set; }
        public string RawValue { get; set; }
        public string SpacePlusRanking { get; set; }
        public string ValueOnly { get; set; }
        public string ValueType { get; set; }
        public decimal? ValueValue { get; set; }
        public decimal? PercentageValue { get; set; }
        public decimal? NetMarginValue { get; set; }
        public decimal? CombinedValue { get; set; }
    }
}
