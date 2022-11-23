using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;
using SearchEngine.UITests.PageObjectModels;

namespace SearchEngine.UITests
{

    [Trait("Category", "ProofOfConcept")]
    public class ProofOfConceptClass
    {

      

       [Fact]
       public void SendAndSubmitForm()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                var pocPage = new ProofOfConceptPage(driver);
                pocPage.NavigateTo();
                pocPage.MaximizeBrowserWindow();
                
                pocPage.EnterSearchWord("Hund ");
                pocPage.EnterSearchWord("kat");
                pocPage.ClickCheckboxComplainantUpheld();
                pocPage.OpenCompanySelectbox();
                TestHelper.Pause(1000);
                pocPage.FindCompanyName("Alm. Brand");
                pocPage.ClickSubmitButton();
                pocPage.ScrollWindowByJS(0, 500);
                TestHelper.Pause();
                pocPage.ChangeElementColorByJS("saved-caseNumber");
                pocPage.OpenSummary();
                pocPage.ScrollWindowByJS(0, 500);
                pocPage.SummaryText();
                TestHelper.Pause(20000);
                
                Assert.Equal("81400", pocPage.CaseNumber);

            }

        }

    }
}
