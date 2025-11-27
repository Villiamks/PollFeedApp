using ClassLibrary.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization; // Needed
using Microsoft.JSInterop;
using WebFront.Services;

namespace WebFront.Components.Pages;

public partial class Login
{
    // Inject the AuthenticationStateProvider
    [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; } = default!;
    
    private string username = "";
    private string password = "";

    private async Task HandleLogin()
    {
        var loginDto = new LoginDTO { Username = username, Password = password };
        
        // 1. Call API
        var token = await UserService.LoginUser(loginDto);

        if (!string.IsNullOrEmpty(token))
        {
            // 2. Notify Auth Provider
            var customProvider = (CustomAuthProvider)AuthStateProvider;
            await customProvider.Login(token);
            
            // 3. Navigate
            nv.NavigateTo("");
        }
        else
        {
            await js.InvokeVoidAsync("alert", "Invalid credentials");
        }
    }

    private void ToNewUser()
    {
        nv.NavigateTo("RegisterUser");
    }
}