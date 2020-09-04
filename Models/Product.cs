using System;
using System.Collections.Generic;

namespace makeupDashboard.Models
{
    public partial class Product
    {
        public string Retailer { get; set; }
        public string ProductId { get; set; }
        public string SkuId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public decimal ListPrice { get; set; }
        public decimal? SalePrice { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? SaleDate { get; set; }
        public string Url { get; set; }
    }
}
