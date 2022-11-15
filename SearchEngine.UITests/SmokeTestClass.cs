using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using ApprovalTests.Reporters;
using System.IO;
using ApprovalTests;
using ApprovalTests.Reporters.Windows;
using SearchEngine.UITests.PageObjectModels;

namespace SearchEngine.UITests
{
    public class SmokeTestClass
    {
        private const string HomeUrl = "https://localhost:7017/";
        private const string PrivateUrl = "https://localhost:7017/Home/Privacy";
        private const string HomeTitle = "- WebApplication1";



        [Fact]
        [Trait("Category", "smoke")]
        public void LoadHomePage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                var homePage = new HomePage(driver);
                homePage.NavigateTo();

      
            }
        }


        [Fact]
        [Trait("Category","smoke")]
        public void ManipulatinBrowserWindowSize() 
        {
            using (IWebDriver driver = new ChromeDriver()) 
            {
                
                driver.Navigate().GoToUrl(HomeUrl);
                TestHelper.Pause(2000);
                driver.Manage().Window.Maximize();
                TestHelper.Pause(2000);
                driver.Manage().Window.Minimize();
                TestHelper.Pause(2000);
                driver.Manage().Window.Size = new System.Drawing.Size(300, 400);
                TestHelper.Pause(2000);
                driver.Manage().Window.Position = new System.Drawing.Point(0,0);
                TestHelper.Pause(2000);
                driver.Manage().Window.Position = new System.Drawing.Point(50, 50);
                TestHelper.Pause(2000);
                driver.Manage().Window.Position = new System.Drawing.Point(100, 100);
                TestHelper.Pause(2000);
                driver.Manage().Window.FullScreen();
                TestHelper.Pause(2000);

                //Skal fjernes. Denne bliver kun brugt til at sætte et delay, så man kan se hvad der egentlig sker, når testprogrammet åbner vores browservindue. 
                //TestHelper.Pause();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
            }
        }

        [Fact]
        [Trait("Category", "smoke")]
        public void ReloadHomePage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                
                driver.Navigate().GoToUrl(HomeUrl);
                driver.Navigate().Refresh();
                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);

            }
        }


        [Fact]
        [Trait("Category", "smoke")]
        public void ReloadHomePageOnBack()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                var homePage = new HomePage(driver);
                homePage.NavigateTo();
                driver.Manage().Window.Maximize();
                TestHelper.Pause(2000);
                driver.FindElement(By.Id("txtInputAutocomplete")).SendKeys("Hund ");
                driver.FindElement(By.Id("txtInputAutocomplete")).SendKeys("kat");
                TestHelper.Pause(2000);
                driver.FindElement(By.Id("btnAutocomplete")).Click();
                TestHelper.Pause(2000);
                string initialToken = homePage.GenerationToken; 

                driver.Navigate().GoToUrl(PrivateUrl);
                TestHelper.Pause(2000);
                driver.Navigate().Back();
                TestHelper.Pause(2000);
                homePage.EnsurePageHasLoaded();
                TestHelper.Pause(2000);
                string reloadedToken = homePage.GenerationToken;

                Assert.NotEqual(initialToken, reloadedToken);
            }
        }

        [Fact]
        [Trait("Category", "smoke")]
        public void ReloadHomePageOnForward()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl(PrivateUrl);
                //TestHelper.Pause();

                driver.Navigate().GoToUrl(HomeUrl);
                //TestHelper.Pause(100); // Giver random number tid til at genererer et nummer, ellers bliver værdien sat til - ""
                //IWebElement randomNumber = driver.FindElement(By.Id("readomNumberToTestOn"));
                //string initialToken = randomNumber.Text;

                //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(10));
                //IWebElement randomNumber = wait.Until((d) => d.FindElement(By.Id("readomNumberToTestOn")));
                //string initialToken = randomNumber.Text;

                driver.Navigate().Back();
                //TestHelper.Pause();

                driver.Navigate().Forward();
                TestHelper.Pause(200);// Giver random number tid til at genererer et nummer, ellers bliver værdien sat til - ""


                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);

                //string reloadedToken = driver.FindElement(By.Id("readomNumberToTestOn")).Text;
                //Assert.NotEqual(initialToken, reloadedToken);

            }
        }

        [Fact]
        [Trait("Category", "smoke")]
        public void OpenfrequentlyAskedQuestions()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                var homePage = new HomePage(driver);
                homePage.NavigateTo();

                homePage.ClickfrequentlyAskedQuestionsLink();


                ReadOnlyCollection<string> allTabs = driver.WindowHandles;
                string homePageTab = allTabs[0];
                string frequentlyQuestionsTab = allTabs[1];

                driver.SwitchTo().Window(frequentlyQuestionsTab);

                TestHelper.Pause(200);

                Assert.Equal("https://ankeforsikring.dk/Sider/faq.aspx", driver.Url);
            }
        }




        [Fact]
        [Trait("Category", "smoke")]
        [UseReporter(typeof(BeyondCompareReporter))]
        public void ScreenShotHomePage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl(PrivateUrl);
                driver.Manage().Window.Maximize();
                ITakesScreenshot screenShotDriver = (ITakesScreenshot)driver;

                Screenshot screenshot = screenShotDriver.GetScreenshot();

                TestHelper.Pause(5000);

                screenshot.SaveAsFile("privacy.bmp", ScreenshotImageFormat.Bmp);

                FileInfo file = new FileInfo("privacy.bmp");
                Approvals.Verify(file);

            }
        }

    }
}
