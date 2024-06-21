using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Models;
using PetNetwork.WPF.ViewModels.Validation;

namespace PetNetwork.WPF.ViewModels;

public class PersonViewModel : BaseViewModel
{
    private string _firstName = string.Empty;
    public string FirstName
    {
        get => _firstName;
        set 
        {
            if (_firstName == value) return;
            _firstName = value;
            OnPropertyChanged();
        }
    }

    private string _lastName = string.Empty;
    public string LastName
    {
        get => _lastName;
        set 
        {
            if (_lastName == value) return;
            _lastName = value;
            OnPropertyChanged();
        }
    }

    private string _phone = string.Empty;
    public string Phone
    {
        get => _phone;
        set 
        {
            if (_phone == value) return;
            _phone = value;
            OnPropertyChanged();
        }
    }

    private Gender _gender;
    public Gender Gender
    {
        get => _gender;
        set 
        {
            if (_gender == value) return;
            _gender = value;
            OnPropertyChanged();
        }
    }

    public AddressViewModel AddressViewModel { get; } = new();

    private string _identityCardNo = string.Empty;
    public string IdentityCardNo
    {
        get => _identityCardNo;
        set
        {
            if (_identityCardNo == value) return;
            _identityCardNo = value;
            OnPropertyChanged();
        }
    }

    public Person ToPerson(string email) =>
        new(email, FirstName, LastName, Phone, Gender, AddressViewModel.ToAddress(), IdentityCardNo);

    private readonly PersonInputValidation _validation = new();

    public virtual string this[string columnName]
    {
        get
        {
            return columnName switch
            {
                "FirstName" => _validation.ValidateFirstName(FirstName),
                "LastName" => _validation.ValidateLastName(LastName),
                "Phone" => _validation.ValidatePhone(Phone),
                "Address" => _validation.ValidateAddress(AddressViewModel),
                "IdentityCardNo" => _validation.ValidateIdentityCardNo(IdentityCardNo),
                _ => string.Empty
            };
        }
    }

    private readonly string[] _validatedProperties = { "FirstName", "LastName", "Phone", "Address", "IdentityCardNo" };

    public bool IsValid => _validatedProperties.All(property => this[property] == string.Empty);
}