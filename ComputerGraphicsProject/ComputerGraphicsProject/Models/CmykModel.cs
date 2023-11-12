using Aspose.Svg;
using Aspose.Svg.Drawing;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ComputerGraphicsProject.Models
{
    public class CmykModel
    {

        [BindProperty]
        [Required]
        public IFormFile File { get; set; }

        [Range(0, 255, ErrorMessage = "The value of C must be between 0 and 255.")]
        public int C { get; set; }

        [Range(0, 255, ErrorMessage = "The value of M must be between 0 and 255.")]
        public int M { get; set; }

        [Range(0, 255, ErrorMessage = "The value of Y must be between 0 and 255.")]
        public int Y { get; set; }

        [Range(0, 255, ErrorMessage = "The value of K must be between 0 and 255.")]
        public int K { get; set; }

        public ColorBytesModel colorBytesModel { get; set; }

        public CmykModel()
        {
            colorBytesModel = ColorBytesModel.GetInstance();
        }

    }
}
