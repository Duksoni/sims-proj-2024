using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace PetNetwork.WPF.ViewModels;

public class AllPaymentsViewModel : BaseViewModel
{
    protected readonly PaymentService PaymentService;

    public ICollectionView PaymentsView { get; }

    public ObservableCollection<PaymentViewModel> Payments;

	private PaymentViewModel? _selectedPayment;
	public PaymentViewModel? SelectedPayment
	{
		get => _selectedPayment;
        set
		{
			_selectedPayment = value;
			OnPropertyChanged();
		}
	}

    public AllPaymentsViewModel(bool initTable = true)
    {
        PaymentService = new PaymentService(Injector.CreateInstance<IRepository<Payment>>());
        Payments = new ObservableCollection<PaymentViewModel>();
        PaymentsView = CollectionViewSource.GetDefaultView(Payments);
        if (!initTable) return;
        SetTable();
    }

    public void SetTable()
    {
        Payments.Clear();
        var allPayments = PaymentService.GetAll();
        var models = allPayments.Select(payment => new PaymentViewModel(payment)).ToList();
        foreach (var model in models)
            Payments.Add(model);
    }
}