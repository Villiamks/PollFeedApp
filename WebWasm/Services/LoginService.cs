using System.Net.Http.Json;
using WebWasm.Models;
using Microsoft.JSInterop;
using System.Text.Json;

namespace WebWasm.Services;

public class LoginService : ILoginService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly IUserService _userService;
    private string? _sessionToken;

    public LoginService(IJSRuntime jsRuntime, IUserService userService)
    {
        _jsRuntime = jsRuntime;
        _userService = userService;
    }

    public async Task Login(Users user, string sessionToken)
    {
        _sessionToken = sessionToken;
        // Store userId in browser localStorage
        await _jsRuntime.InvokeAsync<object>("localStorage.setItem", new object[] { $"session:{sessionToken}", user.UserId.ToString() });
    }

    public async Task Logout()
    {
        if (_sessionToken != null)
        {
            await _jsRuntime.InvokeAsync<object>("localStorage.removeItem", new object[] { $"session:{_sessionToken}" });
            _sessionToken = null;
        }
    }

    public async Task<bool> IsLoggedIn(string? sessionToken)
    {
        if (string.IsNullOrEmpty(sessionToken))
            return false;

        var userIdStr = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", new object[] { $"session:{sessionToken}" });
        return !string.IsNullOrEmpty(userIdStr);
    }

    public async Task<Users?> GetLoggedinnUser(string sessionToken)
    {
        var userIdStr = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", new object[] { $"session:{sessionToken}" });

        if (string.IsNullOrEmpty(userIdStr))
            return null;

        _sessionToken = sessionToken;

        // Fetch user from API
        var users = await _userService.GetUsers();
        return users?.FirstOrDefault(u => u.UserId == int.Parse(userIdStr));
    }
}
