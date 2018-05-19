using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CryptoDataWebScraper.Models
{
    public class Coin
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }


        public Coin(string symbol, string name, string price)
        {
            Symbol = symbol;
            Name = name;
            Price = Double.Parse(price, NumberStyles.Currency);
        }
    }
}