using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using System.Collections.ObjectModel;

namespace PetNetwork.WPF.ViewModels
{
    class UpdatePetViewModel : BaseViewModel
    {
        private readonly PetService _petService;
        private readonly AnimalTypeService _animalTypeService;

        public ObservableCollection<Gender> Genders { get; }
        public ObservableCollection<AnimalType> AnimalTypes { get; }
        public ObservableCollection<PetHealthCondition> HealthConditions { get; }

        private bool _updated;
        public bool Updated
        {
            get => _updated;
            private set
            {
                _updated = value;
                OnPropertyChanged();
            }
        }

        public PetViewModel Pet { get; }


        public UpdatePetViewModel(PetViewModel SelectedPet)
        {
            var petRepo = Injector.CreateInstance<IRepository<Pet>>();
            var animalTypeRepo = Injector.CreateInstance<IRepository<AnimalType>>();

            _petService = new PetService(petRepo);
            _animalTypeService = new AnimalTypeService(animalTypeRepo);

            Genders = new(Enum.GetValues(typeof(Gender)).Cast<Gender>());
            AnimalTypes = new(_animalTypeService.GetAll(false));
            HealthConditions = new(Enum.GetValues(typeof(PetHealthCondition)).Cast<PetHealthCondition>());

            Pet = new PetViewModel(_petService.Get(SelectedPet.Id));
        }


        public RelayCommand TryUpdatePetCommand => new(_ => TryUpdatePet(), _ => Pet.IsValid);


        private void TryUpdatePet()
        {
            _petService.Update(Pet.ToPet(Pet.Id));
            Updated = true;
        }
    }
}
