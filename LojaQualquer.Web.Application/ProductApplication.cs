using LojaQualquer.Web.Application.Interfaces;
using LojaQualquer.Web.Application.Models.Request;
using LojaQualquer.Web.Application.Models.Response;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LojaQualquer.Web.Application
{
    public class ProductApplication : BaseApplication, IProductApplication
    {
        public ProductApplication(HttpClient httpClient, IUser user) : base(httpClient, user) { }
        public async Task<IList<ProductResponse>> GetByFilterAsync(FilterProductRequest filter)
        {
            var response = await _httpClient.GetAsync("/api/product" + BuildQuery(filter));

            if (response.IsSuccessStatusCode)
                return await Deserialize<IList<ProductResponse>>(response);

            return new List<ProductResponse>
            {
                new ProductResponse {
                    StatusCode = (int)response.StatusCode,
                    ResponseError = (int)response.StatusCode == 400
                        ? await Deserialize<ResponseError>(response)
                        : null
                }
            };
        }

        public async Task<ProductCreateResponse> PostAsync(ProductCreateUpdateRequest request)
        {
            var response = await _httpClient.PostAsync("/api/product", Content(request));

            if (response.IsSuccessStatusCode)
                return await Deserialize<ProductCreateResponse>(response);

            return new ProductCreateResponse
            {
                StatusCode = (int)response.StatusCode,
                ResponseError = (int)response.StatusCode == 400
                    ? await Deserialize<ResponseError>(response)
                    : null
            };
        }

        public async Task<ProductResponse> GetByIdAsync(int productId)
        {
            var response = await _httpClient.GetAsync($"/api/product/{productId}");

            if (response.IsSuccessStatusCode)
                return await Deserialize<ProductResponse>(response);

            return new ProductResponse
            {
                StatusCode = (int)response.StatusCode,
                ResponseError = (int)response.StatusCode == 400
                    ? await Deserialize<ResponseError>(response)
                    : null
            };
        }

        public async Task<BaseResponse> PutAsync(int productId, ProductCreateUpdateRequest request)
        {
            var response = await _httpClient.PutAsync($"/api/product/{productId}", Content(request));

            if (response.IsSuccessStatusCode)
                return new BaseResponse
                {
                    StatusCode = (int)response.StatusCode
                };

            return new BaseResponse
            {
                StatusCode = (int)response.StatusCode,
                ResponseError = (int)response.StatusCode == 400
                    ? await Deserialize<ResponseError>(response)
                    : null
            };
        }
    }
}