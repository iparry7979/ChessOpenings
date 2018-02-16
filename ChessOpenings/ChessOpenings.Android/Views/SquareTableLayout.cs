﻿using System;
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
    class SquareTableLayout : TableLayout
    {
        public SquareTableLayout(Context context) : base(context)
        {

        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            //base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            int parentWidth = MeasureSpec.GetSize(widthMeasureSpec);
            int parentHeight = MeasureSpec.GetSize(heightMeasureSpec);
            int maxSquareWidth = Math.Min(parentWidth, parentHeight);
            this.SetMeasuredDimension(maxSquareWidth, maxSquareWidth);
            this.LayoutParameters = new LinearLayout.LayoutParams(maxSquareWidth, maxSquareWidth);
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
        }
    }
}