using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;

namespace PetNetwork.Application.UseCases;

public class PetService
{
    private readonly IRepository<Pet> _petRepository;


    public PetService(IRepository<Pet> petRepository)
    {
        _petRepository = petRepository;
    }

    public void Add(Pet pet) => _petRepository.Add(pet);

    public void Update(Pet pet) => _petRepository.Update(pet);

    public Pet? Get(string id) => _petRepository.Get(id);

    public IList<Pet> GetAll(bool includeRemoved = false) => _petRepository.GetAll(includeRemoved);
}