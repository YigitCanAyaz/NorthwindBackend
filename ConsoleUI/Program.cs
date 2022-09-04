using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    // SOLID
    // O harfi => Open Closed Principle (Yaptığın yazılıma yeni bir özellik ekliyorsan, mevcuttaki hiçbir koduna dokunamazsın)
    class Program
    {
        static void Main(string[] args)
        {
            ProductTest();
            // IoC
            // CategoryTest();
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());

            foreach (var category in categoryManager.GetAll().Data)
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal(), new CategoryManager(new EfCategoryDal()));

            var result = productManager.GetProductDetails();

            if (result.Success)
            {
                foreach (var product in productManager.GetProductDetails().Data)
                {
                    Console.WriteLine(product.ProductName + " - " + product.CategoryName);
                }
            }

            else
            {
                Console.WriteLine(result?.Message);
            }

        }
    }
}
