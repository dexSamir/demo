using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniqloProject.DataAccess;
using UniqloProject.ViewModel.Basket;

namespace UniqloProject.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
	{
		readonly UniqloAppDbContext _context;
		public HeaderViewComponent(UniqloAppDbContext context)
		{
			_context = context; 
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var basketIds = JsonSerializer.Deserialize<List<BasketProductItemVM>>(Request.Cookies["basket"] ?? "[]");

			var prods = await _context.Products
				.Where(x => basketIds.Select(y => y.Id).Any(y => y == x.Id))
				.Select(x => new ProductItemVM
				{
					Id = x.Id,
					Name = x.Name,
					Discount = x.Discount,
					SellPrice = x.SellPrice,
					ImageUrl = x.CoverImage
				}).ToListAsync();

			foreach (var item in prods)
			{
				item.Count = basketIds!.FirstOrDefault(x => x.Id == item.Id)!.Count;
			}
			return View(); 
		}
	}
}

