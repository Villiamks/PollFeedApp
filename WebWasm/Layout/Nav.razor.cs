namespace WebWasm.Layout;

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
        await JSRuntime.InvokeAsync<object>("localStorage.removeItem", new object[] { "sessionToken" });
        nv.NavigateTo("login");
    }

    private bool hasRendered = false;

    protected override void OnInitialized()
    {
        showNav = false;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && !hasRendered)
        {
            hasRendered = true;

            // Check if user is logged in
            string? sessionToken = await JSRuntime.InvokeAsync<string?>("localStorage.getItem", new object[] { "sessionToken" });
            isLoggedIn = await LoginService.IsLoggedIn(sessionToken);
            StateHasChanged();
        }
    }
}
