
namespace Expences.Infraestrocture.Utils.PasswordHasher
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool Verification(string passwordHash, string inputPassword);
    }
}
