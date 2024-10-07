namespace Application.AdditionalLogic;

public static class PasswordHasher
{
    public static string Generate(string password) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword(password);

    public static bool Verify(string password, string hashedPassword)
    {
        try
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
        }
        catch
        {
            return false;
        }
    }
}