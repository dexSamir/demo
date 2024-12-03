using System;
namespace UniqloProject.ViewModel.Slider
{
    public class UpdateSliderVM
    {
        public string Title { get; set; } = null!;
        public string SubTitle { get; set; } = null!; 
        public string? Link { get; set; }
        public IFormFile? File { get; set; } 
        public string? ExistingImageUrl { get; set; } 
    }

}

