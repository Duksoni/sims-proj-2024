using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;

namespace PetNetwork.Domain.Models;

public class Person : ISerializable
{
    public string Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Phone { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public Gender Gender { get; set; }

    public Address Address { get; set; }

    public string IdentityCardNo { get; set; }

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

    internal Person()
    {
        Id = string.Empty;
        FirstName = string.Empty;
        LastName = string.Empty;
        Phone = string.Empty;
        Address = new Address();
        IdentityCardNo = string.Empty;
    }

    public Person(string id, string firstName, string lastName, string phone, Gender gender, Address address, string identityCardNo)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Gender = gender;
        Address = address;
        IdentityCardNo = identityCardNo;
    }

    [JsonConstructor]
    public Person(string id, string firstName, string lastName, string phone, Gender gender, Address address, string identityCardNo, bool deleted)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Gender = gender;
        Address = address;
        IdentityCardNo = identityCardNo;
        _deleted = deleted;
    }
}