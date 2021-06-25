using LojaQualquer.Web.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LojaQualquer.Web.Application
{
    public class BaseApplication
    {
        protected readonly HttpClient _httpClient;

        public BaseApplication(HttpClient httpClient, IUser user)
        {
            httpClient.BaseAddress = new Uri("https://localhost:44346");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {user.Token}");

            _httpClient = httpClient;
        }

        public StringContent Content<T>(T obj)
        {
            return new StringContent(
                JsonSerializer.Serialize(obj),
                Encoding.UTF8,
                "application/json");
        }

        public async Task<T> Deserialize<T>(HttpResponseMessage response)
        {
            return JsonSerializer.Deserialize<T>(
                await response.Content.ReadAsStringAsync(), 
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        public string BuildQuery(object parameters)
        {
            if (parameters == null) return "";

            var json = JsonSerializer.Serialize(parameters);
            var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

            var query = "?";

            foreach (var prop in dictionary)
            {
                if (prop.Value != null) query += $"{prop.Key}={prop.Value}&";
            }

            return query;
        }
    }
}