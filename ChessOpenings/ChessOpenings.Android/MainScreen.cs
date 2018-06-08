﻿using System;

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
            //var dataFragment = new OpeningDataFragment();

            //BoardController controller = new BoardController(boardFragment, dataFragment);

            //boardFragment.boardController = controller;
            //dataFragment.controller = controller;

            var ft = FragmentManager.BeginTransaction();
            ft.Add(Resource.Id.board_fragment_container, boardFragment);
            //ft.Add(Resource.Id.data_fragment_container, dataFragment);
            ft.Commit();

            

        }

        
    }
}


