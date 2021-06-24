using LojaQualquer.Web.Application.Interfaces;
using LojaQualquer.Web.Application.Models.Request;
using LojaQualquer.Web.Application.Models.Response;
using System.Net.Http;
using System.Threading.Tasks;

namespace LojaQualquer.Web.Application
{
    public class LoginApplication : BaseApplication, ILoginApplication
    {
        public LoginApplication(HttpClient httpClient) : base(httpClient) { }

        public async Task<TokenResponse> PostAsync(LoginRequest request) 
        {
            var response = await _httpClient.PostAsync("/api/login", Content(request));

            if (response.IsSuccessStatusCode)
                return await Deserialize<TokenResponse>(response);

            return new TokenResponse { ResponseError = await Deserialize<ResponseError>(response) };
        }
    }
}