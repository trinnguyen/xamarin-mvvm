# Xamarin Mvvm and Autofac
Tiny Mvvm and Autofac for Xamarin cross-platform (native Xamarin.iOS/Xamarin.Android/PCL)

- [x] PCL Core with Autofac as DI and IoC container
- [x] Default Logger with System.Diagnostics.Debug
- [x] Basic mobile app components include: Presenter, ViewMode, View
- [x] Sample Xamarin native application

# Sample

## Core
Create new singleton class as default App instance
```
namespace SampleAutofac
{
	public class SampleApp : Mvvm.AppBase
	{
		private static SampleApp _app;
		public static SampleApp App
		{
			get 
			{
				_app = _app ?? new SampleApp();
				return _app;
			}
		}

		protected override void RegisterCoreComponents(ContainerBuilder builder)
		{
			
		}

		protected override void Startup()
		{
			Presenter.Show<FirstViewModel>();	
		}
	}
}
```
## Xamarin.iOS application
Init application from AppDelegate.cs with Autofac Module of iOS platform specific registrations

```
public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
{
    //Init application with Autofac Module for platform specific
    SampleApp.App.Init(new IosAutofacModule());

    return true;
}
```

## Xamarin.Android application
Init from OnCreate of the first Activity, mostly SplashActivity, also attach Autofac Module for Android specific classes

```
protected override void OnCreate(Bundle savedInstanceState)
{
    base.OnCreate(savedInstanceState);

    //Init application with Autofac Module for platform specific
    SampleApp.App.Init(new DroidAutofacModule());
}
```
