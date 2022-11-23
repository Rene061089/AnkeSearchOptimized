using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;

namespace SearchEngine.UITests.PageObjectModels
{
    class HomePage : BasePage
    {
        protected override string PageTitle => "- WebApplication1";
        protected override string PageUrl => "https://localhost:7017/";
        

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

        public void EnterSearchWord(string word) => Driver.FindElement(By.Id("txtInputAutocomplete")).SendKeys(word);

        public void ClickCheckboxComplainantUpheld() => Driver.FindElement(By.Id("checkboxComplainantUpheld")).Click();

        public void ClickSubmitButton() =>  Driver.FindElement(By.Id("btnAutocomplete")).Click();

        public string CaseNumber => Driver.FindElement(By.ClassName("saved-caseNumber")).Text;

        


        public PrivacyPage ClickPrivacyLink()
        {
            Driver.FindElement(By.Name("ToPortal")).Click();
            return new PrivacyPage(Driver);
        }


    }
}
