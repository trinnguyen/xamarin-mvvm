using Android.App;
using Android.Widget;
using Android.OS;

namespace SampleAutofac.Droid
{
	[Activity(Label = "SampleAutofac", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			//Init application with Autofac Module for platform specific
			SampleApp.App.Init(new DroidAutofacModule());
		}
	}
}

