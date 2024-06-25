using Newtonsoft.Json;
using PetNetwork.Domain.Interfaces;

namespace PetNetwork.Domain.Models;

public class Address : ISerializable
{

    public string Id => $"{Street.Replace(' ', '-')}_{StreetNo}";

    public string Street { get; set; }

    public string StreetNo { get; set; }

    public string Town { get; set; }

    private int _postalCode = 11000;
    public int PostalCode
    {
        get => _postalCode;
        set
        {
            if (value is < 11000 or > 38299)
                throw new ArgumentException("Postal code must be in range [11000, 38300)");
            _postalCode = value;
        }
    }

    private bool _deleted;
    public bool Deleted
    {
        get => _deleted;
        set
        {
            if (!_deleted && value)
                _deleted = value;
        }
    }

    internal Address()
    {
        Street = string.Empty;
        StreetNo = string.Empty;
        Town = string.Empty;
    }

    public Address(string street, string streetNo, string town, int postalCode)
    {
        Street = street;
        StreetNo = streetNo;
        Town = town;
        PostalCode = postalCode;
    }

    [JsonConstructor]
    public Address(string street, string streetNo, string town, int postalCode, bool deleted)
    {
        Street = street;
        StreetNo = streetNo;
        Town = town;
        PostalCode = postalCode;
        _deleted = deleted;
    }
}