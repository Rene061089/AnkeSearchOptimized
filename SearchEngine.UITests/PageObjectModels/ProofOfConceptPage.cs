using OpenQA.Selenium;
using System.Drawing;
using System.Threading;

namespace SearchEngine.UITests.PageObjectModels
{
    class ProofOfConceptPage : BasePage
    {

        protected override string PageTitle => "- WebApplication1";
        protected override string PageUrl => "https://localhost:7017/";


        public ProofOfConceptPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public static void Pause(int secondsToPause = 3000)
        {
            Thread.Sleep(secondsToPause);
        }

        public void EnterSearchWord(string word) => Driver.FindElement(By.Id("txtInputAutocomplete")).SendKeys(word);
        public void ClickSubmitButton() => Driver.FindElement(By.Id("btnAutocomplete")).Click();
        public void OpenCompanySelectbox() => Driver.FindElement(By.Id("companyNames")).Click();
        public void FindCompanyName(string CompanyName) => Driver.FindElement(By.CssSelector("[data-tooltip*= " + "'" + CompanyName + "'" + "]")).Click();
        public void ClickCheckboxComplainantUpheld() => Driver.FindElement(By.Id("checkboxComplainantUpheld")).Click();
        public void OpenSummary() => Driver.FindElement(By.ClassName("arrow")).Click();
        public void SaveRuling() => Driver.FindElement(By.ClassName("star")).Click();
        public void RemoveSaveRuling() => Driver.FindElement(By.ClassName("saved")).Click();
        public void GoToSavedRulings() => Driver.FindElement(By.ClassName("goToSavedRulings")).Click();
        public void GoToSearchResult() => Driver.FindElement(By.ClassName("goToSearchResult")).Click();
        public void ClickGetMoreRulings() => Driver.FindElement(By.ClassName("getMoreRulings")).Click();
        public void OpenOrCloseAllSummaries() => Driver.FindElement(By.ClassName("open-close-all-resumées")).Click();


        public string CaseNumber => Driver.FindElement(By.ClassName("saved-caseNumber")).Text;
        public string CaseNumberSaved => Driver.FindElement(By.XPath("//*[@id='localSavedCases']/div[1]/div[2]/div/div[2]/div[2]")).Text;
        public string RulingsFound => Driver.FindElement(By.ClassName("totalRulingsSpan")).Text;
        public string NoSavedRulingsText => Driver.FindElement(By.CssSelector("div.filler-when-empty p")).Text;
        public string ToManyRulingsText => Driver.FindElement(By.ClassName("user-be-more-specifik-text")).Text;


        public void ResetButtonByJs()
        {
            string script = "document.getElementsByClassName('reset-search-button')[0].click();";
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript(script);
        }

        public void HighligtSummaryWordByJs(string word)
        {

            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            string searchWord = word;
            string colorType = "background-color:yellow";
            string colorStyle = $" style ={colorType}";
            word = $"<span{colorStyle}>{word}</span>";

            string summary = $"document.getElementsByClassName('summary')[0].innerHTML.replaceAll('{searchWord}', '{word}')";
            string summary2 = $"document.getElementsByClassName('summary')[0].innerHTML = {summary} ";

            js.ExecuteScript(summary);
            js.ExecuteScript(summary2);


        }

        public void ScrollWindowByJS(int x, int y)
        {
            string scroll = "window.scrollBy(" + x + "," + y + ")";
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript(scroll);

        }

        public void ScrollToViewByJS(string className)
        {
            string goTo = "'" + className + "'";
            string scroll = $"document.getElementsByClassName({goTo})[0].scrollIntoView()";
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript(scroll);

        }


        public void ChangeElementColorByJS(string classNameOrId)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            string changeColor = "document.getElementsByClassName(" + "'" + classNameOrId + "'" + ")[0].style.color = 'red';";
            js.ExecuteScript(changeColor);

        }


        public void ClickLinkByJS(string linkId)
        {

            string linkToClick = "'" + linkId + "'"; 
            string script = $"document.getElementById({linkToClick}).click();";
            
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript(script);

        }


       
    }
}
