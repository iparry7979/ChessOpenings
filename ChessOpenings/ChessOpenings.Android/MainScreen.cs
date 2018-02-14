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

            TextView tv = new TextView(this);
            ViewGroup.LayoutParams lp = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
            tv.LayoutParameters = lp;
            tv.Text = "Code TV";
            tv.SetTextColor(Color.Black);
            tv.SetBackgroundColor(Color.White);
            boardLayout.AddView(tv);

            BuildBoard(boardTable);

            


        }

        private void BuildBoard(TableLayout boardTable)
        {
            //Builds the Table Layout from the Board object

            for (int i = 0; i < Board.squaresArray.GetLength(0); i++)
            {
                TableRow currentRow = new TableRow(this);
                TableRow.LayoutParams rowParams = new TableRow.LayoutParams(TableRow.LayoutParams.WrapContent, TableRow.LayoutParams.WrapContent);
                currentRow.LayoutParameters = rowParams;

                for (int j = 0; j < Board.squaresArray.GetLength(1); j++)
                {
                    View squareLayout = BuildSquare(Board.squaresArray[i, j]);
                    currentRow.AddView(squareLayout);
                }
                boardTable.AddView(currentRow);
            }

            /*TableRow tr = new TableRow(this);
            TableRow.LayoutParams tlp = new TableRow.LayoutParams(TableRow.LayoutParams.WrapContent, TableRow.LayoutParams.WrapContent);
            tr.LayoutParameters = tlp;

            TextView tvr = new TextView(this);
            ViewGroup.LayoutParams lpr = new TableRow.LayoutParams(TableRow.LayoutParams.WrapContent, TableRow.LayoutParams.WrapContent);
            tvr.LayoutParameters = lpr;
            tvr.Text = "Code TR";
            tvr.SetTextColor(Color.Black);
            tvr.SetBackgroundColor(Color.White);

            tr.AddView(tvr);

            boardTable.AddView(tr);*/
        }

        public View BuildSquare(Square squareModel)
        {
            TextView rtn = new TextView(this);
            rtn.LayoutParameters = new TableRow.LayoutParams(TableRow.LayoutParams.WrapContent, TableRow.LayoutParams.WrapContent);
            Color squareColor = squareModel.Colour == Enums.Colour.White ? Color.White : Color.Black;
            rtn.SetBackgroundColor(squareColor);
            if (squareModel.Piece != null)
            {
                rtn.Text = squareModel.Piece.ToString();

                rtn.SetTextColor(squareModel.Piece.colour == Enums.Colour.White ? Color.LightGray : Color.DarkGray);
            }
            else
            {
                rtn.Text = "EMP";
                rtn.SetTextColor(Color.Gray);
            }
            return rtn;
        }
    }
}


