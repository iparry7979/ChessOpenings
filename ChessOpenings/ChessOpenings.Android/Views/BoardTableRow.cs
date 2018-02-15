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

namespace ChessOpenings.Droid.Views
{
    public class BoardTableRow : TableRow
    {
        public BoardTableRow(Context context) : base(context) { }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            //base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            int parentWidth = MeasureSpec.GetSize(widthMeasureSpec);
            int parentHeight = MeasureSpec.GetSize(heightMeasureSpec);
            int rowHeight = parentHeight / 8;
            this.SetMeasuredDimension(parentWidth, rowHeight);
            this.LayoutParameters = new TableLayout.LayoutParams(parentWidth, rowHeight);
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
        }
    }
}