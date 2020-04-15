using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;

namespace TestAutomationRegisterWebSite
{
    public class ThreeTestScenariosForRegisterWebSite
    {
        private IWebDriver driver;
        public string homeURL;

        [SetUp]
        public void SetupTest()
        {
            homeURL = "http://demo.automationtesting.in/Register.html";
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

        }

        [Test(Description = "Scenario:The application display the required message on Last Name")]
        public void required_message_last_name_field()
        {
            driver.Navigate().GoToUrl(homeURL);
            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(15));

            IWebElement FirstName = driver.FindElement(By.CssSelector("[ng-model='FirstName']"));
            FirstName.SendKeys("Test");
            IWebElement LastName = driver.FindElement(By.CssSelector("[ng-model='LastName']"));
            LastName.SendKeys("");

            IWebElement SubmitButton = driver.FindElement(By.Id("submitbtn"));
            SubmitButton.Click();

            Assert.AreEqual("true", LastName.GetAttribute("required"));
        }

        [TearDown]
        public void TearDownTest()
        {
            driver.Close();
        }
    }
}
