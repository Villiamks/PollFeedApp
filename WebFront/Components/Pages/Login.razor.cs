using ClassLibrary;
using Microsoft.JSInterop;
using WebFront.Services;

namespace WebFront.Components.Pages;

public partial class Login
{
    private string username = "";
    private string password = "";

    private async Task HandleLogin()
    {
        var userList = await UserService.GetUsers();
        Users user = userList.ToList().FirstOrDefault(u => u.UserName == username);

        if (user != null && BCrypt.Net.BCrypt.HashPassword(password, user.Salt) == user.PasswordHash)
        {
            LoginService.Login(user);
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