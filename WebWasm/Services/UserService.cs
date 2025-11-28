using System.Net.Http.Json;
using WebWasm.Models;

namespace WebWasm.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Users>?> GetUsers()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Users>>("api/user");
    }

    public async Task<Users?> GetUser(int id)
    {
        return await _httpClient.GetFromJsonAsync<Users>($"api/user/{id}");
    }

    public async Task CreateUser(string username, string password, string email)
    {
        string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
        string hash = BCrypt.Net.BCrypt.HashPassword(password, salt);
        Users user = new Users()
        {
            UserName = username,
            Email = email,
            PasswordHash = hash,
            Salt = salt,
        };

        await _httpClient.PostAsJsonAsync("api/user", user);
    }

    public async Task DeleteUser(int id)
    {
        await _httpClient.DeleteAsync($"api/user/{id}");
    }
}
