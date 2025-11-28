using ClassLibrary;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace WebFront.Services;
using ClassLibrary.DTOs;
using System.Net.Http.Headers;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly ProtectedSessionStorage _sessionStorage;
    
    public UserService(HttpClient httpClient, ProtectedSessionStorage sessionStorage)
    {
        _httpClient = httpClient;
        _sessionStorage = sessionStorage;
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
        await SetAuthHeader();
        return await _httpClient.GetFromJsonAsync<Users>($"api/user/{id}");
    }

    public Task CreateUser(string userName, string password, string email)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteUser(int id)
    {
        await SetAuthHeader();
        await _httpClient.DeleteAsync($"api/user/{id}");
    }
    
    private async Task SetAuthHeader()
    {
        try
        {
            // Retrieve the token from the same storage your CustomAuthProvider uses
            var result = await _sessionStorage.GetAsync<string>("authToken");

            if (result.Success && !string.IsNullOrEmpty(result.Value))
            {
                // Attach the token to the Authorization header
                _httpClient.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Bearer", result.Value);
            }
        }
        catch
        {
            // This might fail during server-side prerendering (when JS isn't available).
            // We swallow the error so the app doesn't crash, 
            // though the API call will likely fail with 401 if it happens.
        }
    }
}