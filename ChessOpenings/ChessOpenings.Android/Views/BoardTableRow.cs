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
    public class BoardTableRow : TableRow
    {
        public int measuredHeight;

        public BoardTableRow(Context context) : base(context) { }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            //base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            /*int parentWidth = MeasureSpec.GetSize(widthMeasureSpec);
            int parentHeight = MeasureSpec.GetSize(heightMeasureSpec);
            SquareTableLayout parent = (SquareTableLayout)this.Parent;
            int rowHeight = parent.MeasuredHeight / 8;
            measuredHeight = rowHeight;
            this.SetMeasuredDimension(parentWidth, rowHeight);
            this.LayoutParameters = new TableLayout.LayoutParams(parentWidth, rowHeight);
            this.SetBackgroundColor(Color.Blue);*/
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
        }

        protected override void OnDraw(Canvas canvas)
        {
            /*SquareTableLayout parent = (SquareTableLayout)this.Parent;
            int h = parent.MeasuredHeight / 8;
            this.SetMeasuredDimension(parent.MeasuredWidth, h); */
            int h1 = this.Height;
            base.OnDraw(canvas);
        }
    }
}