using System;
using System.ComponentModel.DataAnnotations;

namespace UniqloProject.ViewModel.Slider
{
    public class SliderCreateVM
    {
        [MaxLength(32, ErrorMessage = "Title must be less than 32 charachters"), Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } 
        [MaxLength(63, ErrorMessage = "Subtitle must be less than 64 charachters"), Required(ErrorMessage = "Subtitle is required")]
        public string Subtitle { get; set; } 
        public string Link { get; set; }
        public string ImageUrl { get; set; } 
        public IFormFile File { get; set; } = null!; 
    }
}

