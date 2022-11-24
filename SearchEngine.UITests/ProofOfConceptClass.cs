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
                pocPage.ChangeElementColorByJS("saved-companyName");
                pocPage.ChangeElementColorByJS("saved-result");
                Assert.Equal("81400", pocPage.CaseNumber);


                pocPage.OpenSummary();
                pocPage.ScrollWindowByJS(0, 500);
                pocPage.HighligtSummaryWordByJs("hund");
                TestHelper.Pause(1000);
                pocPage.HighligtSummaryWordByJs("kat");
                TestHelper.Pause(5000);


                TestHelper.Pause();
                pocPage.SaveRuling();
                TestHelper.Pause();


                pocPage.ScrollWindowByJS(0, -500);
                pocPage.ChangeElementColorByJS("reset-search-button");
                TestHelper.Pause(1000);
                pocPage.ResetButtonByJs();
                TestHelper.Pause(1000);
                string howManyCases = pocPage.RulingsFound;
                Assert.Equal("46375", howManyCases);
                pocPage.ChangeElementColorByJS("totalRulingsSpan");
                TestHelper.Pause(5000);


                pocPage.GoToSavedRulings();
                Assert.NotEqual("Du har ikke gemt nogle afgørelser", pocPage.NoSavedRulingsText);
                TestHelper.Pause(5000);
                pocPage.RemoveSaveRuling();
                TestHelper.Pause();
                Assert.Equal("Du har ikke gemt nogle afgørelser", pocPage.NoSavedRulingsText);
                TestHelper.Pause(5000);

                pocPage.GoToSearchResult();
                TestHelper.Pause(5000);
            }

        }

        [Fact]
        public void LoadMaxRulings()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var counter = 1;
                var pocPage = new ProofOfConceptPage(driver);
                pocPage.NavigateTo();
                pocPage.MaximizeBrowserWindow();
                TestHelper.Pause(2000);
                pocPage.ScrollWindowByJS(0, 1800);
                TestHelper.Pause();
                
                while (counter < 10)
                {
                    Assert.NotEqual("Du har hentet 100 afgørelser og endnu ikke fundet hvad du ledte efter." + "\r\n" +
                             "Vær mere specifik i din søgning for at indsævre søgningen, dette kan gøres ved at vælge flere søgeparametre.", pocPage.ToManyRulingsText);
                    pocPage.ClickGetMoreRulings();
                    counter++;
                    
                    TestHelper.Pause(500);
                }

                TestHelper.Pause();
                Assert.Equal("Du har hentet 100 afgørelser og endnu ikke fundet hvad du ledte efter." + "\r\n" +
                             "Vær mere specifik i din søgning for at indsævre søgningen, dette kan gøres ved at vælge flere søgeparametre.", pocPage.ToManyRulingsText);
            }

        }

    }
}
