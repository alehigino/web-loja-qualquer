using LojaQualquer.Web.Application.Models.Request;
using LojaQualquer.Web.Application.Models.Response;
using System.Threading.Tasks;

namespace LojaQualquer.Web.Application.Interfaces
{
    public interface ILoginApplication
    {
        Task<TokenResponse> PostAsync(LoginRequest request);
    }
}