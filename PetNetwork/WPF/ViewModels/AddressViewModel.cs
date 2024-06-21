using PetNetwork.Domain.Models;

namespace PetNetwork.WPF.ViewModels;

public class AddressViewModel : BaseViewModel
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
    public int PostalCode
    {
        get => _postalCode;
        set
        {
            if (_postalCode == value) return;
            if (value is < 11000 or > 38299)
                _postalCode = 11000;
            _postalCode = value;
            OnPropertyChanged();
        }
    }

    public Address ToAddress() => new(Street, StreetNo, Town, PostalCode);
}