using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;

namespace SearchEngine.UITests.PageObjectModels
{
    class HomePage
    {
        private readonly IWebDriver Driver;
        private const string PageUrl = "https://localhost:7017/";
        private const string PageTitle = "- WebApplication1";

        public HomePage(IWebDriver driver)
        {
            Driver = driver;
        }


        public ReadOnlyCollection<IWebElement> TableHeaders
        {
            get
            {
               return Driver.FindElements(By.XPath("//*[@id='myHeader']/div[2]/div"));
            }
        }

        //
        public string GenerationToken => Driver.FindElement(By.ClassName("saved-caseNumber")).Text;
        public void ClickfrequentlyAskedQuestionsLink() => Driver.FindElement(By.Name("frequentlyAskedQuestions")).Click();

        public void NavigateTo()
        {
            Driver.Navigate().GoToUrl(PageUrl);
            EnsurePageHasLoaded();
        }

        public void EnsurePageHasLoaded()
        {
            bool ensurePageHasLoaded = (Driver.Url == PageUrl ) && (Driver.Title == PageTitle);

            if (!ensurePageHasLoaded)
            {
                throw new Exception($"Falid to load page. Page URL = '{Driver.Url}' Page Source \r\n {Driver.PageSource}");
            }
        }

    }
}
