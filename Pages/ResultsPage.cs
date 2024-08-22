using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCAmazonSpecflowBDDFramework.Pages
{
    public class ResultsPage
    {
        private readonly IWebDriver driver;

        private IWebElement lenovo => driver.FindElement(By.XPath("(//i[@class='a-icon a-icon-checkbox'])[1]"));
        //private By minPrice = By.XPath("//input[@id='p_36/range-slider_slider-item_lower-bound-slider']");
        //private By maxPrice = By.XPath("//input[@id='p_36/range-slider_slider-item_upper-bound-slider']");

        public ResultsPage(IWebDriver driver) 
        {
            this.driver=driver;
        }

        //click Lenovo checkbox
        public void ClickLenovo()
        {
            lenovo.Click();
        }

    }
}
