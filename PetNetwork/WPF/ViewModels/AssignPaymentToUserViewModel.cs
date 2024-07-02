using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using System.Collections.ObjectModel;

namespace PetNetwork.WPF.ViewModels;

public class AssignPaymentToUserViewModel : BaseViewModel
{
    public ObservableCollection<string> UserEmails { get; }

	private string? _selectedEmail;
    public string? SelectedEmail
	{
		get => _selectedEmail;
        set
		{
			_selectedEmail = value;
			OnPropertyChanged();
		}
	}

	private bool _assigned;
    public bool Assigned
	{
		get => _assigned;
        private set
		{
			_assigned = value;
			OnPropertyChanged();
		}
	}

	private readonly PaymentViewModel _selectedPayment;

    public RelayCommand SubmitCommand => new(_ => Submit(), _ => SelectedEmail != null);

    public AssignPaymentToUserViewModel(PaymentViewModel selectedPayment)
    {
        _selectedPayment = selectedPayment;
        var userRepo = Injector.CreateInstance<IRepository<UserAccount>>();
        var personRepo = Injector.CreateInstance<IRepository<Person>>();
        var userService = new UserService(userRepo, personRepo);
        UserEmails = new ObservableCollection<string>(userService.GetAllAccounts().Where(acc => acc.Status != AccountStatus.Rejected).Select(acc => acc.Id));
    }

    private void Submit()
    {
        _selectedPayment.Payer = SelectedEmail!;
        Assigned = true;
    }
}