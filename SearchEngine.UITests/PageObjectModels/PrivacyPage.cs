using OpenQA.Selenium;
using System;


namespace SearchEngine.UITests.PageObjectModels 
{
    class PrivacyPage : BasePage
    {
        //private readonly IWebDriver Driver;
        protected override string PageUrl => "https://localhost:7017/Home/Privacy/";
        protected override string PageTitle => "Privacy Policy - WebApplication1";


        public PrivacyPage(IWebDriver driver)
        {
            Driver = driver;
        }



    }
}
