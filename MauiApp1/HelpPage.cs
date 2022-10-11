namespace PingWall;

public class HelpPage : ContentPage
{
	public HelpPage()
	{
        
        Content = new WebView
        {
            Source = "help.html",
            
        };
        
        var backToolbarItem = new ToolbarItem
        {
            Priority = 1,
            Order=ToolbarItemOrder.Primary,
            IconImageSource="back.png",
            Text="Go Back"
        };
        backToolbarItem.Clicked += async (sender, args) => { await Navigation.PopAsync(); };
        ToolbarItems.Add(
            backToolbarItem
            );
    }
}