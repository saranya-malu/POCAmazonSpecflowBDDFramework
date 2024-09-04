using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCAmazonSpecflowBDDFramework.Pages
{
    public class HomePage
    {
        private readonly IWebDriver driver;//webdriver instance
        private readonly String pageurl = "https://www.amazon.in/";//homepage url


        public HomePage(IWebDriver driver)//parametrized constructor
        {
            this.driver = driver;
        }

        private IWebElement searchBox => driver.FindElement(By.XPath("//input[@id='twotabsearchtextbox']"));
        private IReadOnlyCollection<IWebElement> AutoSuggestList => driver.FindElements(By.XPath("//div[contains(@class,'results-container')]//div[contains(@class,'suggestion-container')]//div[@role='button']"));
        
        //navigate to homepage
        public void NavigateToHomePage()//method to navigateto homepage
        {
            driver.Navigate().GoToUrl(pageurl);
        }

        //Verify page title
        public string GetPageTitle()
        {
            return driver.Title;
        }

        //Search for laptop
        public void SearchLaptop(string searchItem)
        {
            searchBox.SendKeys(searchItem);
        }

        //Click on auto suggestion list option
        public ResultsPage ClickAutoSuggestList(string suggestionText)
        {

            Thread.Sleep(5000);

            //    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            foreach (var item in AutoSuggestList)
              {
                  if (item.Text == suggestionText)
                  {

                      WebDriverWait wait =new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                      item.Click();
                      return new ResultsPage(driver);//redirected to search results page
                  }
              }
            return null;
        }
    }
}
