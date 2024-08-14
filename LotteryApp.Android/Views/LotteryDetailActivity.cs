using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using LotteryApp.Core.Models;
using System;

namespace LotteryApp.Android
{
    [Activity(Label = "Lottery Detail")]
    public class LotteryDetailActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_lottery_detail);

            // Get the passed data from the intent
            var drawDate = Intent.GetStringExtra("DrawDate");
            var number1 = Intent.GetStringExtra("Number1");
            var number2 = Intent.GetStringExtra("Number2");
            var number3 = Intent.GetStringExtra("Number3");
            var number4 = Intent.GetStringExtra("Number4");
            var number5 = Intent.GetStringExtra("Number5");
            var number6 = Intent.GetStringExtra("Number6");
            var bonusBall = Intent.GetStringExtra("BonusBall");
            var topPrize = Intent.GetStringExtra("TopPrize");

            // Set the values to the TextViews
            FindViewById<TextView>(Resource.Id.drawDateTextView).Text = drawDate;
            FindViewById<TextView>(Resource.Id.number1TextView).Text = number1;
            FindViewById<TextView>(Resource.Id.number2TextView).Text = number2;
            FindViewById<TextView>(Resource.Id.number3TextView).Text = number3;
            FindViewById<TextView>(Resource.Id.number4TextView).Text = number4;
            FindViewById<TextView>(Resource.Id.number5TextView).Text = number5;
            FindViewById<TextView>(Resource.Id.number6TextView).Text = number6;
            FindViewById<TextView>(Resource.Id.bonusBallTextView).Text = bonusBall;
            FindViewById<TextView>(Resource.Id.topPrizeTextView).Text = $"£{topPrize:n0}";
        }
    }
}
