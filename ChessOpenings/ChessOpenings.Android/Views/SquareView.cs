using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using ChessOpenings.Models;
using Android.Graphics;

namespace ChessOpenings.Droid.Views
{
    public class SquareView : TextView
    {
        public Square squareModel;

        public SquareView(Context context, Square squareModel) : base(context)
        {
            this.squareModel = squareModel;
        }

        public SquareView(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
        }

        public SquareView(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize();
        }

        private void Initialize()
        {

        }

        public void SelectSquare()
        {
            this.SetBackgroundColor(Color.Orange);
        }

        public void UnSelectSquare()
        {
            this.UpdateBackgroundColor();
        }

        public void UpdateBackgroundColor()
        {
            Color squareColor = squareModel.Colour == Enums.Colour.White ? Color.White : Color.Black;
            this.SetBackgroundColor(squareColor);
        }

        public bool ContainsPiece()
        {
            return squareModel.Piece != null;
        }

        public Enums.Colour? GetPieceColor()
        {
            if (squareModel.Piece != null)
            {
                return squareModel.Piece.colour;
            }
            return null;
        }
    }
}