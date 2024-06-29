using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Models;
using PetNetwork.WPF.ViewModels.Validation;
using System.ComponentModel;

namespace PetNetwork.WPF.ViewModels;

public class PersonViewModel : BaseViewModel, IDataErrorInfo
{
    public string Email { get; } = string.Empty;

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

    public AddressViewModel Address { get; } = new();

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

    public PersonViewModel()
    {
    }

    public PersonViewModel(Person personalInfo)
    {
        _firstName = personalInfo.FirstName;
        _lastName = personalInfo.LastName;
        _phone = personalInfo.Phone;
        _gender = personalInfo.Gender;
        Address = new AddressViewModel(personalInfo.Address);
        _identityCardNo = personalInfo.IdentityCardNo;
        Email = personalInfo.Id;
    }

    public Person ToPerson(string email) =>
        new(email, FirstName, LastName, Phone, Gender, Address.ToAddress(), IdentityCardNo);

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
                "IdentityCardNo" => _validation.ValidateIdentityCardNo(IdentityCardNo),
                _ => string.Empty
            };
        }
    }

    public string Error => string.Empty;

    private readonly string[] _validatedProperties = { "FirstName", "LastName", "Phone", "IdentityCardNo" };

    public bool IsValid => _validatedProperties.All(property => this[property] == string.Empty) && Address.IsValid;
}
