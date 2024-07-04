using System.Text.RegularExpressions;

namespace PetNetwork.WPF.ViewModels.Validation
{
    public class PetInputValidation : InputValidation
    {
        private readonly Regex _stringRegex = new(@"^[\w'\-,.]*[^_!¡?÷?¿\/\\+=@#$%ˆ&*(){}|~<>;:[\]]*$");

        public string ValidateBreed(string breed)
        {
            if (string.IsNullOrEmpty(breed)) return GetRequiredFieldMessage("Breed");
            return _stringRegex.IsMatch(breed) ? string.Empty : "Invalid breed.";
        }

        public string ValidateColour(string colour)
        {
            if (string.IsNullOrEmpty(colour)) return GetRequiredFieldMessage("Colour");
            return _stringRegex.IsMatch(colour) ? string.Empty : "Invalid colour.";
        }

        public string ValidateBirthYear(int birthYear)
        {
            if (birthYear == -1) return GetRequiredFieldMessage("Birth year");
            return (birthYear < 1800 || birthYear > DateTime.Now.Year) ? "Invalid birth year." : string.Empty;
        }
    }
}
