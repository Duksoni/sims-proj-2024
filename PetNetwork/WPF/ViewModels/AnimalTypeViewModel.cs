using PetNetwork.Application.Utility;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using System.ComponentModel;
using PetNetwork.Application.UseCases;

namespace PetNetwork.WPF.ViewModels
{
    class AnimalTypeViewModel : BaseViewModel, IDataErrorInfo
    {
        public string Id { get; set; } = string.Empty;

        public AnimalTypeViewModel()
        {
            Id = string.Empty;
        }

        public AnimalTypeViewModel(AnimalType animalTypeInfo)
        {
            Id = animalTypeInfo.Id;
        }

        public AnimalType ToAnimalType(string type) => new AnimalType(type);

        public AnimalType ToAnimalType() => new AnimalType();

        public virtual string this[string columnName]
        {
            get
            {
                return columnName switch
                {
                    _ => string.Empty
                };
            }
        }

        public string Error => string.Empty;

        public bool IsValid => IsValidId(Id);

        bool IsValidId(string id)
        {
            var animalTypeRepo = Injector.CreateInstance<IRepository<AnimalType>>();
            var _animalTypeService = new AnimalTypeService(animalTypeRepo);

            var animalTypes = _animalTypeService.GetAll(true);

            return (animalTypes.Contains(ToAnimalType(id)) && Id != string.Empty) ? false : true;
        }
    }
}
 