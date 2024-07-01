using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Models;
using System.ComponentModel;

namespace PetNetwork.WPF.ViewModels;

public class PaymentViewModel : BaseViewModel, IDataErrorInfo
{
    public string Id { get; } = string.Empty;

    public string PayerBankAccountNo { get; } = string.Empty;

	private string _payer = string.Empty;
	public string Payer
	{
		get => _payer;
        set
		{
            if (_payer == value) return;
            _payer = value;
			OnPropertyChanged();
		}
	}

    private string _purpose = string.Empty;
    public string Purpose
    {
        get => _purpose;
        set
        {
            if (_purpose == value) return;
            _purpose = value;
            OnPropertyChanged();
        }
    }

    private decimal _amount;
    public decimal Amount
    {
        get => _amount;
        set
        {
            if (_amount == value) return;
            if (value < 0)
                value = 0;
            _amount = value;
            OnPropertyChanged();
        }
    }

    private DateTime _paymentDate = DateTime.Now;
    public DateTime PaymentDate
    {
        get => _paymentDate;
        set
        {
            if (_paymentDate == value) return;
            _paymentDate = value;
            OnPropertyChanged();
        }
    }

    private PaymentType _paymentType;
    public PaymentType PaymentType
    {
        get => _paymentType;
        set
        {
            _paymentType = value;
            OnPropertyChanged();
        }
    }

    private string _assignedPetId = string.Empty;
    public string AssignedPetId
    {
        get => _assignedPetId;
        set
        {
            _assignedPetId = value;
            OnPropertyChanged();
        }
    }

    public PaymentViewModel() { }

    public PaymentViewModel(Payment payment)
    {
        Id = payment.Id;
		_payer = payment.Payer;
        _purpose = payment.Purpose;
        _amount = payment.Amount;
        _paymentDate = payment.PaymentDate;
        _paymentType = payment.PaymentType;
        _assignedPetId = payment.AssignedPetId;
    }

    /// <summary> Use when importing from bank statement file </summary>
    public PaymentViewModel(PaymentDTO payment)
    {
        PayerBankAccountNo = payment.RacunZaduzenja;
        _payer = payment.NazivZaduzenja;
        Purpose = payment.SvrhaDoznake;
        Amount = payment.Iznos;
        PaymentDate = payment.VremeIzvrsenja;
    }

	public Payment ToPayment() => Id == string.Empty 
        ? new Payment(_payer, Purpose, Amount, PaymentDate, PaymentType, AssignedPetId)
        : new Payment(Id, _payer, Purpose, Amount, PaymentDate, PaymentType, AssignedPetId, false);
    
    public string Error => string.Empty;

    public string this[string columnName]
    {
        get
        {
            return columnName switch
            {
                "Payer" => Payer == string.Empty ? "You must enter the payer." : string.Empty,
                "Purpose" => Purpose == string.Empty ? "You must enter the donation purpose." : string.Empty,
                "Amount" => Amount == 0 ? "You must enter the amount" : string.Empty,
                _ => string.Empty
            };
        }
    }

    private readonly string[] _validatedProperties = { "Payer", "Purpose", "Amount" };

    public bool IsValid => _validatedProperties.All(property => this[property] == string.Empty);
}