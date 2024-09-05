using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using POCAmazonSpecflowBDDFramework.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace POCAmazonSpecflowBDDFramework.Pages
{
    public class ResultsPage
    {
        private readonly IWebDriver driver;
        private IJavaScriptExecutor _jsExecutor;
        private Actions actions;

        private IWebElement lenovo => driver.FindElement(By.XPath("//span[text()='Lenovo']"));

        private IWebElement goButton => driver.FindElement(By.XPath("//input[@class='a-button-input' and @type='submit']"));
        private IWebElement addtoCart => driver.FindElement(By.XPath("(//button[@class='a-button-text'])[1]"));

        private IWebElement moveToCart => driver.FindElement(By.XPath("//span[@id='nav-cart-count']"));
        private IWebElement proceedtoPay => driver.FindElement(By.XPath("//input[@value='Proceed to checkout']"));

        private IWebElement userName => driver.FindElement(By.XPath("//input[@type='email']"));

        private IWebElement password => driver.FindElement(By.XPath("//input[@type='password']"));

        private IWebElement continueButton => driver.FindElement(By.XPath("//span[@id='continue']"));

        private IWebElement signInButton => driver.FindElement(By.XPath("//input[@id='signInSubmit']"));

        private IWebElement deliveryAddress => driver.FindElement(By.XPath("//span[@id='shipToThisAddressButton']"));

        private IWebElement paymentOption => driver.FindElement(By.XPath("//input[@value='SelectableAddCreditCard']"));

        private IWebElement priceRangeUpperBound => driver.FindElement(By.XPath("//input[contains(@id,'slider-item_upper-bound-slider')]"));

        private IWebElement priceRangeLowerBound => driver.FindElement(By.XPath("//input[contains(@id,'slider-item_lower-bound-slider')]"));

        IReadOnlyCollection<IWebElement> filterBrandNameList => driver.FindElements(By.XPath("//div[@id='brandsRefinements']//ul[@class='a-unordered-list a-nostyle a-vertical a-spacing-medium']//span[@class='a-list-item']/a//span[@class='a-size-base a-color-base']"));

        private readonly string _nextButton = "//span[@class='s-pagination-strip']//*[contains(@class,'s-pagination-next')]";
        private IWebElement NextButton => driver.FindElement(By.XPath(_nextButton));

        private readonly string _resultRow = "//div[@class='a-section']//div[@class='a-section a-spacing-small a-spacing-top-small']";


        private readonly string _productTitle = "//div[@class='a-section']//div[@class='a-section a-spacing-small a-spacing-top-small']//div[@data-cy='title-recipe']//span";


        private readonly string _ratingElement = "//div[@class='a-section']//div[@class='a-section a-spacing-small a-spacing-top-small']//div[@data-cy='title-recipe']//span[contains(text(),'{0}')]/parent::a/parent::h2/parent::div/following-sibling::div[@data-cy='reviews-block']//span[contains(@aria-label,'stars')]";


        private readonly string _ItemOffer = "//div[@class='a-section']//div[@class='a-section a-spacing-small a-spacing-top-small']//div[@data-cy='title-recipe']//span[contains(text(),'{0}')]/parent::a/parent::h2/parent::div/following-sibling::div[@class='puisg-row']//div[@data-cy='price-recipe']//span[contains(text(),'%')]";

        private readonly string _paginationButtons = "//span[@class='s-pagination-strip']//a[@class='s-pagination-item s-pagination-button']";

        private readonly string _currentPage = "//span[@class='s-pagination-strip']//span[contains(@aria-label,'Current page')]";

        private IWebElement CurrentPage => driver.FindElement(By.XPath(_currentPage));
        IReadOnlyCollection<IWebElement> PaginationButtons => driver.FindElements(By.XPath(_paginationButtons));

        IReadOnlyCollection<IWebElement> Pagination => driver.FindElements(By.CssSelector("span.s-pagination-strip"));
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

        public void SelectBrandNameAsFilter(String BrandName)
        {
            foreach (IWebElement element in filterBrandNameList)
            {
                String Brand = element.Text;
                if (Brand.Equals(BrandName))
                {
                    element.Click();
                    break;
                }
            }
        }

        //Extract numeric value
        public int ExtractNumericValue(string priceText)
        {
            string numericText = new string(priceText.Where(c => char.IsDigit(c)).ToArray());//convert enumerable characters to string
            return int.Parse(numericText); //convert thestring to integer
        }

        // Extract the minimum and maximum prices directly from the slider attributes
        public int CalculateSliderValue(int price, int maxSliderValue)
        {
            int minPrice = ExtractNumericValue(priceRangeUpperBound.GetAttribute("aria-valuetext"));//calling Extract method to get value from upper range after conversion
            int maxPrice = ExtractNumericValue(priceRangeLowerBound.GetAttribute("aria-valuetext"));//calling Extract method to get value from lower range after conversion

            // Clamp the input price within the slider’s range
            if (price < minPrice) price = minPrice;
            if (price > maxPrice) price = maxPrice;

            // Calculate the slider value proportionally
            double proportion = (double)(price - minPrice) / (maxPrice - minPrice);
            return (int)(Math.Round(proportion * maxSliderValue));
        }

        //Move slider
        private void SetSliderValueWithJavaScript(IWebElement slider, int value)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].value = arguments[1]; arguments[0].dispatchEvent(new Event('input')); arguments[0].dispatchEvent(new Event('change'));", slider, value);
        }

        //Setprice range to sliders
        public void SetPriceRangeFilter(int lowerPrice, int upperPrice)
        {
            // Calculate slider values based on the desired price range
            int maxSliderValue = int.Parse(priceRangeUpperBound.GetAttribute("max"));
            int lowerValue = CalculateSliderValue(lowerPrice, maxSliderValue);
            int upperValue = CalculateSliderValue(upperPrice, maxSliderValue);

            // Ensure lowerValue is not greater than upperValue
            if (lowerValue > upperValue)
            {
                lowerValue = upperValue - 1;  // Adjust to avoid overlap or invalid state
            }

            // Set the value attribute directly for the lower and upper bounds
            SetSliderValueWithJavaScript(priceRangeLowerBound, lowerValue);
            Thread.Sleep(1000);  // Small delay to ensure the UI updates
            SetSliderValueWithJavaScript(priceRangeUpperBound, upperValue);

            // Click the Go button to apply the filter

            goButton.Click();
        }

        //Click next button and navigate to 5 or less pages
        public void NextButtonNavigation()
        {
            int pageCount = PaginationButtons.Count();
            int currentPageNo = 1;
            int totalPageLimit = 5;
            int totalCount = pageCount + currentPageNo;

            if (IsPageStripPresent() != false)
            {
                do
                {
                    // Get laptop details from the current page
                    JsonArray laptopDetails = GetLaptopDetails();

                    // Get top 3 laptops from the current page
                    JsonArray top3Laptops = GetTop3Laptops(laptopDetails);

                    // Write the top 3 laptops to Excel, specifying the sheet name as the current page number
                    WriteJsonArrayToExcel(top3Laptops);

                    if (IsNextButtonDisabled())// Check if the next button is disabled (i.e., no more pages to navigate)
                    {
                        break;
                    }


                    NextButton.Click();// Click the next button and navigate to the next page
                    currentPageNo++;
                }
                while (currentPageNo <= totalPageLimit);// Limit to 5 pages or less                                     // Write all collected data to Excel after the loop
            }
        }

        // Check if next button is disabled
        public bool IsNextButtonDisabled()
        {
            return NextButton.GetAttribute("class").Contains("pagination-disabled");
        }

        //Check pagestrip ispresent
        public bool IsPageStripPresent()
        {
            try
            {
                return Pagination.Count > 0;
            }
            catch
            {
                return false;
            }
        }

        //Get sorted laptop details
        public JsonArray GetLaptopDetails()
        {
            List<IWebElement> products = GetProductsTitle();//fetches title of all prodcuts
            var laptopDetailsList = new List<LaptopDetails>();

            int productCount = Math.Min(products.Count, 4); // Adjust based on available products, max 4

            for (int i = 0; i < productCount; i++)
            {
                string laptopName = products[i].Text;
                int keywordIndex = laptopName.IndexOf("Laptop");
                if (keywordIndex != -1)
                {
                    laptopName = laptopName.Substring(0, keywordIndex + "Laptop".Length).Trim();//If the word "Laptop" is present in the name, it trims the name to include only the part up to "Laptop."
                }

                double rating = 0.0;
                int offer = 0;
                List<IWebElement> productRatingElements = GetProductsRating(laptopName);//fetch product rating
                List<IWebElement> productOfferElements = GetProductsOffer(laptopName);//fetch product offers

                if (productRatingElements.Count > 0 && productOfferElements.Count > 0)
                {
                    string ratingText = productRatingElements[0].GetAttribute("aria-label");//Extracts and parses the rating from the aria-label attribute of the product rating element
                    var Ratingmatch = Regex.Match(ratingText, @"(\d+(\.\d+)?)");
                    if (Ratingmatch.Success)
                    {
                        double parsedRating;
                        if (double.TryParse(Ratingmatch.Value, out parsedRating))
                        {
                            rating = parsedRating;
                        }
                    }

                    string offerText = productOfferElements[0].Text;//Extracts the percentage of the offer using a regex match
                    var Offermatch = Regex.Match(offerText, @"(\d+)% off");
                    if (Offermatch.Success)
                    {
                        offer = int.Parse(Offermatch.Groups[1].Value);
                    }
                }

                laptopDetailsList.Add(new LaptopDetails
                {
                    LaptopName = laptopName,
                    Rating = rating,
                    Offer = offer
                });
            }


            var jsonArray = new JsonArray();
            foreach (var detail in laptopDetailsList)
            {
                var jsonObject = new JsonObject
                {
                    ["LaptopName"] = detail.LaptopName,
                    ["Rating"] = detail.Rating,
                    ["Offer"] = detail.Offer
                };
                jsonArray.Add(jsonObject);
            }

            return jsonArray;
        }

        //get all product titles
        public List<IWebElement> GetProductsTitle()
        {
            return driver.FindElements(By.XPath(_productTitle)).ToList();//return product title
        }

        //get rating of laptops
        public List<IWebElement> GetProductsRating(String laptopName)
        {
            return driver.FindElements(By.XPath(String.Format(_ratingElement, laptopName))).ToList();//getting laptop ratings using laptop name
        }

        //get offers of laptop
        public List<IWebElement> GetProductsOffer(String laptopName)
        {
            return driver.FindElements(By.XPath(String.Format(_ItemOffer, laptopName))).ToList();//getting laptop offers using laptop names
        }

        //Writing top 3 laptop details to excel
        public void WriteJsonArrayToExcel(JsonArray jsonArray)
        {
            // Get the base directory and project root directory
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine($"Base Directory: {baseDirectory}");

            string projectRootDirectory = System.IO.Path.GetFullPath(System.IO.Path.Combine(baseDirectory, @"..\..\..\..\..\..\"));
            Console.WriteLine($"Project Root Directory: {projectRootDirectory}");

            // Set up folder and file paths
            string folderPath = System.IO.Path.Combine(projectRootDirectory, "LapTopData");
            string filePath = System.IO.Path.Combine(folderPath, "LapTopDetails.xlsx");

            // Create the directory if it doesn't exist
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Create an Excel workbook
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("page1");
                worksheet.Cell(1, 1).Value = "LaptopName";
                worksheet.Cell(1, 2).Value = "Rating";
                worksheet.Cell(1, 3).Value = "Offer";

                int row = 2; // Start from the second row
                foreach (var item in jsonArray)
                {
                    var jsonObject = item.AsObject();
                    worksheet.Cell(row, 1).Value = jsonObject["LaptopName"]?.ToString();
                    worksheet.Cell(row, 2).Value = jsonObject["Rating"]?.ToString();
                    worksheet.Cell(row, 3).Value = jsonObject["Offer"]?.ToString();
                    row++;
                }

                workbook.SaveAs(filePath);
            }
            Console.WriteLine($"Excel file created at: {filePath}");
        }

        //sorts laptops based on rating and offer
        public JsonArray GetTop3Laptops(JsonArray jsonArray)
        {
            var laptopDetailsList = jsonArray.Select(item =>
            {
                var jsonObject = item.AsObject();
                return new LaptopDetails
                {
                    LaptopName = jsonObject["LaptopName"]?.ToString(),
                    Rating = jsonObject["Rating"]?.GetValue<double>() ?? 0.0,
                    Offer = jsonObject["Offer"]?.GetValue<int>() ?? 0
                };
            }).ToList();


            var sortedList = laptopDetailsList
                .OrderByDescending(l => l.Rating)
                .ThenByDescending(l => l.Offer)
                .Take(3)
                .ToList();


            var sortedJsonArray = new JsonArray();
            foreach (var detail in sortedList)
            {
                var jsonObject = new JsonObject
                {
                    ["LaptopName"] = detail.LaptopName,
                    ["Rating"] = detail.Rating,
                    ["Offer"] = detail.Offer
                };
                sortedJsonArray.Add(jsonObject);
            }
            return sortedJsonArray;
        }
    }
}


