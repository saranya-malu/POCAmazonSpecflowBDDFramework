using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCAmazonSpecflowBDDFramework.Pages
{
    public class BaseClass
    {
        private readonly IWebDriver driver;//webdriver instance
        private readonly String pageurl = "https://www.amazon.in/";//homepage url
        //public readonly WebDriverWait wait;


        public BaseClass(IWebDriver driver)//parametrized constructor
        {
            this.driver = driver;
        }

        private IWebElement searchBox => driver.FindElement(By.XPath("//input[@id='twotabsearchtextbox']"));

        private IReadOnlyCollection<IWebElement> AutoSuggestList => driver.FindElements(By.XPath("//div[@class='s-suggestion s-suggestion-ellipsis-direction']"));

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

        public void ClickAutoSuggestList(string searchTerm)
        {
            /*  foreach (var item in AutoSuggestList)
              {
                  if (item.Text == suggestionText)
                  {

                      //WebDriverWait wait =new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                      //Thread.Sleep(10000);
                      item.Click();
                      Thread.Sleep(10000);
                      break;
                  }
              }*/
            var suggestion = AutoSuggestList.FirstOrDefault(s => s.Text.Contains(searchTerm));
            if (suggestion != null)
            {
                suggestion.Click();
            }
            else
            {
                throw new NoSuchElementException($"No autosuggestion found for {searchTerm}");
            }
        }
    }
}
