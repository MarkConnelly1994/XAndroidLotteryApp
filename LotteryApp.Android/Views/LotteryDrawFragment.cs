using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using AndroidX.RecyclerView.Widget;
using LotteryApp.Core.Models;
using LotteryApp.Core.Services;
using LotteryApp.Core.ViewModels;
using System;
using System.Collections.Generic;

namespace LotteryApp.Android
{
    /// <summary>
    /// Class for showing the lottery recycler view.
    /// </summary>
    public class LotteryDrawFragment : Fragment
    {
        private RecyclerView recyclerView;
        private LotteryPageViewModel _viewModel;
        private LotteryDrawAdapter _adapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var lotteryDataService = new LotteryDataService(Activity);
            var preferencesService = ServiceLocator.Resolve<IPreferencesService>();
            var connectivityService = ServiceLocator.Resolve<IConnectivityService>();
            _viewModel = new LotteryPageViewModel(lotteryDataService, preferencesService, connectivityService);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Inflate the view.
            var view = inflater.Inflate(Resource.Layout.fragment_lottery_draw, container, false);

            // Find the recycler by ID.
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.lotteryRecyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Activity));

            _adapter = new LotteryDrawAdapter(Activity, new List<LotteryDrawModel>());
            recyclerView.SetAdapter(_adapter);

            // Attach ItemTouchHelper for swipe actions
            var swipeHandler = new SwipeToDetailCallback(Activity, _adapter);
            var itemTouchHelper = new ItemTouchHelper(swipeHandler);
            itemTouchHelper.AttachToRecyclerView(recyclerView);

            LoadDataAsync();

            return view;
        }

       

        /// <summary>
        /// Get the data from the json file using the viewModel service.
        /// </summary>
        private async void LoadDataAsync()
        {
            await _viewModel.LoadLotteryDrawsAsync();
            if (_viewModel.LotteryDraws.Count > 0)
            {
                _adapter.UpdateData(_viewModel.LotteryDraws);
                _adapter.NotifyDataSetChanged();
            }
            else if (!string.IsNullOrEmpty(_viewModel.ErrorMessage))
            {
                Toast.MakeText(Activity, _viewModel.ErrorMessage, ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(Activity, "No lottery tickets available, please try again.", ToastLength.Short).Show();
            }
        }
    }
}
