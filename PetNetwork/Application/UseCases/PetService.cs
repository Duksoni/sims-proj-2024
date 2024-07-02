using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;

namespace PetNetwork.Application.UseCases
{
    internal class PetService
    {
        private readonly IRepository<Pet> _petRepository;


        public PetService(IRepository<Pet> petRepository)
        {
            _petRepository = petRepository;
        }


        public void AddPet(Pet pet)
        {
            _petRepository.Add(pet);
        }

        public void DeletePet(Pet pet)
        {
            _petRepository.Remove(pet.Id);
        }

        public void UpdatePet(Pet pet)
        {
            Pet? oldPet = _petRepository.Get(pet.Id);

            if (oldPet == null) return;

            _petRepository.Update(pet);
        }

        public Pet? GetPet(string id)
        {
            return _petRepository.Get(id);
        }

        public IList<Pet> GetAllPets()
        {
            return _petRepository.GetAll();
        }

        public IList<Pet> GetNonDeletedPets()
        {
            IList<Pet> currentPets = new List<Pet>();
            IList<Pet> allPets = GetAllPets();

            foreach (var pet in allPets)
            {
                if (!pet.Deleted) currentPets.Add(pet);
            }

            return currentPets;
        }
    }
}
