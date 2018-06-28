using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using System.Data.Entity;

namespace CryptoDataWebScraper.Models.HAP
{
    public class HAPScraper
    {
        public void Run()
        {
            /*
            using (var db = new LedgerContext())
            {
                var url = "https://coinmarketcap.com/";

                var htmlWeb = new HtmlWeb();

                var htmlDocument = new HtmlDocument();

                htmlDocument = htmlWeb.Load(url);

                HtmlNodeCollection allRows = htmlDocument.DocumentNode.SelectNodes("//table[1]/tbody[1]/tr[*]");

                List<Coin> currencyList = new List<Coin>();

                foreach (var row in allRows)
                {
                    var CoinPosition = row.ChildNodes[1].InnerText;
                    var CoinSymbol = row.ChildNodes[3].ChildNodes[3].InnerText;
                    var CoinName = row.ChildNodes[3].ChildNodes[7].InnerText;
                    var CoinMarketcap = row.ChildNodes[5].InnerText;
                    var CoinChange = row.ChildNodes[13].InnerText;
                    var CoinPrice = row.ChildNodes[7].InnerText;


                    Coin coin = new Coin(CoinPosition, CoinSymbol, CoinName, CoinMarketcap, CoinChange, CoinPrice);
                    currencyList.Add(coin);
                }

                var ledger = new Ledger
                {
                    CryptoCoins = currencyList,
                    Time = DateTime.Now
                };
                db.Ledgers.Add(ledger);
                db.SaveChanges();
                Console.WriteLine();


            }
            */
        }
        
    }
        
}