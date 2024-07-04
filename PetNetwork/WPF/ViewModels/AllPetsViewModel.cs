using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using PetNetwork.WPF.Views.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace PetNetwork.WPF.ViewModels
{
    public class AllPetsViewModel : BaseViewModel
    {
        private readonly PetService _petService;

        public PetViewModel Pet { get; set; }

        public ObservableCollection<PetViewModel> AllPets { get; }

        public ICollectionView PetsView { get; }

        private PetViewModel? _selectedPet;
        public PetViewModel? SelectedPet
        {
            get => _selectedPet;
            set
            {
                _selectedPet = value;
                OnPropertyChanged();
            }
        }


        public AllPetsViewModel()
        {
            var petRepo = Injector.CreateInstance<IRepository<Pet>>();
            _petService = new PetService(petRepo);
            AllPets = new ObservableCollection<PetViewModel>();
            PetsView = CollectionViewSource.GetDefaultView(AllPets);
            SetCollection();
        }


        public RelayCommand AddPetViewCommand => new(_ => AddPetView());

        public RelayCommand UpdatePetViewCommand => new(_ => UpdatePetView(SelectedPet), _ => IsValidUpdatePetView());


        private void AddPetView()
        {
            var addPetView = new AddPetView().ShowDialog();
            if (addPetView != true) return;
            this.SetCollection();
        }

        private void UpdatePetView(PetViewModel SelectedPet)
        {
            var updatePetView = new UpdatePetView(SelectedPet).ShowDialog();
            if (updatePetView != true) return;
            this.SetCollection();
        }

        public void SetCollection()
        {
            var pets = _petService.GetAll();
            var models = (from Pet pet in pets select new PetViewModel(pet)).ToList();

            AllPets.Clear();
            foreach (var model in models) AllPets.Add(model);
        }

        private bool IsValidUpdatePetView() => SelectedPet != null;
    }
}
