using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using Xunit.Abstractions;

namespace SearchEngine.UITests
{
    [Trait("Category", "Applications")]
    public class HTMLInteractionTestClass
    {
        private const string HomeUrl = "https://localhost:7017/";
        private const string PrivateUrl = "https://localhost:7017/Home/Privacy/";


        private readonly ITestOutputHelper output;

        public HTMLInteractionTestClass(ITestOutputHelper output)
        {
            this.output = output;
        }



        [Fact]
        public void GoToPrivacyPageByLink()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl(HomeUrl);
                TestHelper.Pause();
                IWebElement privacyLink = driver.FindElement(By.Name("ToPortal"));
                privacyLink.Click();

                TestHelper.Pause();

                Assert.Equal("Privacy Policy - WebApplication1", driver.Title);
                Assert.Equal(PrivateUrl, driver.Url);
            }
        }

        [Fact]
        public void GoToPrivacyPageByLinkText()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl(HomeUrl);
                TestHelper.Pause();
                IWebElement privacyLinkByLinkText = driver.FindElement(By.LinkText("Log på portal"));
                privacyLinkByLinkText.Click();

                TestHelper.Pause();

                Assert.Equal("Privacy Policy - WebApplication1", driver.Title);
                Assert.Equal(PrivateUrl, driver.Url);
            }
        }


        [Fact]
        public void GoToPrivacyByClassLink()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl(HomeUrl);
                TestHelper.Pause();
                IWebElement privacyLinkByClassName = driver.FindElement(By.ClassName("ToPortalClass"));
                privacyLinkByClassName.Click();

                TestHelper.Pause();

                Assert.Equal("Privacy Policy - WebApplication1", driver.Title);
                Assert.Equal(PrivateUrl, driver.Url);
            }
        }


        [Fact]
        public void DateHeaderInMyRulingTables()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl(HomeUrl);
                TestHelper.Pause();
                driver.Manage().Window.Maximize();
                IWebElement headerNameCell = driver.FindElement(By.ClassName("header-dato"));
               
                string headerName = headerNameCell.Text;
                TestHelper.Pause();

                Assert.Equal("Dato", headerName);

            }
        }

        [Fact]
        public void AllHeadersInMyRulingTables()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl(HomeUrl);
                //TestHelper.Pause();
                driver.Manage().Window.Maximize();
                ReadOnlyCollection<IWebElement> headerNameCells = driver.FindElements(By.XPath("//*[@id='myHeader']/div[2]/div"));
                TestHelper.Pause();

                Assert.Equal("Dato", headerNameCells[0].Text);
                Assert.Equal("Kendelsesnr.", headerNameCells[1].Text);
                Assert.Equal("Emneord", headerNameCells[2].Text);
                Assert.Equal("Selskab", headerNameCells[3].Text);
                Assert.Equal("Forsikringstype", headerNameCells[4].Text);
                Assert.Equal("Medhold", headerNameCells[5].Text);
                Assert.Equal("P.K", headerNameCells[6].Text);

            }
        }


        [Fact]
        public void FillInSearchParams()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Navigate to '{HomeUrl}'");
                driver.Navigate().GoToUrl(HomeUrl);
                driver.Manage().Window.Maximize();
                TestHelper.Pause();
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Typing in value to input text field");
                driver.FindElement(By.Id("txtInputAutocomplete")).SendKeys("Hund ");
                TestHelper.Pause(1000);
                driver.FindElement(By.Id("txtInputAutocomplete")).SendKeys("kat");
                TestHelper.Pause(1000);
                //Select Checkbox
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding element and clicking on it");
                driver.FindElement(By.Id("checkboxComplainantUpheld")).Click();
                TestHelper.Pause(1000);
                //select from custom company dropdown
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding element and opens it by clicking on it");
                driver.FindElement(By.Id("companyNames")).Click();
                TestHelper.Pause(1000);
                driver.FindElement(By.CssSelector("[data-tooltip*='Tryg Forsikring']")).Click();
                TestHelper.Pause(1000);
                driver.FindElement(By.CssSelector("[data-tooltip*='Alm. Brand']")).Click();
                //TestHelper.Pause(1000);
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding element and closes it by clicking on it");
                driver.FindElement(By.Id("companyNames")).Click();
                TestHelper.Pause(1000);
                ////select from custom incurance-type dropdown
                //driver.FindElement(By.Id("insuranceType")).Click();
                //TestHelper.Pause(1000);
                //driver.FindElement(By.CssSelector("[data-tooltip*='Ulykke']")).Click();
                //TestHelper.Pause(1000);
                //driver.FindElement(By.Id("insuranceType")).Click();
                //Select Date between
                //driver.FindElement(By.Id("fromDate")).Click();
                //TestHelper.Pause(1000);
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding element and setting the date by filling in value as a text sting");
                driver.FindElement(By.Id("fromDate")).SendKeys("24062012");
                //TestHelper.Pause(1000);
                driver.FindElement(By.Id("toDate")).SendKeys("26062012");
                //TestHelper.Pause(2000);
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Submitting the form");
                driver.FindElement(By.Id("btnAutocomplete")).Click();

                Assert.Equal("25.06.2012", driver.FindElement(By.ClassName("saved-date")).Text);
                Assert.Equal("81400", driver.FindElement(By.ClassName("saved-caseNumber")).Text);
                Assert.Equal("accept af risiko, personskade, hundeansvarsforsikring, besidder", driver.FindElement(By.ClassName("saved-searchWords")).Text);
                Assert.Equal("alm. brand", driver.FindElement(By.ClassName("saved-companyName")).Text);
                Assert.Equal("husdyr", driver.FindElement(By.ClassName("saved-insuranceType")).Text);
                Assert.Equal("klager medhold", driver.FindElement(By.ClassName("saved-result")).Text);
                Assert.Equal("ja", driver.FindElement(By.ClassName("saved-principle")).Text);

                TestHelper.Pause(5000);
            }
        }
        

        //[Fact]
        //public void GoToFogPByXPath()
        //{
        //    using (IWebDriver driver = new ChromeDriver())
        //    {

        //        driver.Navigate().GoToUrl(HomeUrl);
        //        TestHelper.Pause();
        //        driver.Manage().Window.Maximize();
        //        IWebElement fogpLinkXpath = driver.FindElement(By.XPath("/html/body/footer[1]/section/div/div/div[2]/div/div/div[2]"));

        //        fogpLinkXpath.Click();

        //        TestHelper.Pause();

        //        //Assert.Equal("Privacy Policy - WebApplication1", driver.Title);
        //        //Assert.Equal(PrivateUrl, driver.Url);
        //    }
        //}
    }
}
