using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCAmazonSpecflowBDDFramework.Pages;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using POCAmazonSpecflowBDDFramework.Helpers;
using System.Data;
using TechTalk.SpecFlow.Assist;
using System.Text.Json.Nodes;

namespace POCAmazonSpecflowBDDFramework.StepDefinitions
{
    [Binding]
    public class HomePageStepDefinitions
    {
        private readonly HomePage homepage;
        private readonly IWebDriver driver;
        private ResultsPage resultPage;


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
        }

        [When(@"I select brand name '([^']*)'")]
        public void WhenISelectBrandName(string lenovo)
        {
            resultPage.ClickLenovo();
        }

        [When(@"I select brand name")]
        public void WhenISelectBrandName(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            resultPage.SelectBrandNameAsFilter(data.Brand);
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

        [Then(@"I select price range in filter")]
        public void WhenISelectPriceRangeInFilter(Table table)
        {
            var dataTable=SpecflowTableHelper.ToDataTable(table);
            foreach (DataRow row in dataTable.Rows)
            {
                resultPage.SetPriceRangeFilter(int.Parse(row.ItemArray[0].ToString()), int.Parse(row.ItemArray[1].ToString()));
            }
        }

        [Then(@"I navigate till last page of search results")]
        public void ThenINavigateTillLastPageOfSearchResults()
        {
          resultPage.NextButtonNavigation();
            JsonArray LaptopDetails = resultPage.GetLaptopDetails();
            JsonArray Top3Laptops = resultPage.GetTop3Laptops(LaptopDetails);
            Console.WriteLine(LaptopDetails.ToString());
            resultPage.WriteJsonArrayToExcel(Top3Laptops);
        }
    }
}
