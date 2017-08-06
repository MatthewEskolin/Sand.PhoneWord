using System;

using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Sand.PhoneWord
{
    [Activity(Label = "ColorSwitcherActivity")]
    public class ColorSwitcherActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            ActionBar.Title = null;



            SetContentView(Resource.Layout.ColorSwitcher);

            var colorView = FindViewById<View>(Resource.Id.view1);
            colorView.SetBackgroundColor(Color.Green);

            var btnGo = FindViewById<Button>(Resource.Btns.btnChangeColor);


            btnGo.Click += ChangeViewBackGround;

        }

        private void ChangeViewBackGround(object sender, EventArgs e)
        {
            var colorView = FindViewById<View>(Resource.Id.view1);

            colorView.SetBackgroundColor(getRandomColor());


        }


        private Color getRandomColor()
        {
            var rnd = new Random();
            var r = rnd.Next(0, 255);
            var g = rnd.Next(0, 255);
            var b = rnd.Next(0, 255);

            return new Color(r, g, b);
        }
    }
}