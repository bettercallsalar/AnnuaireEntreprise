namespace AnnuaireEntreprise.Maui;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		Console.WriteLine("Application starting.");
		// The root page 
		MainPage = new NavigationPage(new MainPage());
	}
}
