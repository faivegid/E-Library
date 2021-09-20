using GBLAC.Models;
using System.Threading.Tasks;

namespace GBLAC.Services.TokenGeneration.Interface
{
    public interface ITokenGenerator
    {
        Task<string> CreateTokenAsync(AppUser appUser);
    }
}