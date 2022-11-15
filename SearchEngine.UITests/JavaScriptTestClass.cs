using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;

namespace SearchEngine.UITests
{
    public class JavaScriptTestClass
    {
        private const string HomeUrl = "https://localhost:7017/";

        [Fact]
        public void ClickOverlayedlink()
        {

            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                TestHelper.Pause();

                //Dette vil ikke virke med et overlay på HTML siden brug istedet Javascript injektion
                //driver.FindElement(By.Id("fogp")).Click();

                string script = "document.getElementById('fogp').click();";
                string scroll = "document.getElementById('fogp').scrollIntoView()";
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                js.ExecuteScript(scroll);
                TestHelper.Pause();
                js.ExecuteScript(script);

                TestHelper.Pause();

                ReadOnlyCollection<string> allTabs = driver.WindowHandles;
                string homePageTab = allTabs[0];
                string fogp = allTabs[1];

                driver.SwitchTo().Window(fogp);
                TestHelper.Pause(1000);

                Assert.Equal("https://www.fogp.dk/", driver.Url);

            }

        }
    }
}
