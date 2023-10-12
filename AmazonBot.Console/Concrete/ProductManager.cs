using AmazonBot.Console.Abstract;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonBot.Console.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IWebDriver _driver;

        string url = "https://www.amazon.com.tr/s?k=dogo";
        public ProductManager(IWebDriver webDriver)
        {
            _driver = webDriver;
        }
        public  List<Product> GetAllProducts()
        {
            List <Product> allProducts=new List<Product>();

            string source = IsPageFullyLoaded(url, _driver);
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = ConvertToHtmlDocument(source);
           
            if (doc != null)
            {
                HtmlNode parentsInfos = doc.DocumentNode.SelectSingleNode(".//div[@class='s-main-slot s-result-list s-search-results sg-row']");
                if (parentsInfos != null)
                {

                    List<HtmlNode> childrens = parentsInfos.SelectNodes(".//div[@class='sg-col-4-of-24 sg-col-4-of-12 s-result-item s-asin sg-col-4-of-16 sg-col s-widget-spacing-small sg-col-4-of-20']").ToList();

                    if (childrens!=null)
                    {
                       
                        foreach (var product in childrens)
                        {
                            Product new_product = new Product();
                            HtmlNode baseproduct = product.SelectSingleNode(".//div[@class='a-section a-spacing-small puis-padding-left-micro puis-padding-right-micro']");
                          
                            //Product Name
                            HtmlNode productTextSideName = baseproduct.ChildNodes[1];
                            var pName = productTextSideName.InnerText;
                            new_product.ProductName = pName;

                            allProducts.Add(new_product);
                     
                    
                        }
                    }
                }
            }


             return  allProducts.ToList();
        }

  
        public static string IsPageFullyLoaded(string url, IWebDriver driver)
        {
            driver.Url = url;
            // Wait for the page to complete loading
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
            // Optionally, you can wait for specific elements or conditions to be present/visible
            // Add your custom wait conditions here if needed
            string pageSource = driver.PageSource.ToString();

            return pageSource;
        }

        public static HtmlDocument ConvertToHtmlDocument(string htmlString)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlString);
            return htmlDoc;
        }

    }
}
