using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ChessOpenings.Models;
using ChessOpenings.ViewInterfaces;

namespace ChessOpenings.Droid
{
	[Activity (Label = "ChessOpenings.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainScreen : Activity, IMainScreen
	{

        public Board Board { get; set; }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            Board = new Board();

            Board.InitialiseBoard();

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            LinearLayout boardLayout = FindViewById<LinearLayout>(Resource.Id.boardLayout);
            TableLayout boardTable = FindViewById<TableLayout>(Resource.Id.board);

            BuildBoard(boardTable);
		}

        private void BuildBoard(TableLayout boardTable)
        {
            //Builds the Table Layout from the Board object

            TableRow row = new TableRow(this);
            TableRow.LayoutParams layoutParams = new TableRow.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);

            TextView tv = new TextView(this);
            ViewGroup.LayoutParams tvParams = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            tv.LayoutParameters = tvParams;
            tv.Text = "Table Cell";

            row.AddView(tv);
            boardTable.AddView(row);
        }
	}
}


