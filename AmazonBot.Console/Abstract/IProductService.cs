using AmazonBot.Console.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonBot.Console.Abstract
{
    public  interface IProductService
    {

        List<Product> GetAllProducts();
    }
}
