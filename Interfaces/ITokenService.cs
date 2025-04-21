using DATINGAPP.entities;

namespace DATINGAPP.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);

    }
}
