using System;
using System.Collections.Generic;

namespace LotteryApp.Core.Models
{
    /// <summary>
    /// Represents a lottery draw.
    /// </summary>
    public class LotteryDrawModel
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the date of the draw.
        /// </summary>
        public DateTime DrawDate { get; set; }

        /// <summary>
        /// Gets or sets the first drawn number.
        /// </summary>
        public int Number1 { get; set; }

        /// <summary>
        /// Gets or sets the second drawn number.
        /// </summary>
        public int Number2 { get; set; }

        /// <summary>
        /// Gets or sets the third drawn number.
        /// </summary>
        public int Number3 { get; set; }

        /// <summary>
        /// Gets or sets the fourth drawn number.
        /// </summary>
        public int Number4 { get; set; }

        /// <summary>
        /// Gets or sets the fifth drawn number.
        /// </summary>
        public int Number5 { get; set; }

        /// <summary>
        /// Gets or sets the sixth drawn number.
        /// </summary>
        public int Number6 { get; set; }

        /// <summary>
        /// Gets or sets the bonus ball number.
        /// </summary>
        public int BonusBall { get; set; }

        /// <summary>
        /// Gets or sets the top prize amount.
        /// </summary>
        public long TopPrize { get; set; }

        /// <summary>
        /// Gets the list of numbers for the specific draw.
        /// </summary>
        /// <returns></returns>
        public List<int> GetNumbersList()
        {
            return new List<int> { Number1, Number2, Number3, Number4, Number5, Number6 };
        }
    }
}

