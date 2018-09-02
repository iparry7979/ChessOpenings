using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading;
using System.Threading.Tasks;

namespace ChessOpenings.Droid
{
    [Activity(Label = "Chess Openings", Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true, Icon = "@drawable/CheckIcon")]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.splash);

            //Thread.Sleep(4000);

            Task.Run(() =>
           {
               Thread.Sleep(1500);
               StartActivity(typeof(MainScreen));
           });

            //StartActivity(typeof(MainScreen));
        }
    }
}