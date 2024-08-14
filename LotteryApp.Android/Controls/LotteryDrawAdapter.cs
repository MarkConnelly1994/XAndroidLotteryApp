using Android.Content;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using LotteryApp.Core.Models;
using System.Collections.Generic;

namespace LotteryApp.Android
{
    public class LotteryDrawAdapter : RecyclerView.Adapter
    {
        private readonly List<LotteryDrawModel> _lotteryDraws;
        private readonly Context _context;

        public LotteryDrawAdapter(Context context, List<LotteryDrawModel> lotteryDraws)
        {
            _context = context;
            _lotteryDraws = lotteryDraws;
        }

        public LotteryDrawModel GetItem(int position)
        {
            return _lotteryDraws[position];
        }

        public void UpdateData(List<LotteryDrawModel> newDraws)
        {
            _lotteryDraws.Clear();
            _lotteryDraws.AddRange(newDraws);
        }

        public override int ItemCount => _lotteryDraws.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is LotteryDrawViewHolder viewHolder)
            {
                var item = _lotteryDraws[position];
                viewHolder.DrawDateTextView.Text = item.DrawDate.ToString();
                viewHolder.DrawNumbersTextView.Text = $"{item.Number1}, {item.Number2}, {item.Number3}, {item.Number4}, {item.Number5}, {item.Number6}";
                viewHolder.BonusBallTextView.Text = $"Bonus Ball: {item.BonusBall}";
                viewHolder.TopPrizeTextView.Text = $"Top Prize: {item.TopPrize:C}";

                // Attach swipe gesture listener to the item view
                var swipeListener = new OnSwipeTouchListener(_context);
                swipeListener.OnSwipeLeft += () =>
                {
                    // Navigate to the detail page for the swiped item
                    NavigateToDetailPage(item);
                };
                viewHolder.ItemView.SetOnTouchListener(swipeListener);

                // Handle item click
                viewHolder.ItemView.Click += (sender, e) =>
                {
                    NavigateToDetailPage(item);
                };
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.lottery_draw_item, parent, false);
            return new LotteryDrawViewHolder(itemView);
        }

        private void NavigateToDetailPage(LotteryDrawModel draw)
        {
            var intent = new Intent(_context, typeof(LotteryDetailActivity));
            intent.PutExtra("DrawDate", draw.DrawDate.ToString());
            intent.PutExtra("Number1", draw.Number1.ToString());
            intent.PutExtra("Number2", draw.Number2.ToString());
            intent.PutExtra("Number3", draw.Number3.ToString());
            intent.PutExtra("Number4", draw.Number4.ToString());
            intent.PutExtra("Number5", draw.Number5.ToString());
            intent.PutExtra("Number6", draw.Number6.ToString());
            intent.PutExtra("BonusBall", draw.BonusBall.ToString());
            intent.PutExtra("TopPrize", draw.TopPrize);
            _context.StartActivity(intent);
        }
    }
}
