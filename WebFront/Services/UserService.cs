using ClassLibrary;

namespace WebFront.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    
    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<IEnumerable<Users>?> GetUsers()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Users>>("api/users");
    }

    public async Task<Users?> GetUser(int id)
    {
        return await _httpClient.GetFromJsonAsync<Users>($"api/user/{id}");
    }

    public async Task CreateUser(Users user)
    {
        await _httpClient.PostAsJsonAsync("api/users", user);
    }
    
    public async Task DeleteUser(int id)
    {
        await _httpClient.DeleteAsync($"api/user/{id}");
    }
}