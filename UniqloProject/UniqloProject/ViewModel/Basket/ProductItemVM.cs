﻿using System;
namespace UniqloProject.ViewModel.Basket
{
	public class ProductItemVM
	{
		public int Id{ get; set; }
		public string  Name{ get; set; }
		public int  Count{ get; set; }
		public decimal SellPrice{ get; set; }
		public int Discount{ get; set; }
		public string ImageUrl { get; set; }

	}
}

