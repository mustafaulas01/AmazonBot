using AmazonBot.Console.Abstract;
using AmazonBot.Console.Concrete;
using Autofac;
using OpenQA.Selenium.Chrome;

internal class Program
{
    private async static Task   Main(string[] args)
    {
        var containerBuilder = new ContainerBuilder();
        var driver = new ChromeDriver();
        containerBuilder.RegisterInstance<IProductService>(new ProductManager(driver));


        var container = containerBuilder.Build();

        var productService = container.Resolve<IProductService>();

     List<Product> amazonProducts=   productService.GetAllProducts();

        foreach (var product in amazonProducts)
        {

            Console.WriteLine("Product Name : "+product.ProductName);
            Console.WriteLine("-------------------------------------------------------------------------------");

        }

    }
}