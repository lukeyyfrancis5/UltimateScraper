using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CryptoDataWebScraper.Models;
using HtmlAgilityPack;
using System.Data.Entity;

namespace CryptoDataWebScraper.Controllers
{
    public class HomeController : Controller
    {
        private LedgerContext _context;

        public HomeController()
        {
            _context = new LedgerContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        public ActionResult ViewSnaps()
        {
            ViewBag.Message = "Checkout your snap records !";

            var ledgers = _context.Ledgers.ToList();
            return View(ledgers);
        }       

        public ActionResult ViewSnap(int id)
        {
            var ledgers = _context.Ledgers.Include(l => l.CryptoCoins).FirstOrDefault(l => l.LedgerId == id);

            if (ledgers == null)
                return HttpNotFound();

            return View(ledgers);
        }   

        public ActionResult CryptoData()
        {
            ViewBag.Message = "Here are the top 100 coins !";
            ViewBag.Time = DateTime.Now;

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
                    var CoinSymbol = row.ChildNodes[3].ChildNodes[3].InnerText;
                    var CoinName = row.ChildNodes[3].ChildNodes[5].InnerText;
                    var CoinPrice = row.ChildNodes[7].InnerText;

                    Coin coin = new Coin(CoinSymbol, CoinName, CoinPrice);
                    currencyList.Add(coin);
                }

                //Creates a date.time snapshot-this will be my future snapshot button


                var ledger = new Ledger
                {
                    CryptoCoins = currencyList,
                    Time = DateTime.Now
                };
                db.Ledgers.Add(ledger);
                db.SaveChanges();
                Console.WriteLine();

                return View(currencyList);
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}