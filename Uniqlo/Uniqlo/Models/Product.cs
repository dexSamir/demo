using System;
using Uniqlo.ViewModel.Product;

namespace Uniqlo.Models
{
	public class Product : BaseEntity
	{
		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;
		public decimal CostPrice { get; set; }
        public decimal Sellprice { get; set; }
		public int Quantity { get; set; }
		public int Discount { get; set; }
		public string CoverImage { get; set; } = null!;
		public int? CategoryId { get; set; }
		public Category? Category { get; set; }
		public ICollection<ProductImage>? Images { get; set; }


		//public static implicit operator Product(ProductCreateVM vm)
		//{
		//	return new Product
		//	{
		//		CategoryId = vm.CategoryId,
		//		CostPrice = vm.CostPrice,
		//		Description = vm.Description,
		//		Name = vm.Name,
		//		Quantity = vm.Quantity,
		//		Sellprice = vm.Sellprice,
		//		Discount = vm.Discount
		//	};
		//}
    }
}

