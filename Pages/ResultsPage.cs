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

        private IWebElement lenovo => driver.FindElement(By.XPath("//span[text()='Lenovo']"));
        //private By minPrice = By.XPath("//input[@id='p_36/range-slider_slider-item_lower-bound-slider']");
        //private By maxPrice = By.XPath("//input[@id='p_36/range-slider_slider-item_upper-bound-slider']");
        private IWebElement addtoCart => driver.FindElement(By.XPath("//button[@id='a-autoid-1-announce']"));

        private IWebElement moveToCart => driver.FindElement(By.XPath("//span[@id='nav-cart-count']"));
        private IWebElement proceedtoPay => driver.FindElement(By.XPath("//input[@value='Proceed to checkout']"));

        private IWebElement userName => driver.FindElement(By.XPath("//input[@id='ap_email_login']"));

        private IWebElement password => driver.FindElement(By.XPath("//input[@id='ap_password']"));

        private IWebElement continueButton => driver.FindElement(By.XPath("//span[@id='continue']"));
        
        private IWebElement signInButton => driver.FindElement(By.XPath("//input[@id='signInSubmit']"));

        private IWebElement deliveryAddress => driver.FindElement(By.XPath("//span[@id='shipToThisAddressButton']"));

        private IWebElement paymentOption => driver.FindElement(By.XPath("//input[@value='SelectableAddCreditCard']"));
        
        public ResultsPage(IWebDriver driver) 
        {
            this.driver=driver;
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

    }
}
