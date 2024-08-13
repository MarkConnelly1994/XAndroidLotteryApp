using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Android.Content;
using LotteryApp.Core.Models;
using Newtonsoft.Json;

namespace LotteryApp.Core.Services
{
    public class LotteryDataService
    {
        private readonly Context _context;

        public LotteryDataService(Context context)
        {
            _context = context;
        }

        public async Task<List<LotteryDrawModel>> GetLotteryDrawsAsync()
        {
            // Connectivity service check first.
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
                // Log or handle the exception
                Console.WriteLine($"Error reading LotteryData.json: {ex.Message}");
                return new List<LotteryDrawModel>();
            }
        }
    }
}


