using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CryptoDataWebScraper.Models
{
    public class Ledger
    {
        public int LedgerId { get; set; }
        public DateTime Time { get; set; }
        public List<Coin> CryptoCoins { get; set; }
    }
}