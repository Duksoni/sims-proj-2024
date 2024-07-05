using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Models;
using PetNetwork.WPF.ViewModels.Validation;
using System.ComponentModel;

namespace PetNetwork.WPF.ViewModels
{
    public class PetViewModel : BaseViewModel, IDataErrorInfo
    {
        public string Id { get; } = string.Empty;

        private string _breed = string.Empty;
        public string Breed
        {
            get => _breed;
            set
            {
                if (_breed == value) return;
                _breed = value;
                OnPropertyChanged();
            }
        }

        private string _colour = string.Empty;
        public string Colour
        {
            get => _colour;
            set
            {
                if (_colour == value) return;
                _colour = value;
                OnPropertyChanged();
            }
        }

        private int _birthYear = -1;
        public int BirthYear
        {
            get => _birthYear;
            set
            {
                if (_birthYear == value) return;
                _birthYear = value;
                OnPropertyChanged();
            }
        }


        private PetOwnership _ownership = new PetOwnership();
        public PetOwnership Ownership
        {
            get => _ownership;
            set
            {
                if (_ownership == value) return;
                _ownership = value;
                OnPropertyChanged();
            }
        }

        private PetAdoptionStatus _status = new PetAdoptionStatus();
        public PetAdoptionStatus Status
        {
            get => _status;
            set
            {
                if (_status == value) return;
                _status = value;
                OnPropertyChanged();
            }
        }

        private Gender _gender = Gender.Male;
        public Gender Gender
        {
            get => _gender;
            set
            {
                if (_gender == value) return;
                _gender = value;
                OnPropertyChanged();
            }
        }

        private Address _location = new Address();
        public Address Location
        {
            get => _location;
            set
            {
                if (_location == value) return;
                _location = value;
                OnPropertyChanged();
            }
        }

        private string _finalOwner = string.Empty;
        public string FinalOwner
        {
            get => _finalOwner;
            set
            {
                if (_finalOwner == value) return;
                _finalOwner = value;
                OnPropertyChanged();
            }
        }

        private AnimalType _animal = new AnimalType();
        public AnimalType Animal
        {
            get => _animal;
            set
            {
                if (_animal == value) return;
                _animal = value;
                OnPropertyChanged();
            }
        }

        private PetHealth _health = new PetHealth();
        public PetHealth Health
        {
            get => _health;
            set
            {
                if (_health == value) return;
                _health = value;
                OnPropertyChanged();
            }
        }


        public PetViewModel()
        {
            Id = IdGenerator.Generate();
            _breed = string.Empty;
            _colour = string.Empty;
            _birthYear = DateTime.Now.Year;
            _ownership = PetOwnership.Stray;
            _status = PetAdoptionStatus.Pending;
            _gender = Gender.Male;
            _location = new Address();
            _finalOwner = string.Empty;
            _animal = new AnimalType("Cat");
            _health = new PetHealth("Healthy");
        }

        public PetViewModel(Pet petInfo)
        {
            Id = petInfo.Id;
            _breed = petInfo.Breed;
            _colour = petInfo.Colour;
            _birthYear = petInfo.BirthYear;
            _ownership = petInfo.Ownership;
            _status = petInfo.Status;
            _gender = petInfo.Gender;
            _location = petInfo.Location;
            _finalOwner = petInfo.FinalOwner;
            _animal = petInfo.Animal;
            _health = petInfo.Health;
        }

        public Pet ToPet(string id) => new Pet(id, Breed, Colour, BirthYear, Gender, Location, Animal, Health);

        public Pet ToPet() => new Pet(Breed, Colour, BirthYear, Gender, Location, Animal, Health);

        public Pet ToPet(PetViewModel pet) => new Pet(pet.Id, pet.Breed, pet.Colour, pet.BirthYear, pet.Ownership, pet.Status,
                                                      pet.Gender, pet.Location, pet.FinalOwner, pet.Animal, pet.Health, false);

        private readonly PetInputValidation _petValidation = new();
        private readonly AddressInputValidation _addressValidation = new();

        public virtual string this[string columnName]
        {
            get
            {
                return columnName switch
                {
                    "Breed" => _petValidation.ValidateBreed(Breed),
                    "Colour" => _petValidation.ValidateColour(Colour),
                    "BirthYear" => _petValidation.ValidateBirthYear(BirthYear),
                    "Street" => _addressValidation.ValidateStreet(Location.Street),
                    "StreetNo" => _addressValidation.ValidateStreet(Location.StreetNo),
                    "Town" => _addressValidation.ValidateTown(Location.Town),
                    _ => string.Empty
                };
            }
        }

        public string Error => string.Empty;

        private readonly string[] _validatedProperties = { "Breed", "Colour", "BirthYear", "Street", "StreetNo", "Town" };

        public bool IsValid => _validatedProperties.All(property => this[property] == string.Empty);
    }
}
