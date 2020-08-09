using System;
using System.Collections.Generic;

namespace makeupDashboard.Models
{
    public partial class Product
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public string Retailer { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public string Size { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal? NewPrice { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? SaleDate { get; set; }
    }
}
