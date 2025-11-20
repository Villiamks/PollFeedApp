namespace WebFront.Components.Pages;

public partial class Login
{
    private string username = "";
    private string password = "";

    private async Task HandleLogin()
    {
        //TODO

        nv.NavigateTo("");
    }

    private async Task ToNewUser()
    {
        nv.NavigateTo("RegisterUser");
    }
}