using LojaQualquer.Web.Application.Interfaces;
using LojaQualquer.Web.Application.Models.Request;
using LojaQualquer.Web.Application.Models.Response;
using System.Net.Http;
using System.Threading.Tasks;

namespace LojaQualquer.Web.Application
{
    public class LoginApplication : BaseApplication, ILoginApplication
    {
        public LoginApplication(HttpClient httpClient, IUser user) : base(httpClient, user) { }

        public async Task<TokenResponse> PostAsync(LoginRequest request) 
        {
            var response = await _httpClient.PostAsync("/api/login", Content(request));

            if (response.IsSuccessStatusCode)
            {
                return await Deserialize<TokenResponse>(response);
            }

            return new TokenResponse
            {
                StatusCode = (int)response.StatusCode,
                ResponseError = (int)response.StatusCode == 400
                    ? await Deserialize<ResponseError>(response)
                    : null
            };
        }
    }
}