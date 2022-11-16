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
