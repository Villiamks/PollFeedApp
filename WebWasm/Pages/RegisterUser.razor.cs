using WebWasm.Models;
using Microsoft.JSInterop;

namespace WebWasm.Pages;

public partial class RegisterUser
{
    private string Username = "";
    private string Email = "";
    private string Password = "";
    private string RepeatPass = "";

    private async Task HandleRegistration()
    {
        if (Username == "" || Password == "" || RepeatPass == "" || Email == "")
        {
            await js.InvokeAsync<object>("alert", new object[] { "All Fields are required" });
            return;
        }
        if (Password != RepeatPass)
        {
            await js.InvokeAsync<object>("alert", new object[] { "Passwords do not match" });
            return;
        }
        if ( await Exists(Username))
        {
            await js.InvokeAsync<object>("alert", new object[] { "Username already exists" });
            return;
        }
        await UserService.CreateUser(Username, Password, Email);
        nv.NavigateTo("login");
    }

    private async Task<bool> Exists(string username)
    {
        var usersList = await UserService.GetUsers();
        return usersList?.ToList().Any(u => u.UserName.ToLower() == username.ToLower()) ??  false;
    }
}
