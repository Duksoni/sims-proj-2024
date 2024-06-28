using PetNetwork.Domain.Models;
using PetNetwork.WPF.ViewModels.Validation;
using System.ComponentModel;

namespace PetNetwork.WPF.ViewModels;

public class AddressViewModel : BaseViewModel, IDataErrorInfo
{

	private string _street = string.Empty;
    public string Street
    {
        get => _street;
        set
        {
            if (_street == value) return;
            _street = value;
            OnPropertyChanged();
        }
    }

    private string _streetNo = string.Empty;
    public string StreetNo
    {
        get => _streetNo;
        set
        {
            if (_streetNo == value) return;
            _streetNo = value;
            OnPropertyChanged();
        }
    }

    private string _town = string.Empty;
    public string Town
    {
        get => _town;
        set
        {
            if (_town == value) return;
            _town = value;
            OnPropertyChanged();
        }
    }

    private int _postalCode = 11000;
    public string PostalCode
    {
        get => _postalCode.ToString();
        set
        {
            if (!int.TryParse(value, out var parsed))
                return;
            if (_postalCode == parsed) return;
            if (parsed > 38299)
                parsed = 11000;
            _postalCode = parsed;
            OnPropertyChanged();
        }
    }

    public Address ToAddress() => new(_street, _streetNo, _town, _postalCode);

    private readonly AddressInputValidation _validation = new();

    public string this[string columnName]
    {
        get
        {
            return columnName switch
            {
                "Street" => _validation.ValidateStreet(Street),
                "StreetNo" => _validation.ValidateStreetNumber(StreetNo),
                "Town" => _validation.ValidateTown(Town),
                _ => string.Empty
            };
        }
    }

    public string Error => string.Empty;

    private readonly string[] _validatedProperties = { "Street", "StreetNo", "Town" };

    public AddressViewModel()
    {
    }

    public AddressViewModel(Address address)
    {
        _street = address.Street;
        _streetNo = address.StreetNo;
        _town = address.Town;
        _postalCode = address.PostalCode;
    }

    public bool IsValid => _validatedProperties.All(property => this[property] == string.Empty);
}