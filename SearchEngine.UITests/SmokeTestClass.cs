using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


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
                TestHelper.Pause(100);
                IWebElement randomNumber = driver.FindElement(By.Id("readomNumberToTestOn"));
                string initialToken = randomNumber.Text;
                TestHelper.Pause();
                Console.WriteLine(initialToken);
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
                TestHelper.Pause(100);
                IWebElement randomNumber = driver.FindElement(By.Id("readomNumberToTestOn"));
                string initialToken = randomNumber.Text;
                driver.Navigate().Back();
                //TestHelper.Pause();

                driver.Navigate().Forward();
                TestHelper.Pause(100);

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);

                string reloadedToken = driver.FindElement(By.Id("readomNumberToTestOn")).Text;
                Assert.NotEqual(initialToken, reloadedToken);

            }
        }

    }
}
