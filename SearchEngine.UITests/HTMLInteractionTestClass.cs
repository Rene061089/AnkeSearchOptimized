using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SearchEngine.UITests
{
    [Trait("Category", "Applications")]
    public class HTMLInteractionTestClass
    {
        private const string HomeUrl = "https://localhost:7017/";
        private const string PrivateUrl = "https://localhost:7017/Home/Privacy/";

        [Fact]
        public void GoToPrivacyPageByLink()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl(HomeUrl);
                TestHelper.Pause();
                IWebElement privacyLink = driver.FindElement(By.Name("ToPortal"));
                privacyLink.Click();

                TestHelper.Pause();

                Assert.Equal("Privacy Policy - WebApplication1", driver.Title);
                Assert.Equal(PrivateUrl, driver.Url);
            }
        }

        [Fact]
        public void GoToPrivacyPageByLinkText()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl(HomeUrl);
                TestHelper.Pause();
                IWebElement privacyLinkByLinkText = driver.FindElement(By.LinkText("Log på portal"));
                privacyLinkByLinkText.Click();

                TestHelper.Pause();

                Assert.Equal("Privacy Policy - WebApplication1", driver.Title);
                Assert.Equal(PrivateUrl, driver.Url);
            }
        }


    }
}
