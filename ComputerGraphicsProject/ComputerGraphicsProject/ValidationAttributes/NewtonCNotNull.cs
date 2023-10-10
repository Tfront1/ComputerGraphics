using ComputerGraphicsProject.Models;
using System.ComponentModel.DataAnnotations;

namespace ComputerGraphicsProject.ValidationAttributes
{
    public class NewtonCNotNull : ValidationAttribute
    {
        public NewtonCNotNull()
        {
            ErrorMessage = "Real c and imaginary c cant both be 0";
        }
        public override bool IsValid(object? value)
        {
            NewtonFractalModel newtonFractalModel = value as NewtonFractalModel;
            if (newtonFractalModel.RealC == 0 && newtonFractalModel.ImaginaryC == 0) {
                return false;
            }
            return true;
        }
    }
}
