using System.Collections.Generic;
using System.Threading.Tasks;
using LotteryApp.Core.Models;

namespace LotteryApp.Core.Services
{
    public interface ILotteryDataService
    {
        Task<List<LotteryDrawModel>> GetLotteryDrawsAsync();
    }
}
