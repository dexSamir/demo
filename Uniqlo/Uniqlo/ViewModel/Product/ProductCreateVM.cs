using System;
using Uniqlo.Models;

namespace Uniqlo.ViewModel.Product
{
	public class ProductCreateVM
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal CostPrice { get; set; }
        public decimal Sellprice { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public IFormFile CoverImage { get; set; } = null!;
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public IEnumerable<IFormFile>? OtherImages { get; set; } 
    }
}

