using BC = BCrypt.Net.BCrypt;

public static class UserHelpers
{

    public static string HashPassword(string password) => BC.HashPassword(password);

    public static bool Verify(string password, string passwordHash) => BC.Verify(password, passwordHash);
}
