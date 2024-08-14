using System;
using System.Collections.Generic;
using Newtonsoft.Json;

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
        [JsonProperty("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the date of the draw.
        /// </summary>
        [JsonProperty("drawDate")]
        public string? DrawDate { get; set; }

        /// <summary>
        /// Gets or sets the first drawn number.
        /// </summary>
        [JsonProperty("number1")]
        public int Number1 { get; set; }

        /// <summary>
        /// Gets or sets the second drawn number.
        /// </summary>
        [JsonProperty("number2")]
        public int Number2 { get; set; }

        /// <summary>
        /// Gets or sets the third drawn number.
        /// </summary>
        [JsonProperty("number3")]
        public int Number3 { get; set; }

        /// <summary>
        /// Gets or sets the fourth drawn number.
        /// </summary>
        [JsonProperty("number4")]
        public int Number4 { get; set; }

        /// <summary>
        /// Gets or sets the fifth drawn number.
        /// </summary>
        [JsonProperty("number5")]
        public int Number5 { get; set; }

        /// <summary>
        /// Gets or sets the sixth drawn number.
        /// </summary>
        [JsonProperty("number6")]
        public int Number6 { get; set; }

        /// <summary>
        /// Gets or sets the bonus ball number.
        /// </summary>
        [JsonProperty("bonus-ball")]
        public int BonusBall { get; set; }

        /// <summary>
        /// Gets or sets the top prize amount.
        /// </summary>
        [JsonProperty("topPrize")]
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
