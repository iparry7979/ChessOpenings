using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ChessOpenings.Droid.Views
{
    class BoardSquareView : TextView
    {
        public BoardSquareView(Context context) : base(context) { }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            //base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            /*int parentWidth = MeasureSpec.GetSize(widthMeasureSpec);
            int parentHeight = MeasureSpec.GetSize(heightMeasureSpec);
            int pHeight = ((BoardTableRow)this.Parent).MeasuredHeight;
            if (pHeight > 0)
            {
                int squareSize = ((BoardTableRow)this.Parent).MeasuredHeight;
                this.SetMeasuredDimension(squareSize, squareSize);
                this.LayoutParameters = new TableRow.LayoutParams(squareSize, squareSize);
            }*/
            base.OnMeasure(heightMeasureSpec, heightMeasureSpec);
        }

        protected override void OnDraw(Canvas canvas)
        {
            int h1 = this.Height;
            base.OnDraw(canvas);
        }
    }
}