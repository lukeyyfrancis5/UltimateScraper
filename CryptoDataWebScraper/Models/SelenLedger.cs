using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CryptoDataWebScraper.Models
{
    public class SelenLedger
    {
        [Key]
        public int SLId { get; set; }
        public DateTime STime { get; set; }
        public List<Stock> StocksL { get; set; }
    }
}