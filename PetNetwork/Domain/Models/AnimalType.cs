namespace PetNetwork.Domain.Models
{
    internal class AnimalType
    {
        public string Type { get; set; }

        internal AnimalType()
        {
            Type = string.Empty;
        }

        public AnimalType(string type)
        {
            Type = type;
        }
    }
}
