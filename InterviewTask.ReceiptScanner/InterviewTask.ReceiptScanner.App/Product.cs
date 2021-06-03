using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewTask.ReceiptScanner.App
{
    public class Product
    {
        public string Name { get; set; }
        public bool Domestic { get; set; }
        public decimal Price { get; set; }
        public int Weight { get; set; }
        public string Description { get; set; }
    }
}
