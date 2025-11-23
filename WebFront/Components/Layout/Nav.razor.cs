namespace WebFront.Components.Layout;

public partial class Nav
{
    private bool showNav;
    private bool isLoggedIn;
    private String PollsUrl = "";
    private String NewPollUrl = "/newpoll";
    private String LoginUrl = "/login";

    private async Task ToggleNav()
    {
        showNav = !showNav;
    }

    private async Task NavigateTo(String url)
    {
        await ToggleNav();
        nv.NavigateTo(url);
    }

    private async Task Loggout()
    {
        await LoginService.Logout();
        await SessionStorage.DeleteAsync("sessionToken");
        nv.NavigateTo("login");
    }

    protected async override Task OnInitializedAsync()
    {
        showNav = false;

        // Check if user is logged in
        var tokenResult = await SessionStorage.GetAsync<string>("sessionToken");
        string? sessionToken = tokenResult.Success ? tokenResult.Value : null;
        isLoggedIn = await LoginService.IsLoggedIn(sessionToken);
    }
}