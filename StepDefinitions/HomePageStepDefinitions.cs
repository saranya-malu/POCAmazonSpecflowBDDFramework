﻿using OpenQA.Selenium;
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

        [Then(@"I click on Add to cart button")]
        public void ThenIClickOnAddToCartButton()
        {
            throw new PendingStepException();
        }
    }
}
