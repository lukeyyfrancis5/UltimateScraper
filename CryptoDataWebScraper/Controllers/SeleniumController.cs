using System;
using System.Web.Mvc;
using CryptoDataWebScraper.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace CryptoDataWebScraper.Controllers
{
    public class SeleniumController : Controller
    {
        // GET: Selenium
        public ActionResult Selenium()
        {
            ViewBag.Messege = "Yahoo Finance";
            var url = "https://login.yahoo.com/";
            IWebDriver chromeDriver = new ChromeDriver();
            chromeDriver.Navigate().GoToUrl(url);
            var wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(15));
            wait.Until(cdrver => cdrver.FindElement(By.XPath("//input[@id='login-username']")));

            chromeDriver.FindElement(By.Id("login-username")).SendKeys("Ilovecheese5");
            chromeDriver.FindElement(By.Id("login-username")).Submit();

            using (var db = new SeleniumContext())
            {




                return View(db);
            }

            
        }
    }
}