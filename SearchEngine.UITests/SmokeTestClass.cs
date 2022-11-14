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

namespace SearchEngine.UITests
{
    public class SmokeTestClass
    {
        private const string HomeUrl = "https://localhost:7017/";
        private const string PrivateUrl = "https://localhost:7017/Home/Privacy";
        private const string HomeTitle = "- WebApplication1";

        [Fact]
        [Trait("Category","smoke")]
        public void LoadApplicationPage() 
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

                driver.Navigate().GoToUrl(HomeUrl);

                //Den her løsning bruger Hjælper klassen - TestHelper - Det er ikke den bedste måde at sætte en pause på ved at bruge thread sleep.
                //TestHelper.Pause(100);// Giver random number tid til at genererer et nummer, ellers bliver værdien sat til - ""
                //IWebElement randomNumber = driver.FindElement(By.Id("readomNumberToTestOn"));
                //string initialToken = randomNumber.Text;

                // Den her løsning bruger WebDriverWait som vi får til rådighed OpenQA.Selenium.Support.UI; - Dette er en mere korekt måde at sætte et delay på
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(10));
                IWebElement randomNumber = wait.Until((d) => d.FindElement(By.Id("readomNumberToTestOn")));
                string initialToken = randomNumber.Text;

                TestHelper.Pause();
                driver.Navigate().GoToUrl(PrivateUrl);
                //TestHelper.Pause();
                driver.Navigate().Back();
                //TestHelper.Pause();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);

                string reloadedToken = driver.FindElement(By.Id("readomNumberToTestOn")).Text;
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

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(10));
                IWebElement randomNumber = wait.Until((d) => d.FindElement(By.Id("readomNumberToTestOn")));
                string initialToken = randomNumber.Text;

                driver.Navigate().Back();
                //TestHelper.Pause();

                driver.Navigate().Forward();
                TestHelper.Pause(200);// Giver random number tid til at genererer et nummer, ellers bliver værdien sat til - ""


                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);

                string reloadedToken = driver.FindElement(By.Id("readomNumberToTestOn")).Text;
                Assert.NotEqual(initialToken, reloadedToken);

            }
        }

        [Fact]
        [Trait("Category", "smoke")]
        public void OpenfrequentlyAskedQuestions()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl(HomeUrl);
                TestHelper.Pause(2000);

                driver.FindElement(By.Name("frequentlyAskedQuestions")).Click();
                TestHelper.Pause(2000);

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
