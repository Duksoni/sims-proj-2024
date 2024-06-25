using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;

namespace PetNetwork.Application.UseCases;

public class PetSocietyService
{
    private readonly IRepository<PetSociety> _petSocietyRepository;

    public PetSocietyService(IRepository<PetSociety> petSocietyRepository)
    {
        _petSocietyRepository = petSocietyRepository;
    }

    public void Add(PetSociety society)
    {
        if (Get() == null)
        {
            _petSocietyRepository.Add(society);
            return;
        }
        Update(society);
    }

    public void Update(PetSociety updatedSociety) => _petSocietyRepository.Update(updatedSociety);

    public PetSociety? Get() => _petSocietyRepository.GetAll().FirstOrDefault();
}