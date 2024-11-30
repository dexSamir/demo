using System;
using Uniqlo.ViewModel.Product;
using Uniqlo.ViewModel.Slider;

namespace Uniqlo.ViewModel.Common
{
	public class HomeVm
	{
		public IEnumerable<SliderItemVM> Sliders { get; set; }
        public IEnumerable<ProductItemVM> Products { get; set; }
    }
}

