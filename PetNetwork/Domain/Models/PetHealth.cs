using PetNetwork.Domain.Enums;

namespace PetNetwork.Domain.Models
{
    internal class PetHealth
    {
        public string Description { get; set; }

        public PetHealthCondition Condition { get; set; }


        internal PetHealth()
        {
            Description = string.Empty;
            Condition = PetHealthCondition.Healthy;
        }

        public PetHealth(string description, PetHealthCondition condition)
        {
            Description = description;
            Condition = condition;
        }
    }
}
