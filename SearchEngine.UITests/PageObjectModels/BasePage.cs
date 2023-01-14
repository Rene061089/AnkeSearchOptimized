using OpenQA.Selenium;
using System;

namespace SearchEngine.UITests.PageObjectModels
{
    class BasePage
    {
        protected IWebDriver Driver;
        protected virtual string PageUrl { get; }
        protected virtual string PageTitle { get; }


        public void NavigateTo()
        {
            Driver.Navigate().GoToUrl(PageUrl);
            EnsurePageHasLoaded();
        }


        public void MaximizeBrowserWindow() => Driver.Manage().Window.Maximize();
        public void MinimizeBrowserWindow() => Driver.Manage().Window.Minimize();
        public void SetBrowserToMobile()
        {
            //Driver.SwitchTo().ActiveElement().SendKeys(Keys.F12);
            //Driver.SwitchTo().ActiveElement().SendKeys("Ctrl" + "Shift" + "M");
            //Driver.Manage().Window.Size.  (new Dimension(EmulatedDevices.IPHONE7.getWidth(), EmulatedDevices.IPHONE7.getHeight()));
        }

        public void SetFixedBrowsSize(int x, int y) => Driver.Manage().Window.Size = new System.Drawing.Size(x, y);
        public void SetFixedBrowsPosition(int x, int y) => Driver.Manage().Window.Position = new System.Drawing.Point(x, y);


        /// <summary>
        /// Checks that the URL And page title are as we expect
        /// </summary>
        /// <param name="onlyCheckStartOfTheUrlText"></param> default is true, but if this is set to false it will do an exact match of the url.
        /// <exception cref="Exception"></exception>

        public void EnsurePageHasLoaded(bool onlyCheckStartOfTheUrlText = true)
        {

            bool urlIsCorrect;
            if (onlyCheckStartOfTheUrlText)
            {
                urlIsCorrect = Driver.Url.StartsWith(PageUrl);
            }
            else
            {
                urlIsCorrect = Driver.Url == PageUrl;
            }

            bool ensurePageHasLoaded = urlIsCorrect && (Driver.Title == PageTitle);

            if (!ensurePageHasLoaded)
            {
                throw new Exception($"Falid to load page. Page URL = '{Driver.Url}' Page Source \r\n {Driver.PageSource}");
            }
        }

    }
}
