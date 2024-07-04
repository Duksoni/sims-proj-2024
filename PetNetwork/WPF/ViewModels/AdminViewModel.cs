using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;

namespace PetNetwork.WPF.ViewModels;

public class AdminViewModel
{
    private readonly PetSocietyService _petSocietyService;

    public PetSocietyViewModel Society { get; }

    public bool CanAddVolunteer { get; }

    public RelayCommand RegisterSocietyCommand => 
        new(_ => _petSocietyService.Add(Society.ToPetSociety()), _ => Society.IsValid);

    public AdminViewModel()
    {
        var accountRepo = Injector.CreateInstance<IRepository<UserAccount>>();
        var personRepo = Injector.CreateInstance<IRepository<Person>>();
        var userService = new UserService(accountRepo, personRepo);
        _petSocietyService = new PetSocietyService(Injector.CreateInstance<IRepository<PetSociety>>());
        var createdSociety = _petSocietyService.Get();
        Society = createdSociety == null ? new PetSocietyViewModel() : new PetSocietyViewModel(createdSociety);
        CanAddVolunteer = userService.FindUserAccounts(AccountRole.Volunteer).Count == 0;
    }
}