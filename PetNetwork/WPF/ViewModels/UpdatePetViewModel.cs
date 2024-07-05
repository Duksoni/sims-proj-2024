using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using System.Collections.ObjectModel;
using System.Reflection.Metadata.Ecma335;

namespace PetNetwork.WPF.ViewModels
{
    class UpdatePetViewModel : BaseViewModel
    {
        private readonly PetService _petService;
        private readonly AnimalTypeService _animalTypeService;
        private readonly UserService _userService;

        public ObservableCollection<PetOwnership> Ownerships { get; }
        public ObservableCollection<PetAdoptionStatus> AdoptionStatuses { get; }
        public ObservableCollection<Gender> Genders { get; }
        public ObservableCollection<string> UserAccounts { get; }
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
            var userAccountRepo = Injector.CreateInstance<IRepository<UserAccount>>();
            var personRepo = Injector.CreateInstance<IRepository<Person>>();

            _petService = new PetService(petRepo);
            _animalTypeService = new AnimalTypeService(animalTypeRepo);
            _userService = new UserService(userAccountRepo, personRepo);

            Ownerships = new(Enum.GetValues(typeof(PetOwnership)).Cast<PetOwnership>());
            AdoptionStatuses = new(Enum.GetValues(typeof(PetAdoptionStatus)).Cast<PetAdoptionStatus>());
            Genders = new(Enum.GetValues(typeof(Gender)).Cast<Gender>());
            UserAccounts = new();
            AnimalTypes = new(_animalTypeService.GetAll(false));
            HealthConditions = new(Enum.GetValues(typeof(PetHealthCondition)).Cast<PetHealthCondition>());

            var userAccounts = _userService.GetAllAccounts(false);
            foreach (var userAccount in userAccounts)
                UserAccounts.Add(userAccount.Id);

            Pet = new PetViewModel(_petService.Get(SelectedPet.Id));
        }


        public RelayCommand TryUpdatePetCommand => new(_ => TryUpdatePet(), _ => Pet.IsValid);


        private void TryUpdatePet()
        {
            _petService.Update(Pet.ToPet(Pet));
            Updated = true;
        }
    }
}
