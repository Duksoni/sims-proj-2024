using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using System.Collections.ObjectModel;

namespace PetNetwork.WPF.ViewModels;

public class AssignPaymentToPetViewModel : BaseViewModel
{
    private readonly PaymentService _paymentService;

    public ObservableCollection<string> Pets { get; } // TODO replace string with Pet class

    private string? _selectedPet; // TODO replace string? with Pet class
    public string? SelectedPet
    {
        get => _selectedPet;
        set
        {
            _selectedPet = value;
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

    public RelayCommand SubmitCommand => new(_ => Submit(), _ => SelectedPet != null);

    public AssignPaymentToPetViewModel(PaymentViewModel selectedPayment)
    {
        _selectedPayment = selectedPayment;
        _paymentService = new PaymentService(Injector.CreateInstance<IRepository<Payment>>());
        // TODO get all pets from service
        // TODO replace string with Pet class, define ToString in PetClass, so it can show up nicely in UI
        Pets = new ObservableCollection<string>();
    }

    private void Submit()
    {
        _selectedPayment.AssignedPetId = SelectedPet!;
        _paymentService.Update(_selectedPayment.ToPayment());
        Assigned = true;
    }
}