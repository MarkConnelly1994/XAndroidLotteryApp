using System;
using System.Collections.Generic;

namespace LotteryApp.Core.Models
{
	public class LotteryDrawModel
	{
        public string? Id { get; set; }
        public DateTime DrawDate { get; set; }
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Number3 { get; set; }
        public int Number4 { get; set; }
        public int Number5 { get; set; }
        public int Number6 { get; set; }
        public int BonusBall { get; set; }
        public long TopPrize { get; set; }

        public List<int> GetNumbersList()
        {
            return new List<int> { Number1, Number2, Number3, Number4, Number5, Number6 };
        }
    }
}

