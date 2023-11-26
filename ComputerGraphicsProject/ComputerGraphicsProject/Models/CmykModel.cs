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

        public int? C { get; set; }

        public int? M { get; set; }

        public int? Y { get; set; }

        public int? K { get; set; }

        public ColorBytesModel colorBytesModel { get; set; }

        public CmykModel()
        {
            colorBytesModel = ColorBytesModel.GetInstance();
        }

    }
}
