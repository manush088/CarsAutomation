using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Threading;
using log4net;
using log4net.Config;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium.Interactions.Internal;
using OpenQA.Selenium.Interactions;
using System.Drawing;

namespace CarsAutomation
{
    [TestClass]
    public class CarsSearch : BaseTest
    {
        [Timeout(TestTimeout.Infinite)]
        [TestMethod]
        public void CarsSearchTest()
        {
            LoginWithBrowser();

            Logger.Info("Browser Initiated");

            SelectElement carType = new SelectElement(driver.FindElement(By.Id("make-model-search-stocktype")));
            carType.SelectByValue("used");
            Thread.Sleep(1000);
            Logger.Info("New/Used selected as new");

            SelectElement maker = new SelectElement(driver.FindElement(By.Id("makes")));
            maker.SelectByValue("honda");
            Thread.Sleep(1000);
            Logger.Info("Make selected as honda");

            SelectElement model = new SelectElement(driver.FindElement(By.Id("models")));
            model.SelectByValue("honda-pilot");
            Thread.Sleep(1000);
            Logger.Info("Model selected as honda-pilot");

            SelectElement price = new SelectElement(driver.FindElement(By.Id("make-model-max-price")));
            price.SelectByValue("50000");
            Thread.Sleep(1000);
            Logger.Info("Price selected as $50,000");

            SelectElement distance = new SelectElement(driver.FindElement(By.Id("make-model-maximum-distance")));
            distance.SelectByValue("100");
            Thread.Sleep(1000);
            Logger.Info("Distance selected as 100");

            IWebElement zip = driver.FindElement(By.Id("make-model-zip"));
            zip.SendKeys("60008");
            Thread.Sleep(1000);
            Logger.Info("Zip selected as 60008");

            IWebElement search = driver.FindElement(By.XPath("//*[@id='by-make-tab']/div/div[7]/button"));
            search.Click();
            Thread.Sleep(2000);
            Logger.Info("Search button clicked.");

            List<IWebElement> filters = driver.FindElements(By.XPath("//*[@id='active_filter_tags']/div[1]/label")).ToList();
            Thread.Sleep(2000);
            Assert.AreEqual(4, filters.Count());
            Assert.IsTrue(filters.Any(x => x.Text.Trim() == "Used"));
            Assert.IsTrue(filters.Any(x => x.Text.Trim() == "Honda"));
            Assert.IsTrue(filters.Any(x => x.Text.Trim() == "Pilot"));
            Assert.IsTrue(filters.Any(x => x.Text.Trim() == "Max price: $50,000"));
            Thread.Sleep(1000);
            Logger.Info("Selected filters verified.");

            SelectElement newUsed = new SelectElement(driver.FindElement(By.Id("stock-type-select")));
            newUsed.SelectByValue("new");
            Thread.Sleep(2000);
            Logger.Info("Used filter changed to new.");

            var selectedValue = newUsed.SelectedOption.GetAttribute("value");
            Assert.AreEqual("new", selectedValue);
            Logger.Info("Verify new is selected.");
            Thread.Sleep(2000);

            Point hoverTrim = driver.FindElement(By.ClassName("trim-group")).Location;
            ((IJavaScriptExecutor)driver).ExecuteScript("return window.title;");
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0," + (hoverTrim.Y) + ");");
            Thread.Sleep(2000);

            IWebElement check = driver.FindElement(By.CssSelector("#trim > div > div:nth-child(7) > label"));
            check.Click();
            Thread.Sleep(2000);
            Logger.Info("Touring 8-Passenger checked.");

            Point filterScroll = driver.FindElement(By.Id("search_form_container")).Location;
            ((IJavaScriptExecutor)driver).ExecuteScript("return window.title;");
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0," + (filterScroll.Y) + ");");
            Thread.Sleep(2000);

            List<IWebElement> updatedfilters = driver.FindElements(By.XPath("//*[@id='active_filter_tags']/div[1]/label")).ToList();
            Assert.AreEqual(5, updatedfilters.Count());
            Assert.IsTrue(updatedfilters.Any(x => x.Text.Trim() == "Touring 8-Passenger"));
            Thread.Sleep(2000);
            Logger.Info("Verify selected filters.");

           // driver.FindElements(By.ClassName("vehicle-card"))[2].Click();
            driver.FindElement(By.XPath("/html/body/section/div[2]/div[6]/div/div[1]/div[3]/div/div[2]/a")).Click();
            Thread.Sleep(10000);
            Logger.Info("Click on car.");

            string actualvalue = driver.FindElement(By.CssSelector(".title-section h1")).Text;
            Assert.IsTrue(actualvalue.Contains("Honda Pilot Touring 8-Passenger"), actualvalue + " doesn't contains 'Honda Pilot Touring 8-Passenger'");
            Thread.Sleep(2000);
            Logger.Info("Verified the title of the car.");

            bool isAvailble = driver.FindElement(By.XPath("//*[@id='fields-lead-form-embedded']/section/div/div[2]/button")).Displayed;
            Assert.IsTrue(isAvailble, "Button is not displayed");
            Thread.Sleep(2000);
            Logger.Info("Verified Check Availability button is displayed.");

            IWebElement firstName = driver.FindElement(By.Id("first_name"));
            firstName.SendKeys("Car");
            Thread.Sleep(1000);
            Logger.Info("Enter first name as Car.");

            IWebElement lastName = driver.FindElement(By.Id("last_name"));
            lastName.SendKeys("Owner");
            Thread.Sleep(1000);
            Logger.Info("Enter last name as Owner.");

            IWebElement email = driver.FindElement(By.Id("email"));
            email.SendKeys("carowner@yahoo.com");
            Thread.Sleep(1000);
            Logger.Info("Enter email as carowner@yahoo.com.");

            //Point hoverItem = driver.FindElement(By.ClassName("listing-loan-calculator")).Location;
            Point hoverItem = driver.FindElement(By.ClassName("basics-section")).Location;

            ((IJavaScriptExecutor)driver).ExecuteScript("return window.title;");
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0," + (hoverItem.Y) + ");");

            ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            screenshot.SaveAsFile(Path.GetDirectoryName(Path.GetDirectoryName(TestContext.TestRunDirectory)) + "/CarsAutomation/ScreenShots/Calculator_" + DateTime.UtcNow.ToString("yyyyMMddHHmmssffff") + ".png" );

            Logger.Info("Took the screen shot and saved in folder.");
        }
    }
}
