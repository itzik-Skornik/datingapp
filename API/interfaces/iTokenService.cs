
using API.Entities;

namespace API.interfaces
{
    public interface iTokenService
    {
        string CreateToken(Appuser user);

        
    }
}