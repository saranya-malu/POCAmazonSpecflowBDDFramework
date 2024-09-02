using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
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
        public static bool isInitialized=false;
        public Hooks(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public static void InitializeWebDriver(ScenarioContext scenarioContext)
        {
            string browserType = Environment.GetEnvironmentVariable("BROWSER_TYPE")?.ToLower() ??"chrome";
            switch(browserType)
            {
                case "chrome":
                    driver = new ChromeDriver();//initialize webdriver instance
                    break;
                case "edge":
                    driver = new EdgeDriver();
                    break;
                throw new NotFoundException("Browser type '{browserType}' is not supported.");
            }
            
            // Ensure ScenarioContext is not null before using it
            if (scenarioContext == null)
            {
                throw new NullReferenceException("ScenarioContext is not initialized.");
            }

            scenarioContext["WebDriver"]= driver;
            driver.Manage().Window.Maximize(); 
            driver.Manage().Cookies.DeleteAllCookies();
        }

/*             [AfterScenario]
               public static void CloseBrowser()
               {
                   driver.Quit();
                    driver=null;
               }
*/
        //property to get webdriver instance in step definitions
        public static IWebDriver Driver
        {
            get { return driver; } 
        }
    }
}
