using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ComputerGraphicsProject.Models
{
    public class HslModel
    {
        [BindProperty]
        [Required]
        public IFormFile File { get; set; }

        [Range(0, 360, ErrorMessage = "The value of H must be between 0 and 360.")]
        public int? H { get; set; }

        public int? S { get; set; }

        public int? L { get; set; }

        public ColorBytesModel colorBytesModel { get; set; }

        public HslModel()
        {
            colorBytesModel = ColorBytesModel.GetInstance();
        }
    }
}
