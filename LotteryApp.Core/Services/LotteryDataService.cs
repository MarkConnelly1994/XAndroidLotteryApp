using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Android.Content;
using LotteryApp.Core.Models;
using Newtonsoft.Json;

namespace LotteryApp.Core.Services
{
    /// <summary>
    /// Lottery Data service.
    /// </summary>
    public class LotteryDataService : ILotteryDataService
    {
        private readonly Context _context;

        public LotteryDataService(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Get the lottery data from json.
        /// </summary>
        /// <returns>serialized json</returns>
        public async Task<List<LotteryDrawModel>> GetLotteryDrawsAsync()
        {
            try
            {
                using (var stream = _context.Assets.Open("LotteryData.json"))
                using (var reader = new StreamReader(stream))
                {
                    var json = await reader.ReadToEndAsync();
                    var lotteryData = JsonConvert.DeserializeObject<LotteryData>(json);
                    return lotteryData.Draws;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading LotteryData.json: {ex.Message}");
                return new List<LotteryDrawModel>();
            }
        }
    }
}
