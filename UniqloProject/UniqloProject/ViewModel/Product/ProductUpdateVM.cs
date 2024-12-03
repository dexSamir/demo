using System;
using System.ComponentModel.DataAnnotations;

namespace UniqloProject.ViewModel.Product
{
	public class ProductUpdateVM
	{
        [MaxLength(32, ErrorMessage = "Name must be less than 32 charachter"), Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; } = null!;

        [MaxLength(64, ErrorMessage = "Description must be less than 32 charachter"), Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "CostPrice is required")]
        public decimal CostPrice { get; set; }

        [Required(ErrorMessage = "SellPrice is required")]
        public decimal SellPrice { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
        public int Discount { get; set; }

        [Required(ErrorMessage = "Cover File is required")]
        public IFormFile CoverFile { get; set; } = null!;

        public int? CategoryId { get; set; }
    }
}

