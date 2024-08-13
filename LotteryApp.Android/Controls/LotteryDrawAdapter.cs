using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using LotteryApp.Core.Models;

namespace LotteryApp.Android.Controls
{
    public class LotteryDrawAdapter : BaseAdapter<LotteryDrawModel>
    {
        private readonly Activity _context;
        private readonly List<LotteryDrawModel> _items;

        public LotteryDrawAdapter(Activity context, List<LotteryDrawModel> items)
        {
            _context = context;
            _items = items;
        }

        public override LotteryDrawModel this[int position] => _items[position];

        public override int Count => _items.Count;

        public override long GetItemId(int position) => position;

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



