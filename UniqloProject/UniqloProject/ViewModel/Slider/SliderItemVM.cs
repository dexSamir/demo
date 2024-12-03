using System;
namespace UniqloProject.ViewModel.Slider
{
	public class SliderItemVM
    {
        public string Title { get; set; } = null!;

        public string Subtitle { get; set; } = null!;

        public string? Link { get; set; }

        public string ImageUrl { get; set; } = null!;

        public IFormFile? File { get; set; }
    }
}

