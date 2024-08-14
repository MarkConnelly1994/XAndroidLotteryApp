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
using AndroidX.Lifecycle;
using LotteryApp.Core.ViewModels;
using Android.Content;

namespace LotteryApp.Android

{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private ListView _lotteryListView;
        private LotteryPageViewModel _viewModel;

        protected override async void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            // Set up the toolbar
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "ConneLotto";



            // Set up the ViewModel and ListView
            _lotteryListView = FindViewById<ListView>(Resource.Id.lotteryListView);
            var lotteryDataService = new LotteryDataService(this);
            _viewModel = new LotteryPageViewModel(lotteryDataService);

            await LoadDataAsync();
        }

        /// <summary>
        /// Load the json data.
        /// </summary>
        /// <returns></returns>
        private async Task LoadDataAsync()
        {
            await _viewModel.LoadLotteryDrawsAsync();
            if (_viewModel.LotteryDraws.Count > 0)
            {
                var adapter = new LotteryDrawAdapter(this, _viewModel.LotteryDraws);
                _lotteryListView.Adapter = adapter;
            }
            else if (!string.IsNullOrEmpty(_viewModel.ErrorMessage))
            {
                Toast.MakeText(this, _viewModel.ErrorMessage, ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "No lottery data available", ToastLength.Short).Show();
            }
        }
    }
}