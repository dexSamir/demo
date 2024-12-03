using System;
using UniqloProject.ViewModel.Category;
using UniqloProject.ViewModel.Product;
using UniqloProject.ViewModel.Slider;

namespace UniqloProject.ViewModel.Common
{
	public class HomeVM
	{
		public IEnumerable<SliderItemVM> Sliders { get; set; }
		public IEnumerable<CategoryItemVM> Categories { get; set; }
        public IEnumerable<ProductItemVM> Products { get; set; }
    }
}

