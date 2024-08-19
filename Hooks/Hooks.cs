using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCAmazonSpecflowBDDFramework.Hooks
{
    [Binding]
    public class Hooks       
    {
        private readonly ScenarioContext scenarioContext;
        private static IWebDriver driver;//webdriver reference variable

        public Hooks(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public static void InitializeWebDriver(ScenarioContext scenarioContext)
        {
            driver=new ChromeDriver();//initialize webdriver instance
            // Ensure ScenarioContext is not null before using it
            if (scenarioContext == null)
            {
                throw new NullReferenceException("ScenarioContext is not initialized.");
            }

            scenarioContext["WebDriver"]= driver;
            driver.Manage().Window.Maximize();
            
        }

 /*       [AfterScenario]
        public static void CloseBrowser()
        {
            driver.Quit();
        }*/

        //property to get webdriver instance in step definitions
        public static IWebDriver Driver
        {
            get { return driver; } 
        }
    }
}
