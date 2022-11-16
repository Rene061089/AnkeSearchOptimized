using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace SearchEngine.UITests
{
    public sealed class ChromeDriverShared : IDisposable
    {

        public IWebDriver Driver { get; private set; }

        public ChromeDriverShared()
        {
            Driver = new ChromeDriver();
        }

        public void Dispose()
        {
            Driver.Dispose();
        }
    }
}
