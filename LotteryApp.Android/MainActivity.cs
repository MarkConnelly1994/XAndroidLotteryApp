using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using LotteryApp.Core;
using LotteryApp.Core.Services;
using Android.Widget;
using System.Threading.Tasks;
using LotteryApp.Core.Models;
using LotteryApp.Android.Controls;
using System.Collections.Generic;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace LotteryApp.Android

{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private ListView _lotteryListView;
        private LotteryDataService _lotteryDataService;
        private List<LotteryDrawModel> _lotteryDraws;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            // Set up the toolbar
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "ConneLotto";

           

            _lotteryListView = FindViewById<ListView>(Resource.Id.lotteryListView);

            _lotteryDataService = new LotteryDataService(this);
            _lotteryDraws = await _lotteryDataService.GetLotteryDrawsAsync();

            if (_lotteryDraws.Count > 0)
            {
                var adapter = new LotteryDrawAdapter(this, _lotteryDraws);
                _lotteryListView.Adapter = adapter;
            }
            else
            {
                Toast.MakeText(this, "No lottery data available", ToastLength.Short).Show();
            }
        }
    }
}