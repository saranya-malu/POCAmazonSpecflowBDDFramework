using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCAmazonSpecflowBDDFramework.Pages;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace POCAmazonSpecflowBDDFramework.StepDefinitions
{
    [Binding]
    public class HomePageStepDefinitions
    {
        private readonly HomePage homepage;
        private readonly IWebDriver driver;
        private ResultsPage resultPage;
        private List<Laptop> allLaptops;

        //private ScenarioContext scenarioContext;
        public HomePageStepDefinitions(ScenarioContext scenarioContext)
        {
            var driver = (IWebDriver)scenarioContext["WebDriver"];
            if (driver == null)
            {
                throw new NullReferenceException("WebDriver is not initialized.");
            }

            homepage = new HomePage(driver);
        }

        [Given(@"I navigate to home page")]
        public void WhenINavigateToHomePage()
        {
            homepage.NavigateToHomePage();// Call the method to navigate to the home page
        }

        [Then(@"I should see the page Title")]
        public void ThenIShouldSeeThePageTitle()// Check if the page title is as expected
        {
            string actualTitle = homepage.GetPageTitle();
            string expectedTitle = "Online Shopping site in India: Shop Online for Mobiles, Books, Watches, Shoes and More - Amazon.in";
            Assert.AreEqual(expectedTitle, actualTitle,"Page title is incorrect");
        }

        [When(@"I search for '([^']*)'")]
        public void WhenISearchFor(string laptop)
        {
            homepage.SearchLaptop(laptop);
            
        }

        [Then(@"I click on '([^']*)' option")]
        public void ThenIClickOnOption(string searchItem)
        {
            resultPage=homepage.ClickAutoSuggestList(searchItem);
            //homepage.ClickAutoSuggestList(searchItem);
        }

        [When(@"I select brand name '([^']*)'")]
        public void WhenISelectBrandName(string lenovo)
        {
            resultPage.ClickLenovo();
        }

        [Then(@"I click on Add to cart button")]
        public void ThenIClickOnAddToCartButton()
        {
            resultPage.ClickAddtoCart();
        }

        [Then(@"I click on cart button")]
        public void ThenIClickOnCartButton()
        {
            resultPage.ClickMoveToCart();
        }

        [Then(@"I click on Payment method button")]
        public void ThenIClickOnPaymentMethodButton()
        {
            resultPage.ClickOnPay();
        }

        [When(@"I enter username and password")]
        public void WhenIEnterUsernameAndPassword()
        {
            resultPage.EnterUserName("7034652730");
            resultPage.ClickContinue();
            resultPage.EnterPassword("Auto@2255");
            resultPage.SignIn();

        }

        [Then(@"I choose delivery address")]
        public void ThenIChooseDeliveryAddress()
        {
            resultPage.ClickOnUseThisAddress();
        }

        [Then(@"I select a payment method")]
        public void ThenISelectAPaymentMethod()
        {
            resultPage.ScrollDown();
            resultPage.ClickOnPaymentMethod();
        }

        [When(@"I select price range")]
        public void WhenISelectPriceRange()
        {
            resultPage.PriceRange(100,200);
           // int leftOffset = CalculateLeftSliderOffset(minprice);
        }

        [Then(@"I click on next button till (.*)th page")]
        public void ThenIClickOnNextButtonTillThPage(int pageno)
        {
            int currentPage = 1;
            allLaptops.AddRange(resultPage.GetLaptops());
            while (currentPage <= pageno)
            {
                resultPage.GoToNextPage();
                currentPage++;
                break;
            }
        }

        [Then(@"I identify (.*) laptops based on reviews and offers")]
        public void ThenIIdentifyLaptopsBasedOnReviewsAndOffers(int number)
        {
            Thread.Sleep(5000);
            var topLaptops = allLaptops.OrderByDescending(I => I.Reviews) //sorting by reviews in descending order
                .ThenByDescending(I => I.Offers) //sorting by offers in descending order if reviews are the same
                .Take(number) //Taking the top number of laptops
                .ToList();

            foreach (var laptop in topLaptops)
            {
                Console.WriteLine("Laptop: { laptop.Title}, Reviews: { laptop.Reviews}, Offers: { laptop.Offers}");
            }
        }
    }
}
