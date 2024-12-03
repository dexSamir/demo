
using System;
namespace UniqloProject.ViewModel.Product
{
	public class ProductItemVM
	{
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!; 

        public decimal Price { get; set; }
        public int Discount { get; set; }

        public bool IsInStock { get; set; } 
    }
}

