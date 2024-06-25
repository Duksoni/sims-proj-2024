using PetNetwork.Domain.Models;
using System.ComponentModel;

namespace PetNetwork.WPF.ViewModels;

public class PetSocietyViewModel : BaseViewModel, IDataErrorInfo
{

    private readonly string _id = string.Empty;


    private string _name = string.Empty;
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    private string _bankAccount = string.Empty;
    public string BankAccount
    {
        get => _bankAccount;
        set
        {
            if (!long.TryParse(value, out _))
            {
                _bankAccount = string.Empty;
                return;
            }
            _bankAccount = value;
            OnPropertyChanged();
        }
    }

    public PetSocietyViewModel() { }

    public PetSocietyViewModel(PetSociety society)
    {
        _id = society.Id;
        Name = society.Name;
        BankAccount = society.BankAccount;
    }

    public PetSociety ToPetSociety() => _id == string.Empty ? new PetSociety(_name, _bankAccount) : new PetSociety(_id, _name, _bankAccount);

    public string this[string columnName]
    {
        get
        {
            return columnName switch
            {
                "Name" => Name == string.Empty ? "Society name is required" : string.Empty,
                "BankAccount" => BankAccount == string.Empty ? "Bank account is required" : string.Empty,
                _ => string.Empty
            };
        }
    }
    public string Error => string.Empty;

    private readonly string[] _validatedProperties = { "Name", "BankAccount" };

    public bool IsValid => _validatedProperties.All(property => this[property] == string.Empty);

}