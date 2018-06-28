using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CryptoDataWebScraper.Models;
using HtmlAgilityPack;
using System.Data.Entity;
using CryptoDataWebScraper.Models.HAP;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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

        public ActionResult YahooStock()
        {
            ViewBag.Message = "Here are your Stocks !";
            ViewBag.Time = DateTime.Now;

            /*
            IWebDriver chromeDriver = new ChromeDriver();
            Auth.YahooSignin(chromeDriver);

            var ext = ExtractStock.Extractor(chromeDriver);
            */

            var url = "https://login.yahoo.com/";

            var pass = "Iloverice5";

            var email = "lukebrandonfrancis@yahoo.com";

            var chromeDriver = new ChromeDriver();

            chromeDriver.Navigate().GoToUrl(url);

            var wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(120));
            chromeDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);

            IWebElement emailBox = chromeDriver.FindElement(By.XPath("//*[@id='login-username']"));
            emailBox.SendKeys(email);

            IWebElement nextBox = chromeDriver.FindElement(By.XPath("//*[@id='login-signin']"));
            nextBox.Click();

            wait.Until(d => d.FindElement(By.Id("login-passwd")));

            IWebElement passBox = chromeDriver.FindElement(By.Id("login-passwd"));
            passBox.SendKeys(pass);
            passBox.SendKeys(Keys.Enter);

            chromeDriver.Navigate().GoToUrl("https://finance.yahoo.com/");

            Console.WriteLine("Complete !");
            
            using (var db = new LedgerContext())
            {
                List<Stock> stocksList = new List<Stock>();
                for(var row = 2; row < chromeDriver.FindElements(By.XPath("//section[2]/table/tbody/tr[*]")).Count; row++)
                {   
                    var stockSymbol =
                        chromeDriver.FindElement(By.XPath("//section[2]/table/tbody/tr[" + row + "]/td[1]/a")).Text;

                    var stockName =
                        chromeDriver.FindElement(By.XPath("//section[2]/table/tbody/tr[" + row + "]/td[1]/p")).Text;

                    var stockLPrice =
                        chromeDriver.FindElement(By.XPath("//section[2]/table/tbody/tr[" + row + "]/td[2]")).Text;

                    var stockChange =
                        chromeDriver.FindElement(By.XPath("//section[2]/table/tbody/tr[" + row + "]/td[3]/span")).Text;

                    var stockPChange =
                        chromeDriver.FindElement(By.XPath("//section[2]/table/tbody/tr[" + row + "]/td[4]/span")).Text;

                    Stock stock = new Stock(stockSymbol, stockName, stockLPrice, stockChange, stockPChange);
                    stocksList.Add(stock);                
                }

                    var ledger = new SelenLedger
                    {
                        StocksL = stocksList,
                        STime = DateTime.Now
                    };
                    db.SelenLedgers.Add(ledger);
                    Console.WriteLine();
                    db.SaveChanges();

                    return View(stocksList);
            }
            
            
        }
        
        
        public ActionResult ViewStockSnaps()
        {
            ViewBag.Message = "Checkout your Stock snap records !";

            var ledgers = _context.SelenLedgers.ToList();
            return View(ledgers);
        }

        public ActionResult ViewStockSnap(int id)
        {
            var ledgers = _context.SelenLedgers.Include(S => S.StocksL).FirstOrDefault(S => S.SLId == id);

            if (ledgers == null)
                return HttpNotFound();

            return View(ledgers);
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
            /*
            var hap = new HAPScraper();
            hap.Run();
            */

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
                    var CoinChange = row.ChildNodes[13].InnerText;
                    var CoinPrice = row.ChildNodes[7].InnerText;


                    Coin coin = new Coin(CoinPosition, CoinSymbol, CoinName, CoinChange, CoinPrice);
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