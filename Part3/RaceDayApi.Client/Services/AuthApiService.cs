using RaceDayApi.Client.Models;
using static System.Net.WebRequestMethods;

namespace RaceDayApi.Client.Services
{
    public class AuthApiService
    {
        private readonly HttpClient _http;

        public AuthApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<LoginResponse?> LoginAsync(LoginModel model)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", model);
            if (!response.IsSuccessStatusCode)
                return null;
            return await response.Content.ReadFromJsonAsync<LoginResponse>();
        }
    }
}


