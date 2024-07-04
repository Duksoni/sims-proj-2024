using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;

namespace PetNetwork.Application.UseCases
{
    class AnimalTypeService
    {
        private readonly IRepository<AnimalType> _animalTypeRepository;


        public AnimalTypeService(IRepository<AnimalType> animalTypeRepository)
        {
            _animalTypeRepository = animalTypeRepository;
        }


        public void Add(AnimalType type) => _animalTypeRepository.Add(type);

        public void Remove(string id) => _animalTypeRepository.Remove(id);

        public void Update(AnimalType type) => _animalTypeRepository.Update(type);

        public AnimalType? Get(string id) => _animalTypeRepository.Get(id);

        public IList<AnimalType> GetAll(bool includeRemoved = false) => _animalTypeRepository.GetAll(includeRemoved);
    }
}
