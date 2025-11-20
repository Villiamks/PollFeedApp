using ClassLibrary;
using Microsoft.JSInterop;

namespace WebFront.Components.Pages;

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
            await js.InvokeVoidAsync("alert", "All Fields are required");
            return;
        }
        if (Password != RepeatPass)
        {
            await js.InvokeVoidAsync("alert", "Passwords do not match");
            return;
        } 
        if ( await Exists(Username))
        {
            await js.InvokeVoidAsync("alert", "Username already exists");
            return;
        }
        await UserService.CreateUser(Username, Password, Email);
        nv.NavigateTo("login");
    }

    private async Task<bool> Exists(string username)
    {
        var usersList = await UserService.GetUsers();
        return usersList?.ToList().Any(u => u.UserName == username) ??  false;
    }
}