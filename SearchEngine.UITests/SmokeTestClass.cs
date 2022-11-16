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
    public class SmokeTestClass : IClassFixture<ChromeDriverShared>
    {
        private const string HomeUrl = "https://localhost:7017/";
        private const string PrivateUrl = "https://localhost:7017/Home/Privacy";
        private const string HomeTitle = "- WebApplication1";
        private readonly ChromeDriverShared ChromeDriverShared;

        public SmokeTestClass(ChromeDriverShared chromeDriverShared)
        {
            ChromeDriverShared = chromeDriverShared;
            ChromeDriverShared.Driver.Manage().Cookies.DeleteAllCookies();
            ChromeDriverShared.Driver.Navigate().GoToUrl("about:blank");
        }


        [Fact]
        [Trait("Category", "smoke")]
        public void LoadHomePage()
        {

                var homePage = new HomePage(ChromeDriverShared.Driver);
                homePage.NavigateTo();

        }



        [Fact]
        [Trait("Category", "smoke")]
        public void ReloadHomePageOnBack()
        {
          

                var homePage = new HomePage(ChromeDriverShared.Driver);
                homePage.NavigateTo();
                ChromeDriverShared.Driver.Manage().Window.Maximize();
                TestHelper.Pause(2000);
                ChromeDriverShared.Driver.FindElement(By.Id("txtInputAutocomplete")).SendKeys("Hund ");
                ChromeDriverShared.Driver.FindElement(By.Id("txtInputAutocomplete")).SendKeys("kat");
                TestHelper.Pause(2000);
                ChromeDriverShared.Driver.FindElement(By.Id("btnAutocomplete")).Click();
                TestHelper.Pause(2000);
                string initialToken = homePage.GenerationToken;

                ChromeDriverShared.Driver.Navigate().GoToUrl(PrivateUrl);
                TestHelper.Pause(2000);
                ChromeDriverShared.Driver.Navigate().Back();
                TestHelper.Pause(2000);
                homePage.EnsurePageHasLoaded();
                TestHelper.Pause(2000);
                string reloadedToken = homePage.GenerationToken;

                Assert.NotEqual(initialToken, reloadedToken);
            
        }


        [Fact]
        [Trait("Category", "smoke")]
        public void OpenfrequentlyAskedQuestions()
        {
          

                var homePage = new HomePage(ChromeDriverShared.Driver);
                homePage.NavigateTo();

                homePage.ClickfrequentlyAskedQuestionsLink();


                ReadOnlyCollection<string> allTabs = ChromeDriverShared.Driver.WindowHandles;
                string homePageTab = allTabs[0];
                string frequentlyQuestionsTab = allTabs[1];

                ChromeDriverShared.Driver.SwitchTo().Window(frequentlyQuestionsTab);

                TestHelper.Pause(200);

                Assert.Equal("https://ankeforsikring.dk/Sider/faq.aspx", ChromeDriverShared.Driver.Url);
            
        }

        [Fact]
        [Trait("Category", "smoke")]
        public void ManipulatinBrowserWindowSize()
        {
            

                var homePage = new HomePage(ChromeDriverShared.Driver);
                homePage.NavigateTo();
                TestHelper.Pause(2000);

                homePage.MaximizeBrowserWindow();
                TestHelper.Pause(2000);
                ChromeDriverShared.Driver.Manage().Window.Minimize();
                TestHelper.Pause(2000);
                ChromeDriverShared.Driver.Manage().Window.Size = new System.Drawing.Size(300, 400);
                TestHelper.Pause(2000);
                ChromeDriverShared.Driver.Manage().Window.Position = new System.Drawing.Point(0, 0);
                TestHelper.Pause(2000);
                ChromeDriverShared.Driver.Manage().Window.Position = new System.Drawing.Point(50, 50);
                TestHelper.Pause(2000);
                ChromeDriverShared.Driver.Manage().Window.Position = new System.Drawing.Point(100, 100);
                TestHelper.Pause(2000);
                ChromeDriverShared.Driver.Manage().Window.FullScreen();
                TestHelper.Pause(2000);

                Assert.Equal(HomeTitle, ChromeDriverShared.Driver.Title);
                Assert.Equal(HomeUrl, ChromeDriverShared.Driver.Url);
            
        }


        [Fact]
        [Trait("Category", "smoke")]
        [UseReporter(typeof(BeyondCompareReporter))]
        public void ScreenShotHomePage()
        {

                ChromeDriverShared.Driver.Navigate().GoToUrl(PrivateUrl);
                ChromeDriverShared.Driver.Manage().Window.Maximize();

                ITakesScreenshot screenShotDriver = (ITakesScreenshot)ChromeDriverShared.Driver;
                Screenshot screenshot = screenShotDriver.GetScreenshot();

                TestHelper.Pause(2000);
                screenshot.SaveAsFile("privacy.bmp", ScreenshotImageFormat.Bmp);

                FileInfo file = new FileInfo("privacy.bmp");
                Approvals.Verify(file);

           
        }

    }
}
