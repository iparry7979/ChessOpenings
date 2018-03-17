using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ChessOpenings.Models;
using ChessOpenings.ViewInterfaces;
using Android.Graphics;
using ChessOpenings.Droid.Views;
using ChessOpenings.Controllers;
using ChessOpenings.Droid.Fragments;

namespace ChessOpenings.Droid
{
    [Activity(Label = "ChessOpenings.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainScreen : Activity
    {
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
            SetContentView(Resource.Layout.Main);

            var boardFragment = new BoardFragment();
            var ft = FragmentManager.BeginTransaction();
            ft.Add(Resource.Id.board_fragment_container, boardFragment);
            ft.Commit();
        }

        
    }
}


