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
        //public readonly WebDriverWait wait;


        public HomePage(IWebDriver driver)//parametrized constructor
        {
            this.driver = driver;
        }

        private IWebElement searchBox => driver.FindElement(By.XPath("//input[@id='twotabsearchtextbox']"));

        private IReadOnlyCollection<IWebElement> AutoSuggestList => driver.FindElements(By.XPath("//div[@class='s-suggestion s-suggestion-ellipsis-direction']"));

        private IWebElement addtoCart => driver.FindElement(By.XPath("//button[@id='a-autoid-3-announce']"));
        
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
           /* var suggestion = AutoSuggestList.FirstOrDefault(s => s.Text.Contains(searchTerm));
            if (suggestion != null)
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                suggestion.Click();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
            else
            {
                throw new NoSuchElementException($"No autosuggestion found for {searchTerm}");
            }*/
        }

        public void ClickAddtoCart()
        {

        }
    }
}
