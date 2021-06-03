using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace InterviewTask.ReceiptScanner.App
{
    public  class ProductService
    {
        public static List<Product> productsData;
        private const string dataApi = "https://interview-task-api.mca.dev/qr-scanner-codes/alpha-qr-gFpwhsQ8fkY1";

        public List<Product> GetProducts()
        {
            string jsonData = string.Empty;
            try
            {
                jsonData = new WebClient().DownloadString(dataApi);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (!string.IsNullOrEmpty(jsonData))
            {
                List<Product> productsResponse = JsonConvert.DeserializeObject<List<Product>>(jsonData);
                productsData = productsResponse;
                return productsData;
            }
            else
            {
                return null;
            }
        }

        public void PrintProducts()
        {
            GetProducts();

            if (productsData != null)
            {
                var domesticProducts = productsData.Where(p => p.Domestic == true).OrderBy(p => p.Name).ToList();
                var importedProducts = productsData.Where(p => p.Domestic == false).OrderBy(p => p.Name).ToList();

                SortProducts(domesticProducts);
                SortProducts(importedProducts);

                TotalPrice(domesticProducts);
                TotalPrice(importedProducts);

                Console.WriteLine($"Domestic count: {domesticProducts.Count}");
                Console.WriteLine($"Imported count: {importedProducts.Count}");
            }
            else
            {
                Console.WriteLine("Sorry, no data was found!");
            }
        }

        public void SortProducts(List<Product> products)
        {
            if (products.All(p => p.Domestic == true))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(". Domestic:");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(". Imported:");
            }

            foreach (var product in products)
            {
                
                Console.WriteLine($"... {product.Name}, \n   Price: ${product.Price}, \n   {TrunicateString(product.Description,30)}...");
                if(product.Weight == 0)
                { 
                    Console.WriteLine("   Weight: N/A");
                }
                else
                {
                    Console.WriteLine($"   Weight: {product.Weight}g");
                }
            }

        }

        public string TrunicateString(string sentence, int lenght)
        {
            if(sentence.Length > 30)
            {
                return sentence.Substring(0, lenght);
            }
            return sentence;
        }

        public void TotalPrice(List<Product> products)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (products.All(p => p.Domestic == true))
            {
                Console.WriteLine($"Domestic cost: ${products.Sum(p => p.Price)}");
            }
            else
            {
                Console.WriteLine($"Imported cost: ${products.Sum(p => p.Price)}");
            }
            Console.ResetColor();
        }
       
    }
}
