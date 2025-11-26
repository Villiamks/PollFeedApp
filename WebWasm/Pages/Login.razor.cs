using WebWasm.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WebWasm.Services;

namespace WebWasm.Pages;

public partial class Login
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

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

            // Store in localStorage (WebAssembly uses localStorage instead of Valkey)
            await LoginService.Login(user, sessionToken);

            // Store token in browser localStorage
            await JSRuntime.InvokeAsync<object>("localStorage.setItem", new object[] { "sessionToken", sessionToken });

            nv.NavigateTo("");
        }
        else
        {
            await js.InvokeAsync<object>("alert", new object[] { "Wrong username or password" });
        }
    }

    private async Task ToNewUser()
    {
        nv.NavigateTo("RegisterUser");
    }
}
