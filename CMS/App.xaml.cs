namespace CMS;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new MainPage();
    }
    protected override async void OnStart()
    {
        base.OnStart();

        var filePath = Path.Combine(FileSystem.AppDataDirectory, "settings.json");

        await Task.Delay(100);

        if (File.Exists(filePath))
        {
            await Shell.Current.GoToAsync("//Repo");
        }
        else
        {
            await Shell.Current.GoToAsync("//Setup");
        }
    }
}