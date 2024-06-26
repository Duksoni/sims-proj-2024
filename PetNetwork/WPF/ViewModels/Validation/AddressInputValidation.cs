namespace PetNetwork.WPF.ViewModels.Validation;

public class AddressInputValidation : InputValidation
{
    public string ValidateStreet(string street) => string.IsNullOrEmpty(street) ? GetRequiredFieldMessage("Street") : string.Empty;

    public string ValidateStreetNumber(string streetNo) => string.IsNullOrEmpty(streetNo) ? GetRequiredFieldMessage("Street number") : string.Empty;

    public string ValidateTown(string town) => string.IsNullOrEmpty(town) ? GetRequiredFieldMessage("Town") : string.Empty;
}