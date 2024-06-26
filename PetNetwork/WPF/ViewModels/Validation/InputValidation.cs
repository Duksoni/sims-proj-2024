namespace PetNetwork.WPF.ViewModels.Validation;

public abstract class InputValidation
{
    protected static string GetRequiredFieldMessage(string fieldName) => $"{fieldName} is required.";
}