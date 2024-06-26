using System.Text.RegularExpressions;

namespace PetNetwork.WPF.ViewModels.Validation;

public class UserAccountValidation : InputValidation
{
    private readonly Regex _emailRegex =
        new(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

    private readonly Regex _passwordRegex =
        new(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");

    public string ValidateEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            return GetRequiredFieldMessage("Email");
        return _emailRegex.IsMatch(email) ? string.Empty : "Invalid email format.";
    }

    public string ValidatePassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            return "Password is required.";
        return _passwordRegex.IsMatch(password)
            ? string.Empty
            : "Password must contain at least one uppercase\nletter, one lowercase letter," +
              " one digit and be at\nleast 8 characters long.";
    }
}