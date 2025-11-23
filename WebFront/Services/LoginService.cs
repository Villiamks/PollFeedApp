using ClassLibrary;
using StackExchange.Redis;
using System.Text.Json;

namespace WebFront.Services;

public class LoginService : ILoginService
{
    private readonly IConnectionMultiplexer _redis;
    private readonly IUserService _userService;
    private string? _sessionToken;

    public LoginService(IConnectionMultiplexer redis, IUserService userService)
    {
        _redis = redis;
        _userService = userService;
    }

    public async Task Login(Users user, string sessionToken)
    {
        _sessionToken = sessionToken;
        var db = _redis.GetDatabase();

        // Store userId in Valkey with session token as key, expire after 30 minutes
        await db.StringSetAsync($"session:{sessionToken}", user.UserId, TimeSpan.FromMinutes(30));
    }

    public async Task Logout()
    {
        if (_sessionToken != null)
        {
            var db = _redis.GetDatabase();
            await db.KeyDeleteAsync($"session:{_sessionToken}");
            _sessionToken = null;
        }
    }

    public async Task<bool> IsLoggedIn(string? sessionToken)
    {
        if (string.IsNullOrEmpty(sessionToken))
            return false;

        var db = _redis.GetDatabase();
        var userId = await db.StringGetAsync($"session:{sessionToken}");
        return userId.HasValue;
    }

    public async Task<Users?> GetLoggedinnUser(string sessionToken)
    {
        var db = _redis.GetDatabase();
        var userId = await db.StringGetAsync($"session:{sessionToken}");

        if (!userId.HasValue)
            return null;

        _sessionToken = sessionToken;

        // Fetch user from API
        var users = await _userService.GetUsers();
        return users?.FirstOrDefault(u => u.UserId == (int)userId);
    }
}