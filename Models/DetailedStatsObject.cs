using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snappet_Challenge.Models
{
    public class DetailedStatsObject
    {
        public int UserId { get; set; }
        public int TotalRek { get; set; }
        public int TotalBegrijpend { get; set; }
        public int TotalSpelling { get; set; }
        public double PercCorrectRek { get; set; }
        public double PercIncorrectRek { get; set; }
        public double PercCorrectBegrijpend { get; set; }
        public double PercIncorrectBegrijpend { get; set; }
        public double PercCorrectSpelling { get; set; }
        public double PercIncorrectSpelling { get; set; }
        public string EventuallyCorrectRek { get; set; }
        public string EventuallyCorrectBegijpend { get; set; }
        public string EventuallyCorrectSpelling { get; set; }
        public string MultipleIncorrectRek { get; set; }
        public string MultipleIncorrectBegrijpend { get; set; }
        public string MultipleIncorrectSpelling { get; set; }
    }
}
