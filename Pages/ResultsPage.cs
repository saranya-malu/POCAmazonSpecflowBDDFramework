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
        private IJavaScriptExecutor _jsExecutor;
        private Actions actions;

        private IWebElement lenovo => driver.FindElement(By.XPath("//span[text()='Lenovo']"));
        private IWebElement minPrice => driver.FindElement(By.XPath("//input[@id='p_36/range-slider_slider-item_lower-bound-slider']"));
        private IWebElement maxPrice => driver.FindElement(By.XPath("//input[@id='p_36/range-slider_slider-item_upper-bound-slider']"));

        private IWebElement goButton => driver.FindElement(By.XPath("//input[@class='a-button-input' and @type='submit']"));
        private IWebElement addtoCart => driver.FindElement(By.XPath("//button[@id='a-autoid-1-announce']"));

        private IWebElement moveToCart => driver.FindElement(By.XPath("//span[@id='nav-cart-count']"));
        private IWebElement proceedtoPay => driver.FindElement(By.XPath("//input[@value='Proceed to checkout']"));

        private IWebElement userName => driver.FindElement(By.XPath("//input[@type='email']"));

        private IWebElement password => driver.FindElement(By.XPath("//input[@type='password']"));

        private IWebElement continueButton => driver.FindElement(By.XPath("//span[@id='continue']"));

        private IWebElement signInButton => driver.FindElement(By.XPath("//input[@id='signInSubmit']"));

        private IWebElement deliveryAddress => driver.FindElement(By.XPath("//span[@id='shipToThisAddressButton']"));

        private IWebElement paymentOption => driver.FindElement(By.XPath("//input[@value='SelectableAddCreditCard']"));

        private IWebElement nextButton => driver.FindElement(By.XPath("//a[@class='s-pagination-item s-pagination-next s-pagination-button s-pagination-separator']"));

        private By laptopLocators => By.XPath("//span[@class='rush-component s-latency-cf-section']");
        public ResultsPage(IWebDriver driver)
        {
            this.driver = driver;
            //initialize javascript executor
            _jsExecutor = (IJavaScriptExecutor)driver;//cast the webdriver to javascript executor
            actions = new Actions(driver);
        }

        //click Lenovo checkbox
        public void ClickLenovo()
        {
            lenovo.Click();
        }

        //Click to add to cart 
        public void ClickAddtoCart()
        {
            Thread.Sleep(5000);
            addtoCart.Click();
        }

        //Click move to cart
        public void ClickMoveToCart()
        {
            Thread.Sleep(5000);
            moveToCart.Click();
        }

        //Click to payemnt method
        public void ClickOnPay()
        {
            proceedtoPay.Click();
        }

        //Enter username
        public void EnterUserName(string email)
        {
            Thread.Sleep(5000);
            userName.SendKeys(email);
        }

        //Click continue
        public void ClickContinue()
        {
            continueButton.Click();
        }

        //Enter password
        public void EnterPassword(string pword)
        {
            Thread.Sleep(5000);
            password.SendKeys(pword);
        }

        //Click SignIn
        public void SignIn()
        {
            signInButton.Click();
        }

        //Click on delivery address
        public void ClickOnUseThisAddress()
        {
            deliveryAddress.Click();
        }

        //Click on payment method
        public void ClickOnPaymentMethod()
        {
            paymentOption.Click();

        }

        //scroll down
        public void ScrollDown()
        {
            Thread.Sleep(5000);
            int scroll = 2000;
            _jsExecutor.ExecuteScript("window.scrollBy(0,{scroll});");
        }

        public void PriceRange(int leftOffset,int rightOffset)
        {
            //    _jsExecutor.ExecuteScript("document.getElementById('p_36/range-slider_slider-item_lower-bound-slider').value='50000'");
            //    _jsExecutor.ExecuteScript("document.getElementById('p_36/range-slider_slider-item_upper-bound-slider').value='100000'");
            //    _jsExecutor.ExecuteScript("document.querySelector('input.a-button-input').click();");
            actions.ClickAndHold(minPrice).MoveByOffset(leftOffset,0).Release().Perform();
            actions.ClickAndHold(maxPrice).MoveByOffset(rightOffset,0).Release().Perform();
            
        }

        public List<Laptop> GetLaptops()
        {
            var laptopElements = driver.FindElements(laptopLocators);
            List<Laptop> laptops = new List<Laptop>();

            foreach (var laptopElement in laptopElements)
            {
                try
                {
                    string title = laptopElement.FindElement(By.CssSelector("span.a-size-medium a-color-base a-text-normal")).Text;
                    //int reviews = int.Parse(laptopElement.FindElement(By.CssSelector("a.a-popover-trigger a-declarative")).Text.Split(' ')[0].Replace(",", ""));
                    int reviews = int.Parse(laptopElement.FindElement(By.CssSelector("a.a-popover-trigger a-declarative")).Text);
                    //int offers = int.Parse(laptopElement.FindElement(By.CssSelector("span.a-color-secondary span.a-size-base")).Text.Replace(" offers", ""));
                    int offers = int.Parse(laptopElement.FindElement(By.PartialLinkText("off")).Text);

                    laptops.Add(new Laptop { Title = title, Reviews = reviews, Offers = offers });
                }
                catch
                {
                    Console.WriteLine("Failed");
                }

            }

            return laptops;
        }

        // Method to go to the next page
        public void GoToNextPage()
        {
            nextButton.Click();
        }
    }

        // Laptop class to hold details
        public class Laptop
        {
            public string Title { get; set; }
            public int Reviews { get; set; }
            public int Offers { get; set; }
        }

}
