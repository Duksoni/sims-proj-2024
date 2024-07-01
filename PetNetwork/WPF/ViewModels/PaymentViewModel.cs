using PetNetwork.Domain.Models;

namespace PetNetwork.WPF.ViewModels;

public class PaymentViewModel : BaseViewModel
{
    public string Id { get; } = string.Empty;

    public string PayerBankAccountNo { get; } = string.Empty;

	private string _payer;
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

    public string Purpose { get; }

    public decimal Amount { get; }

	public DateTime PaymentDate { get; }

    public PaymentViewModel(Payment payment)
    {
        Id = payment.Id;
		_payer = payment.Payer;
        Purpose = payment.Purpose;
        Amount = payment.Amount;
        PaymentDate = payment.PaymentDate;
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

	public Payment ToPayment() => new(_payer, Purpose, Amount, PaymentDate);
}