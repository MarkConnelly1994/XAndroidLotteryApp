using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LotteryApp.Core.Models;
using LotteryApp.Core.Services;
using Newtonsoft.Json;

namespace LotteryApp.Core.ViewModels
{
    public class LotteryPageViewModel
    {
        public List<LotteryDrawModel> LotteryDraws { get; private set; }
        public bool IsLoading { get; private set; }
        public string ErrorMessage { get; private set; }
        private readonly ILotteryDataService _lotteryDataService;
        private readonly IPreferencesService _preferencesService;
        private readonly IConnectivityService _connectivityService;

        private const string LotteryDrawsKey = "LotteryDraws";

        /// <summary>
        /// Lottery Page view model.
        /// </summary>
        /// <param name="lotteryDataService">Lottery data service.</param>
        /// <param name="preferencesService">Preferences service for saving and loading data.</param>
        /// <param name="connectivityService">Connectivity service for checking internet connection.</param>
        public LotteryPageViewModel(ILotteryDataService lotteryDataService, IPreferencesService preferencesService, IConnectivityService connectivityService)
        {
            _lotteryDataService = lotteryDataService;
            _preferencesService = preferencesService;
            _connectivityService = connectivityService;
            LotteryDraws = new List<LotteryDrawModel>();
            ErrorMessage = string.Empty;
        }

        public async Task LoadLotteryDrawsAsync()
        {
            IsLoading = true;
            try
            {
                if (_connectivityService.IsConnected())
                {
                    // Load from .json file if connected to the internet
                    var draws = await _lotteryDataService.GetLotteryDrawsAsync();
                    LotteryDraws.Clear();
                    foreach (var draw in draws)
                    {
                        LotteryDraws.Add(draw);
                    }

                    // Save the data.
                    SaveLotteryDraws();
                }
                else
                {
                    // No internet connection, load from preferences
                    LoadLotteryDrawsFromPreferences();

                    if (LotteryDraws.Count == 0)
                    {
                        ErrorMessage = "No internet connection and no saved data available.";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Failed to load data: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        /// <summary>
        /// Saves the current list of lottery draws to SharedPreferences.
        /// </summary>
        private void SaveLotteryDraws()
        {
            var json = JsonConvert.SerializeObject(LotteryDraws);
            _preferencesService.SaveList(LotteryDrawsKey, json);
        }

        /// <summary>
        /// Loads the lottery draws from SharedPreferences.
        /// </summary>
        public void LoadLotteryDrawsFromPreferences()
        {
            var json = _preferencesService.GetList(LotteryDrawsKey);
            if (!string.IsNullOrEmpty(json))
            {
                LotteryDraws = JsonConvert.DeserializeObject<List<LotteryDrawModel>>(json);
            }
        }
    }
}
