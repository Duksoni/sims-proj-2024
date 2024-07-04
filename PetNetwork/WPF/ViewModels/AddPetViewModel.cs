using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using System.Collections.ObjectModel;

namespace PetNetwork.WPF.ViewModels
{
    class AddPetViewModel : BaseViewModel
    {
        private readonly PetService _petService;
        private readonly AnimalTypeService _animalTypeService;

        public ObservableCollection<Gender> Genders { get; }
        public ObservableCollection<AnimalType> AnimalTypes { get; }
        public ObservableCollection<PetHealthCondition> HealthConditions { get; }

        private bool _added;
        public bool Added
        {
            get => _added;
            private set
            {
                _added = value;
                OnPropertyChanged();
            }
        }

        public PetViewModel Pet { get; }


        public AddPetViewModel()
        {
            var petRepo = Injector.CreateInstance<IRepository<Pet>>();
            var animalTypeRepo = Injector.CreateInstance<IRepository<AnimalType>>();

            _petService = new PetService(petRepo);
            _animalTypeService = new AnimalTypeService(animalTypeRepo);

            Genders = new(Enum.GetValues(typeof(Gender)).Cast<Gender>());
            AnimalTypes = new(_animalTypeService.GetAll(false));
            HealthConditions = new(Enum.GetValues(typeof(PetHealthCondition)).Cast<PetHealthCondition>());

            Pet = new PetViewModel();
        }


        public RelayCommand TryAddPetCommand => new(_ => TryAddPet(), _ => Pet.IsValid);


        private void TryAddPet()
        {
            _petService.Add(Pet.ToPet());
            Added = true;
        }
    }
}
