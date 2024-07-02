using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using System.Collections.ObjectModel;

namespace PetNetwork.WPF.ViewModels;

public class AddDonationViewModel : BaseViewModel
{
    private readonly PaymentService _paymentService;

    public ReadOnlyObservableCollection<PaymentType> PaymentTypes => new(
        new ObservableCollection<PaymentType>(Enum.GetValues(typeof(PaymentType))
            .Cast<PaymentType>()));

    public ObservableCollection<string> UserEmails { get; }

    private bool _submitted;
    public bool Submitted
    {
        get => _submitted;
        private set
        {
            _submitted = value;
            OnPropertyChanged();
        }
    }

    public PaymentViewModel Payment { get; }

    public RelayCommand SubmitCommand => new(_ => Submit(), _ => Payment.IsValid);

    public AddDonationViewModel()
    {
        Payment = new PaymentViewModel();
        _paymentService = new PaymentService(Injector.CreateInstance<IRepository<Payment>>());
        var userRepo = Injector.CreateInstance<IRepository<UserAccount>>();
        var personRepo = Injector.CreateInstance<IRepository<Person>>();
        var userService = new UserService(userRepo, personRepo);
        UserEmails = new ObservableCollection<string>(userService.GetAllAccounts().Where(acc => acc.Status != AccountStatus.Rejected).Select(acc => acc.Id));
    }

    private void Submit()
    {
        _paymentService.Add(Payment.ToPayment());
        Submitted = true;
    }
}