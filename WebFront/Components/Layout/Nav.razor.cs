namespace WebFront.Components.Layout;

public partial class Nav
{
    private bool showNav;
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
        //TODO 
    }

    protected async override Task OnInitializedAsync()
    {
        showNav = false;
    }
}