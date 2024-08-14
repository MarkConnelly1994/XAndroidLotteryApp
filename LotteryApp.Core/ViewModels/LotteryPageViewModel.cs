using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using LotteryApp.Core.Models;
using LotteryApp.Core.Services;

namespace LotteryApp.Core.ViewModels
{
    public class LotteryPageViewModel
    {
        public List<LotteryDrawModel> LotteryDraws { get; private set; }
        public bool IsLoading { get; private set; }
        public string ErrorMessage { get; private set; }
        private readonly ILotteryDataService _lotteryDataService;

        /// <summary>
        /// Lottery Page view model.
        /// </summary>
        /// <param name="lotteryDataService">Lottery data service.</param>
        public LotteryPageViewModel(ILotteryDataService lotteryDataService)
        {
            _lotteryDataService = lotteryDataService;
            LotteryDraws = new List<LotteryDrawModel>();
            ErrorMessage = string.Empty;
        }

        public async Task LoadLotteryDrawsAsync()
        {
            IsLoading = true;
            try
            {
                var draws = await _lotteryDataService.GetLotteryDrawsAsync();
                LotteryDraws.Clear();
                foreach (var draw in draws)
                {
                    LotteryDraws.Add(draw);
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
    }
}
