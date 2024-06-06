using System.Net.Http.Json;
using Blog.Common.Dtos;
using Microsoft.AspNetCore.Components;

namespace Blog_Client.Pages
{
    public partial class Index : ComponentBase
    {
        private readonly HttpClient _client = new HttpClient();
        private List<UserDto>? _users = new List<UserDto>();
  
        protected override  async Task OnInitializedAsync()
        {
            _users = await _client.GetFromJsonAsync<List<UserDto>>("https://localhost:7246/api/Users");
        }
    }
}
