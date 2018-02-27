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
    class SquareTableLayout : TableLayout
    {
        public int measuredHeight;

        public SquareTableLayout(Context context) : base(context)
        {

        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            //base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            int parentWidth = MeasureSpec.GetSize(widthMeasureSpec);
            int parentHeight = MeasureSpec.GetSize(heightMeasureSpec);
            bool useWidth = parentWidth <= parentHeight;
            /*this.SetMeasuredDimension(maxSquareWidth, maxSquareWidth);
            measuredHeight = maxSquareWidth;
            this.LayoutParameters = new LinearLayout.LayoutParams(maxSquareWidth, maxSquareWidth);*/
            if (useWidth)
            {
                base.OnMeasure(widthMeasureSpec, widthMeasureSpec);
                return;
            }
            base.OnMeasure(heightMeasureSpec, heightMeasureSpec);
        }

        protected override void OnDraw(Canvas canvas)
        {
            int h1 = this.Height;
            base.OnDraw(canvas);
        }
    }
}