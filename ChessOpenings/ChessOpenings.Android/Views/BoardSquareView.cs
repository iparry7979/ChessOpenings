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
    class BoardSquareView : TextView
    {
        public BoardSquareView(Context context) : base(context) { }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            //base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            int parentWidth = MeasureSpec.GetSize(widthMeasureSpec);
            int parentHeight = MeasureSpec.GetSize(heightMeasureSpec);
            this.SetMeasuredDimension(parentWidth, parentWidth);
            this.LayoutParameters = new TableRow.LayoutParams(parentWidth, parentWidth);
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
        }
    }
}