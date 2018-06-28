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
        public string Position { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Change { get; set; }
        public string Price { get; set; }
        
        

        public Coin()
        {   
        }


        public Coin(string position, string symbol, string name, string change, string price)
        {
            try
            {
                Position = position;
                Symbol = symbol;
                Name = name;
                Change = change;
                Price = price;

            }
            catch (Exception e)
            {
                Console.WriteLine("Coin data parse error:\n" + e);
                throw;
            }
            


        }
    }
}