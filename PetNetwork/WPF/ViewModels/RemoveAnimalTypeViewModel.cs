using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetNetwork.WPF.ViewModels
{
    class RemoveAnimalTypeViewModel : BaseViewModel
    {
        public AnimalTypeViewModel AnimalType { get; set; }

        public ObservableCollection<AnimalType> AnimalTypes { get; }

        private readonly AnimalTypeService _animalTypeService;

        private bool _removed;
        public bool Removed
        {
            get => _removed;
            private set
            {
                _removed = value;
                OnPropertyChanged();
            }
        }

        public RemoveAnimalTypeViewModel()
        {
            var animalTypeRepo = Injector.CreateInstance<IRepository<AnimalType>>();

            _animalTypeService = new AnimalTypeService(animalTypeRepo);

            AnimalType = new AnimalTypeViewModel();
            AnimalTypes = new(_animalTypeService.GetAll(false));
        }

        public RelayCommand TryRemoveAnimalTypeCommand => new(_ => TryRemoveAnimalType());

        private void TryRemoveAnimalType()
        {
            _animalTypeService.Remove(AnimalType.Id);
            Removed = true;
        }
    }
}
