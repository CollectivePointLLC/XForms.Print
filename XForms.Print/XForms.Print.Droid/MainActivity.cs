using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using XForms.Print.Droid.Services;
using Android.Print;

namespace XForms.Print.Droid
{
	[Activity (Label = "XForms.Print.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            PrinterService.Init((PrintManager)GetSystemService(Context.PrintService));

            LoadApplication(new App());
        }
	}
}


