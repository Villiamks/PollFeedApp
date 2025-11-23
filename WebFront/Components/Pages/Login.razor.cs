using ClassLibrary;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.JSInterop;
using WebFront.Services;

namespace WebFront.Components.Pages;

public partial class Login
{
    [Inject] private ProtectedSessionStorage SessionStorage { get; set; } = default!;

    private string username = "";
    private string password = "";

    private async Task HandleLogin()
    {
        var userList = await UserService.GetUsers();
        Users user = userList.ToList().FirstOrDefault(u => u.UserName == username);

        if (user != null && BCrypt.Net.BCrypt.HashPassword(password, user.Salt) == user.PasswordHash)
        {
            // Generate session token
            string sessionToken = Guid.NewGuid().ToString();

            // Store in Valkey
            await LoginService.Login(user, sessionToken);

            // Store token in browser session storage
            await SessionStorage.SetAsync("sessionToken", sessionToken);

            nv.NavigateTo("");
        }
        else
        {
            await js.InvokeVoidAsync("alert", "Wrong username or password");
        }
    }

    private async Task ToNewUser()
    {
        nv.NavigateTo("RegisterUser");
    }
}