using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Models;

namespace PetNetwork.WPF.ViewModels;

public class PersonInfoViewModel : BaseViewModel
{
    public string Email { get; }

    public AccountRole Role { get; }

    public AccountStatus Status { get; }

    public string FirstName { get; }

    public string LastName { get; }

    public Gender Gender { get; }

    public string Phone { get; }

    public string IdentityCardNo { get; }

    public AddressViewModel Address { get; }

    public PersonInfoViewModel(UserAccount account, Person person)
    {
        if (account.Id != person.Id)
            throw new ArgumentException($"Mismatched accounts: Account mail: {account.Id}, Person mail: {person.Id}");
        Email = account.Id;
        Role = account.Role;
        Status = account.Status;
        FirstName = person.FirstName;
        LastName = person.LastName;
        Gender = person.Gender;
        Phone = person.Phone;
        IdentityCardNo = person.IdentityCardNo;
        Address = new AddressViewModel(person.Address);
    }
}