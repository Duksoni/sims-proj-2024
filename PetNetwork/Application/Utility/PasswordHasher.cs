using System.Security.Cryptography;
using System.Text;

namespace PetNetwork.Application.Utility;

public static class PasswordHasher
{
    public static string Hash(string password)
    {
        using var md5 = MD5.Create();
        var inputBytes = Encoding.UTF8.GetBytes(password);
        var hashedBytes = md5.ComputeHash(inputBytes);

        StringBuilder builder = new();
        foreach (var hashedByte in hashedBytes)
            builder.Append(hashedByte.ToString("x2"));
        return builder.ToString();
    }

    public static bool Verify(string inputtedPassword, string expectedPassword) => Hash(inputtedPassword) == expectedPassword;
}