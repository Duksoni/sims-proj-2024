using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;

namespace PetNetwork.WPF.ViewModels;

public class AdminViewModel
{
    private readonly PetSocietyService _petSocietyService;

    private readonly UserService _userService;

    public PetSocietyViewModel Society { get; }

    public bool CanAddVolunteer { get; }

    public RelayCommand RegisterSocietyCommand => new(_ => RegisterSociety());

    public AdminViewModel()
    {
        var accountRepo = Injector.CreateInstance<IRepository<UserAccount>>();
        var personRepo = Injector.CreateInstance<IRepository<Person>>();
        _userService = new UserService(accountRepo, personRepo);
        _petSocietyService = new PetSocietyService(Injector.CreateInstance<IRepository<PetSociety>>());
        var createdSociety = _petSocietyService.Get();
        Society = createdSociety == null ? new PetSocietyViewModel() : new PetSocietyViewModel(createdSociety);
        CanAddVolunteer = _userService.FindUserAccounts(AccountRole.Volunteer).Count == 0;
    }

    private void RegisterSociety()
    {
        if (Society.IsValid)
            _petSocietyService.Add(Society.ToPetSociety());
    }
}