using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ComputerGraphicsProject.Models
{
    public class HslModel
    {

        [Range(0, 360, ErrorMessage = "The value of H must be between 0 and 360.")]
        public int H { get; set; }

        [Range(0, 100, ErrorMessage = "The value of S must be between 0 and 100.")]
        public int S { get; set; }

        [Range(0, 100, ErrorMessage = "The value of L must be between 0 and 100.")]
        public int L { get; set; }

        public ColorBytesModel colorBytesModel { get; set; }

        public HslModel()
        {
            colorBytesModel = ColorBytesModel.GetInstance();
        }
    }
}
