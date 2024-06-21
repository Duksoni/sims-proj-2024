namespace PetNetwork.Application.Utility;

public static class IdGenerator
{
    public static string Generate() => Guid.NewGuid().ToString();
}