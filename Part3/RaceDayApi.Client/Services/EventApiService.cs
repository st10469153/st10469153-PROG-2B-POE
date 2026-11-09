using System.Net.Http.Json;
using RaceDayApi.Client.Models;

namespace RaceDayApi.Client.Services
{
    public class EventApiService
    {
            private readonly HttpClient _http;
            public EventApiService(IHttpClientFactory factory)
            {
                _http = factory.CreateClient("CourseRegistrationApi");
            }
            public async Task<List<EventDTO>> GetEventsAsync()
            {
                return await _http.GetFromJsonAsync<List<EventDTO>>("api/events")
                ?? new List<EventDTO>();
            }
        public async Task<bool> CreateEventAsync(EventDTO model)
        {
            var response = await _http.PostAsJsonAsync("api/events", model);
            return response.IsSuccessStatusCode;
        }
    }
}
