using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using log4net;
using System.IO;
using LittleFootLiberaryAutomation;

namespace CarsAutomation
{
    /// <summary>
    /// Base Class that contains all the common methods.
    /// </summary>
    public class BaseTest
    {
        private TestContext testContextInstance;
        protected IWebDriver driver;
       

        public Microsoft.VisualStudio.TestTools.UnitTesting.TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                Console.WriteLine();
                testContextInstance = value;
            }
        }

        /// <summary>
        /// Method to navigate to the browser.
        /// </summary>
        public void LoginWithBrowser()
        {
            //driver = new ChromeDriver();
            string driverpath = Path.GetDirectoryName(Path.GetDirectoryName(TestContext.TestRunDirectory)) + @"\ChromeDriver";
            //IWebDriver driver = new ChromeDriver(Environment.CurrentDirectory);
            driver = new ChromeDriver(driverpath);


            Logger.Info("Launching new Browser" + Common.Default.WebsiteURL);
            driver.Navigate().GoToUrl(Common.Default.WebsiteURL);
            driver.Manage().Window.Maximize();
        }


        [TestCleanup]
        public void BrowserClose()
        {
            driver.Quit();
        }
    
    }
}



    