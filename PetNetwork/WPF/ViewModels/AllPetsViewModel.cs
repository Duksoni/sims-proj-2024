using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using PetNetwork.WPF.Views.UserControls;
using PetNetwork.WPF.Views.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace PetNetwork.WPF.ViewModels
{
    public class AllPetsViewModel : BaseViewModel
    {
        private readonly PetService _petService;
        private readonly AnimalTypeService _animalTypeService;

        public ObservableCollection<PetViewModel> AllPets { get; }
        public ObservableCollection<AnimalType> AnimalTypes { get; }
        public ObservableCollection<PetAdoptionStatus> PetAdoptionStatuses { get; }

        public PetViewModel Pet { get; set; }

        public ICollectionView PetsView { get; }

        AllPetsView AllPetsView;

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


        public AllPetsViewModel(AllPetsView allPetsView)
        {
            var petRepo = Injector.CreateInstance<IRepository<Pet>>();
            var animalTypeRepo = Injector.CreateInstance<IRepository<AnimalType>>();

            _petService = new PetService(petRepo);
            _animalTypeService = new AnimalTypeService(animalTypeRepo);

            AllPets = new ObservableCollection<PetViewModel>();
            PetsView = CollectionViewSource.GetDefaultView(AllPets);
            AnimalTypes = new(_animalTypeService.GetAll(true));
            PetAdoptionStatuses = new(Enum.GetValues(typeof(PetAdoptionStatus)).Cast<PetAdoptionStatus>());

            AllPetsView = allPetsView;

            AllPetsView.AnimalTypeComboBox.SelectedIndex = 0;
            AllPetsView.PetAdoptionStatusComboBox.SelectedIndex = 0;
            
            SetCollection();
        }


        public RelayCommand SearchAnimalTypeCommand => new(_ => SearchAnimalType());

        public RelayCommand SearchBreedCommand => new(_ => SearchBreed(), _ => IsValidSearchBreed());

        public RelayCommand SearchPetAdoptionStatusCommand => new(_ => SearchPetAdoptionStatus());

        public RelayCommand ResetSearchCommand => new(_ => SetCollection());


        private void SearchAnimalType()
        {
            var pets = _petService.GetAll();
            var models = (from Pet pet in pets select new PetViewModel(pet)).ToList();

            AllPets.Clear();
            foreach (var model in models)
                if (model.Animal.Id == AllPetsView.AnimalTypeComboBox.Text) AllPets.Add(model);
        }

        private void SearchBreed()
        {
            var pets = _petService.GetAll();
            var models = (from Pet pet in pets select new PetViewModel(pet)).ToList();

            AllPets.Clear();
            foreach (var model in models)
                if (model.Breed.ToLower() == AllPetsView.BreedTextBox.Text.ToLower()) AllPets.Add(model);
        }

        private void SearchPetAdoptionStatus()
        {
            var pets = _petService.GetAll();
            var models = (from Pet pet in pets select new PetViewModel(pet)).ToList();

            AllPets.Clear();
            foreach (var model in models)
                if ((int)model.Status == AllPetsView.PetAdoptionStatusComboBox.SelectedIndex) AllPets.Add(model);
        }


        private bool IsValidSearchBreed() => (AllPetsView.BreedTextBox.Text != string.Empty);


        public RelayCommand AddPetViewCommand => new(_ => AddPetView(), _ => IsValidAddPetView());

        public RelayCommand UpdatePetViewCommand => new(_ => UpdatePetView(SelectedPet), _ => IsValidUpdatePetView());

        public RelayCommand AddAnimalTypeViewCommand => new(_ => AddAnimalTypeView(), _ => IsValidAddAnimalTypeView());

        public RelayCommand RemoveAnimalTypeViewCommand => new(_ => RemoveAnimalTypeView(), _ => IsValidRemoveAnimalTypeView());


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

        private void AddAnimalTypeView()
        {
            var addAnimalTypeView = new AddAnimalTypeView().ShowDialog();
            if (addAnimalTypeView != true) return;
            this.SetCollection();
        }

        private void RemoveAnimalTypeView()
        {
            var removeAnimalTypeView = new RemoveAnimalTypeView().ShowDialog();
            if (removeAnimalTypeView != true) return;
            this.SetCollection();
        }

        public void SetCollection()
        {
            var pets = _petService.GetAll();
            var models = (from Pet pet in pets select new PetViewModel(pet)).ToList();

            AllPets.Clear();
            foreach (var model in models) AllPets.Add(model);
        }

        private bool IsValidAddPetView() => UserSession.Session?.Account.Role == AccountRole.Volunteer;

        private bool IsValidUpdatePetView() => (SelectedPet != null && UserSession.Session?.Account.Role == AccountRole.Volunteer);

        private bool IsValidAddAnimalTypeView() => UserSession.Session?.Account.Role == AccountRole.Volunteer;

        private bool IsValidRemoveAnimalTypeView() => UserSession.Session?.Account.Role == AccountRole.Volunteer;
    }
}
