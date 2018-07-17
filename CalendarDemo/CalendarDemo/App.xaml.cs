using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace CalendarDemo
{
	public partial class App : Application
	{
	    public App()
	    {
	        InitializeComponent();

	        var page = new MainPage()
	        {
	            BindingContext = new MainViewModel()
	        };

	        MainPage = page;
	    }

	    protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
