using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
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
            driver.Navigate().GoToUrl(homeURL);
        }
        /*
        Scenario: The application display the required message on Last Name
        Given the user is on the “Register” screen
          And the user filled the “First Name” field with “Test”
          And the user leaves the “Last Name” field empty
        When the user clicks on "Submit" button
        Then the application should display required message on the “Last Name” field
        */
        [Test(Description = "Scenario:The application display the required message on Last Name")]
        public void required_message_last_name_field()
        {
            IWebElement first_name = driver.FindElement(By.CssSelector("[ng-model='FirstName']"));
            first_name.SendKeys("Test");
            IWebElement last_name = driver.FindElement(By.CssSelector("[ng-model='LastName']"));
            last_name.SendKeys("");

            IWebElement SubmitButton = driver.FindElement(By.Id("submitbtn"));
            SubmitButton.Click();

            Assert.AreEqual("true", last_name.GetAttribute("required"));
        }

        /*
        Scenario: The application displays validation message when the user mouse over the email address
        Given the user is on the “Register” screen
        When the user hover the mouse on the "Email address" field
        Then the application should display "Provide a valid email id for further updates" message
        */
        [Test(Description = "Scenario: The application displays validation message when the user mouse over the email address")]
        public void email_mouse_over_message()
        {
            IWebElement first_name = driver.FindElement(By.CssSelector("[ng-model='FirstName']"));
            first_name.SendKeys("Test");
            IWebElement last_name = driver.FindElement(By.CssSelector("[ng-model='LastName']"));
            last_name.SendKeys("Test");

            IWebElement email_address = driver.FindElement(By.CssSelector("[ng-model='EmailAdress']"));
            Actions toolAct = new Actions(driver);
            toolAct.MoveToElement(email_address).Build().Perform();
            ////*[@id="basicBootstrapForm"]/div[3]/div[2]/span
            IWebElement email_address_tooltip = driver.FindElement(By.XPath("//span[contains(text(),'Provide a valid email id for further updates')]"));
            string email_address_tooltip_text = email_address_tooltip.Text;

            Assert.AreEqual("Provide a valid email id for further updates", email_address_tooltip_text);
        }

        /*
        Scenario: Creating a new register only with the required field
        Given the user is on the “Register” screen
          And the user filled all the required fields
        When the user clicks on the "Submit" button
        Then the application should create the new register
        */
        [Test(Description = "Scenario: Creating a new register only with the required field")]
        public void register_only_with_required_field()
        {
            IWebElement first_name = driver.FindElement(By.CssSelector("[ng-model='FirstName']"));
            first_name.SendKeys("Test");
            IWebElement last_name = driver.FindElement(By.CssSelector("[ng-model='LastName']"));
            last_name.SendKeys("Test");
            IWebElement email_address = driver.FindElement(By.CssSelector("[ng-model='EmailAdress']"));
            email_address.SendKeys("a@a.com");

            IWebElement phone = driver.FindElement(By.CssSelector("[ng-model='Phone']"));
            phone.SendKeys("1449440634");

            IWebElement gender_male = driver.FindElement(By.XPath("//*[@id='basicBootstrapForm']/div[5]/div/label[1]/input"));
            gender_male.Click();
            IWebElement country = driver.FindElement(By.XPath("//*[@id='countries']/option[33]"));
            country.Click();
            IWebElement birth_year = driver.FindElement(By.XPath("//*[@id='yearbox']/option[25]"));
            birth_year.Click();
            IWebElement birth_month = driver.FindElement(By.XPath("//*[@id='basicBootstrapForm']/div[11]/div[2]/select/option[5]"));
            birth_month.Click();
            IWebElement birth_day = driver.FindElement(By.XPath("//*[@id='daybox']/option[18]"));
            birth_day.Click();
            IWebElement password = driver.FindElement(By.Id("firstpassword"));
            password.SendKeys("Ab12345");
            IWebElement confirm_password = driver.FindElement(By.Id("secondpassword"));
            confirm_password.SendKeys("Ab12345");

            IWebElement SubmitButton = driver.FindElement(By.Id("submitbtn"));
            SubmitButton.Click();

            WebDriverWait wait = new WebDriverWait(driver,System.TimeSpan.FromSeconds(15));
            wait.Until(driver => driver.FindElement(By.XPath("/html/body/section/div[1]/div/div[2]/h4[1]")));
            IWebElement web_table_edit_instructions = driver.FindElement(By.XPath("/html/body/section/div[1]/div/div[2]/h4[1]"));
            string web_table_edit_text = web_table_edit_instructions.Text;
            Assert.AreEqual("- Double Click on Edit Icon to EDIT the Table Row.", web_table_edit_text);
        }

        [TearDown]
        public void TearDownTest()
        {
            driver.Close();
        }
    }
}
