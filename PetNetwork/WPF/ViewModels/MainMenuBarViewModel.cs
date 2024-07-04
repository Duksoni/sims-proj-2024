using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using System.Windows;

namespace PetNetwork.WPF.ViewModels;

public class MainMenuBarViewModel
{
    public PetSocietyViewModel PetSociety { get; }

    public RelayCommand LoginCommand => new(_ => Login(), _ => UserSession.Session == null);

    public RelayCommand LogoutCommand => new(_ => Logout(), _ => UserSession.Session != null);

    public RelayCommand CopyBankAccountToClipboardCommand => new(_ => Clipboard.SetText(PetSociety.BankAccount), 
        _ => PetSociety.BankAccount != string.Empty);

    public RelayCommand CopySocietyNameToClipboardCommand => new(_ => Clipboard.SetText(PetSociety.Name),
        _ => PetSociety.Name != string.Empty);

    public MainMenuBarViewModel()
    {
        var petSocietyService = new PetSocietyService(Injector.CreateInstance<IRepository<PetSociety>>());
        var society = petSocietyService.Get();
        PetSociety = society == null ? new PetSocietyViewModel() : new PetSocietyViewModel(society);
    }

    private static void Login()
    {
        var app = System.Windows.Application.Current as App;
        app?.ResetMainWindow();
    }

    private static void Logout()
    {
        UserSession.Stop();
        var app = System.Windows.Application.Current as App;
        app?.ResetMainWindow();
    }
}