using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;
using SearchEngine.UITests.PageObjectModels;
using System.Collections.ObjectModel;

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
               // TestHelper.Pause(1000);
                pocPage.EnterSearchWord("Hund ");
                pocPage.EnterSearchWord("kat");
                //TestHelper.Pause(1000);
                pocPage.ClickCheckboxComplainantUpheld();
                pocPage.OpenCompanySelectbox();
                TestHelper.Pause(500);
                pocPage.FindCompanyName("Alm. Brand");
                pocPage.ClickSubmitButton();
                pocPage.ScrollWindowByJS(0, 500);
                TestHelper.Pause(500);
                pocPage.ChangeElementColorByJS("saved-caseNumber");
                pocPage.ChangeElementColorByJS("saved-companyName");
                pocPage.ChangeElementColorByJS("saved-result");
                Assert.Equal("81400", pocPage.CaseNumber);
                //TestHelper.Pause();

                pocPage.OpenSummary();
                pocPage.ScrollWindowByJS(0, 500);
                pocPage.HighligtSummaryWordByJs("hund");
                TestHelper.Pause(300);
                pocPage.HighligtSummaryWordByJs("kat");
                //TestHelper.Pause(6000);


                //TestHelper.Pause();
                pocPage.SaveRuling();
                //TestHelper.Pause();


                pocPage.ScrollWindowByJS(0, -500);
                pocPage.ChangeElementColorByJS("reset-search-button");
                //TestHelper.Pause(1000);
                pocPage.ResetButtonByJs();
                TestHelper.Pause(200);
                string howManyCases = pocPage.RulingsFound;
                Assert.Equal("46375", howManyCases);
                pocPage.ChangeElementColorByJS("totalRulingsSpan");
                TestHelper.Pause(200);


                pocPage.GoToSavedRulings();
                TestHelper.Pause(200);
                Assert.NotEqual("Du har ikke gemt nogen afgørelser", pocPage.NoSavedRulingsText);
                Assert.Equal("81400", pocPage.CaseNumberSaved);
                TestHelper.Pause(200);
                pocPage.RemoveSaveRuling();
                //TestHelper.Pause();
                Assert.Equal("Du har ikke gemt nogen afgørelser", pocPage.NoSavedRulingsText);
                TestHelper.Pause(200);

                pocPage.GoToSearchResult();
                //TestHelper.Pause();
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
                    Assert.NotEqual("Du har nu hentet 100 afgørelser, og endnu ikke fundet det du ledte efter." + "\r\n" +
                             "Vær mere specifik i din søgning for at indsnævre resultaterne. Dette kan du gøres ved at vælge flere søgeparametre.", pocPage.ToManyRulingsText);
                    pocPage.ClickGetMoreRulings();
                    counter++;
                    
                    TestHelper.Pause(500);
                }

                TestHelper.Pause();
                Assert.Equal("Du har nu hentet 100 afgørelser, og endnu ikke fundet det du ledte efter." + "\r\n" +
                             "Vær mere specifik i din søgning for at indsnævre resultaterne. Dette kan du gøres ved at vælge flere søgeparametre.", pocPage.ToManyRulingsText);

                TestHelper.Pause(5000);
            }
            
        }

        [Fact]
        public void CheckAllLinks()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var taenkURL = "https://taenk.dk/";
                var fogpURL = "https://www.fogp.dk/";
                var financeEU_URL = "https://finance.ec.europa.eu/consumer-finance-and-payments/retail-financial-services/financial-dispute-resolution-network-fin-net_en";
                var emURL = "https://em.dk/";

                var pocPage = new ProofOfConceptPage(driver);
                pocPage.NavigateTo();
                pocPage.MaximizeBrowserWindow();
                TestHelper.Pause(1000);
                pocPage.ScrollWindowByJS(0, 2800);
                TestHelper.Pause(8000);

                pocPage.ClickLinkByJS("financeEU");
                pocPage.ClickLinkByJS("em");
                pocPage.ClickLinkByJS("ftaenk");
                pocPage.ClickLinkByJS("fogp");
                
                ReadOnlyCollection<string> allTabs = driver.WindowHandles;
                string ftaenksTab = allTabs[1]; 
                string fogpTab = allTabs[2]; 
                string financeEUTab = allTabs[3];
                string emTab = allTabs[4]; 

                driver.SwitchTo().Window(ftaenksTab);
                Assert.Equal(taenkURL, driver.Url);
                TestHelper.Pause(3000);
                driver.SwitchTo().Window(fogpTab);
                Assert.Equal(fogpURL, driver.Url);
                TestHelper.Pause(3000);
                driver.SwitchTo().Window(financeEUTab);
                Assert.Equal(financeEU_URL, driver.Url);
                TestHelper.Pause(3000);
                driver.SwitchTo().Window(emTab);
                Assert.Equal(emURL, driver.Url);

                TestHelper.Pause(5000);
            }
        }

       

    }
}
