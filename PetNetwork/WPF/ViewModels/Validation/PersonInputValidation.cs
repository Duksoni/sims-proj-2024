using System.Text.RegularExpressions;

namespace PetNetwork.WPF.ViewModels.Validation;

public class PersonInputValidation : InputValidation
{
    private readonly Regex _nameRegex =
        new(@"^[\w'\-,.]*[^_!¡?÷?¿\/\\+=@#$%ˆ&*(){}|~<>;:[\]]*$");

    private readonly Regex _phoneRegex = 
        new(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$");

    private readonly int[] _idCardNoWeights = { 7, 6, 5, 4, 3, 2, 7, 6 };

    public string ValidateFirstName(string firstName)
    {
        if (string.IsNullOrEmpty(firstName)) return GetRequiredFieldMessage("First name");
        return _nameRegex.IsMatch(firstName) ? string.Empty : "Invalid first name.";
    }

    public string ValidateLastName(string lastName)
    {
        if (string.IsNullOrEmpty(lastName)) return GetRequiredFieldMessage("Last name");
        return _nameRegex.IsMatch(lastName) ? string.Empty : "Invalid last name.";
    }

    public string ValidatePhone(string phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber))
            return "Phone number is required.";
        return _phoneRegex.IsMatch(phoneNumber) 
            ? string.Empty 
            : "Phone number must follow the format:\n'[+][country code][(area code)]-local number" +
              "\nwith optional spaces, parentheses, and dashes.";
    }

    public string ValidateAddress(AddressViewModel address)
    {
        if (string.IsNullOrEmpty(address.Street)) return GetRequiredFieldMessage("Street");
        if (string.IsNullOrEmpty(address.StreetNo)) return GetRequiredFieldMessage("Street number");
        return string.IsNullOrEmpty(address.Town) ? GetRequiredFieldMessage("Town") : string.Empty;
    }

    public string ValidateIdentityCardNo(string identityCardNo)
    {
        if (identityCardNo.Length != 9 || !long.TryParse(identityCardNo, out _))
            return "Invalid identity format";

        var sum = 0;
        for (var i = 0; i < 8; i++)
            sum += (identityCardNo[i] - '0') * _idCardNoWeights[i];

        var controlDigit = sum % 11;
        if (controlDigit == 10)
            controlDigit = 0;

        var lastDigit = identityCardNo[8] - '0';

        return controlDigit == lastDigit ? string.Empty : "Invalid identity card format.";
    }
}