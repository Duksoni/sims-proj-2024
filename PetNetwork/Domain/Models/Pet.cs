﻿using Newtonsoft.Json.Converters;
using PetNetwork.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PetNetwork.Domain.Interfaces;

namespace PetNetwork.Domain.Models
{
    internal class Pet : ISerializable
    {
        public string Id { get; set; }

        public string Breed { get; set; }

        public string Colour { get; set; }

        public int BirthYear { get; set; }

        public PetOwnership Ownership { get; set; }

        public PetAdoptionStatus Status { get; set; }

        public Gender Gender { get; set; }

        public Address Location { get; set; }

        public UserAccount? FinalOwner { get; set; }

        public AnimalType Animal {  get; set; }

        public PetHealth Health { get; set; }

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


        internal Pet()
        {
            Id = string.Empty;
            Breed = string.Empty;
            Colour = string.Empty;
            BirthYear = -1;
            Ownership = PetOwnership.Stray; // null?
            Status = PetAdoptionStatus.Pending; // null?
            Gender = Gender.Male;
            Location = new Address(); // null?
            FinalOwner = null; // null?
            Animal = new AnimalType();
            Health = new PetHealth();
        }

        public Pet(string id, string breed, string colour, int birthYear, PetOwnership ownership, PetAdoptionStatus status, Gender gender, Address location, UserAccount? finalOwner, AnimalType animal, PetHealth health)
        {
            Id = id;
            Breed = breed;
            Colour = colour;
            BirthYear = birthYear;
            Ownership = ownership;
            Status = status;
            Gender = gender;
            Location = location;
            FinalOwner = finalOwner;
            Animal = animal;
            Health = health;
        }

        [JsonConstructor]
        public Pet(string id, string breed, string colour, int birthYear, PetOwnership ownership, PetAdoptionStatus status, Gender gender, Address location, UserAccount? finalOwner, AnimalType animal, PetHealth health, bool deleted)
        {
            Id = id;
            Breed = breed;
            Colour = colour;
            BirthYear = birthYear;
            Ownership = ownership;
            Status = status;
            Gender = gender;
            Location = location;
            FinalOwner = finalOwner;
            Animal = animal;
            Health = health;
            _deleted = deleted;
        }
    }
}
