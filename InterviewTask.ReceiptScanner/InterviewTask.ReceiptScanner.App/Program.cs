using System;

namespace InterviewTask.ReceiptScanner.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var _productService = new ProductService();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("RECEIPT SCANNER \n");
            Console.ResetColor();

            _productService.PrintProducts();

            Console.ReadLine();
        }
    }
}
