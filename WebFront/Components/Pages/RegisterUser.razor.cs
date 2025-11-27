using ClassLibrary;
using ClassLibrary.DTOs;
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
        if (Password != RepeatPass)
        {
            await js.InvokeVoidAsync("alert", "Passwords don't match");
            return;
        }

        var dto = new RegisterDTO
        {
            Username = Username,
            Email = Email,
            Password = Password
        };

        var success = await UserService.RegisterUser(dto);

        if (success)
        {
            await js.InvokeVoidAsync("alert", "Success! Please login.");
            nv.NavigateTo("/login");
        }
        else
        {
            await js.InvokeVoidAsync("alert", "Registration failed. Please try again.");
        }
    }

    private async Task<bool> Exists(string username)
    {
        var usersList = await UserService.GetUsers();
        return usersList?.ToList().Any(u => u.UserName.ToLower() == username.ToLower()) ??  false;
    }
}