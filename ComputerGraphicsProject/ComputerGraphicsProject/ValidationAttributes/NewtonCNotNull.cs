using ComputerGraphicsProject.Models;
using System.ComponentModel.DataAnnotations;

namespace ComputerGraphicsProject.ValidationAttributes
{
    public class NewtonCNotNull : ValidationAttribute
    {
        public NewtonCNotNull(string massage)
        {
            ErrorMessage = massage;
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
