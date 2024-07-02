using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using System.Collections.ObjectModel;

namespace PetNetwork.WPF.ViewModels;

public class AssignPaymentToPetViewModel : BaseViewModel
{
    private readonly PaymentService _paymentService;

    public ObservableCollection<Pet> Pets { get; }

    private Pet? _selectedPet;
    public Pet? SelectedPet
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
        var petService = new PetService(Injector.CreateInstance<IRepository<Pet>>());
        Pets = new ObservableCollection<Pet>(petService.FindNonAdopted());
    }

    private void Submit()
    {
        _selectedPayment.AssignedPetId = SelectedPet!.Id;
        _paymentService.Update(_selectedPayment.ToPayment());
        Assigned = true;
    }
}