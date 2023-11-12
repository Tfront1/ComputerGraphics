using Aspose.Svg;
using Aspose.Svg.Drawing;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ComputerGraphicsProject.Models
{
    public class CmykModel
    {

        [BindProperty]
        public IFormFile File { get; set; }

        [Range(0, 255)]
        public int C { get; set; }

        [Range(0, 255)]
        public int M { get; set; }

        [Range(0, 255)]
        public int Y { get; set; }

        [Range(0, 255)]
        public int K { get; set; }

        public CmykBytesModel CmykBytesModel { get; set; }

        public CmykModel()
        {
            CmykBytesModel = CmykBytesModel.GetInstance();
        }

    }
}
