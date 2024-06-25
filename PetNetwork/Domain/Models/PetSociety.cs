namespace PetNetwork.Domain.Models;

public class PetSociety
{
    public string Name { get; set; }

    public string BankAccount { get; set; }

    public PetSociety(string name, string bankAccount)
    {
        Name = name;
        BankAccount = bankAccount;
    }

}
