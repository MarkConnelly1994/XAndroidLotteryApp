using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using LotteryApp.Core.Models;

namespace LotteryApp.Android.Controls
{
    /// <summary>
    /// Lottery Adapter class.
    /// </summary>
    public class LotteryDrawAdapter : BaseAdapter<LotteryDrawModel>
    {
        private readonly Activity _context;
        private readonly List<LotteryDrawModel> _items;

        public LotteryDrawAdapter(Activity context, List<LotteryDrawModel> items)
        {
            _context = context;
            _items = items;
        }

        // Returns the model at the specified position in the list.
        public override LotteryDrawModel this[int position] => _items[position];

        // Gets the total number of items in list.
        public override int Count => _items.Count;

        // Returns the unique id for each item.
        public override long GetItemId(int position) => position;

        /// <summary>
        /// Gets the view for each item and binds the data to view.
        /// </summary>
        /// <param name="position">Position in list.</param>
        /// <param name="convertView">Convert item</param>
        /// <param name="parent">Parent</param>
        /// <returns>View</returns>
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.lottery_draw_item, parent, false);

            var drawDateTextView = view.FindViewById<TextView>(Resource.Id.drawDateTextView);
            var drawNumbersTextView = view.FindViewById<TextView>(Resource.Id.drawNumbersTextView);
            var topPrizeTextView = view.FindViewById<TextView>(Resource.Id.topPrizeTextView);

            var item = _items[position];

            drawDateTextView.Text = item.DrawDate.ToString();
            drawNumbersTextView.Text = $"{item.Number1}, {item.Number2}, {item.Number3}, {item.Number4}, {item.Number5}, {item.Number6}";
            topPrizeTextView.Text = $"Top Prize: {item.TopPrize:C}";

            return view;
        }
    }
}



