using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetNetwork.WPF.ViewModels
{
    class AddAnimalTypeViewModel : BaseViewModel
    {
        public AnimalTypeViewModel AnimalType { get; set; }

        private readonly AnimalTypeService _animalTypeService;

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

        public AddAnimalTypeViewModel()
        {
            var animalTypeRepo = Injector.CreateInstance<IRepository<AnimalType>>();

            _animalTypeService = new AnimalTypeService(animalTypeRepo);

            AnimalType = new AnimalTypeViewModel();
        }

        public RelayCommand TryAddAnimalTypeCommand => new(_ => TryAddAnimalType(), _ => AnimalType.IsValid);

        private void TryAddAnimalType()
        {
            var animalTypeRepo = Injector.CreateInstance<IRepository<AnimalType>>();
            var _animalTypeService = new AnimalTypeService(animalTypeRepo);

            var animalTypes = _animalTypeService.GetAll(true);

            if (AnimalType.Id == string.Empty) return;

            foreach (var animalType in animalTypes)
                if (animalType.Id == AnimalType.Id) return;

            _animalTypeService.Add(AnimalType.ToAnimalType(AnimalType.Id));
            Added = true;
        }
    }
}
