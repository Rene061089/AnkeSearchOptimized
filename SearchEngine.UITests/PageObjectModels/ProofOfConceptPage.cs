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

        public void EnterSearchWord(string word) => Driver.FindElement(By.Id("txtInputAutocomplete")).SendKeys(word);
        public void ClickSubmitButton() => Driver.FindElement(By.Id("btnAutocomplete")).Click();
        public void OpenCompanySelectbox() => Driver.FindElement(By.Id("companyNames")).Click();
        public void FindCompanyName(string CompanyName) => Driver.FindElement(By.CssSelector("[data-tooltip*= " + "'" + CompanyName + "'" + "]")).Click();
        public void ClickCheckboxComplainantUpheld() => Driver.FindElement(By.Id("checkboxComplainantUpheld")).Click();
        public void OpenSummary() => Driver.FindElement(By.ClassName("arrow")).Click();
        public void SummaryText()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            string colorType = "color:red;";
            Pause(1000);
            string colorChange = $" style ={colorType}";
            Pause(1000);
            string hund = $"<span{colorChange}>hund</span>";
        

            string summary = $"document.getElementsByClassName('summary')[0].innerHTML.replaceAll('hund', '{hund}')"  ;
            string summary2 =$"document.getElementsByClassName('summary')[0].innerHTML = {summary} ";


            js.ExecuteScript(summary);
           
            js.ExecuteScript(summary2);

           
        }
        public string CaseNumber => Driver.FindElement(By.ClassName("saved-caseNumber")).Text;

        public static void Pause(int secondsToPause = 3000)
        {
            Thread.Sleep(secondsToPause);
        }





        public void ScrollWindowByJS(int x, int y)
        {
            string scroll = "window.scrollBy(" + x + "," + y + ")";
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript(scroll);

        }

        public void ChangeElementColorByJS(string classNameOrId)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            string changeColor = "document.getElementsByClassName(" + "'" + classNameOrId + "'" + ")[0].style.color = 'red';";
            js.ExecuteScript(changeColor);

        }

    }
}
