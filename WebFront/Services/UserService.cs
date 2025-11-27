using ClassLibrary;

namespace WebFront.Services;
using ClassLibrary.DTOs;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    
    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> RegisterUser(RegisterDTO registerDTO)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Auth/register",  registerDTO);
        return response.IsSuccessStatusCode;
    }

    public async Task<string?> LoginUser(LoginDTO loginDTO)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Auth/login", loginDTO);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return result?.Token;
        }
        return null;
    }

    private class LoginResponse
    {
        public string Token { get; set; }
    }
    
    public async Task<IEnumerable<Users>?> GetUsers()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Users>>("api/user");
    }

    public async Task<Users?> GetUser(int id)
    {
        return await _httpClient.GetFromJsonAsync<Users>($"api/user/{id}");
    }

    public Task CreateUser(string userName, string password, string email)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteUser(int id)
    {
        await _httpClient.DeleteAsync($"api/user/{id}");
    }
}