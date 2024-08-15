using Android.Content;
using Android.Views;
using Android.Widget;
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
            NotifyDataSetChanged();
        }

        public override int ItemCount => _lotteryDraws.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is LotteryDrawViewHolder viewHolder)
            {
                var item = _lotteryDraws[position];

                viewHolder.DrawDateTextView.Text = item.DrawDate;
                viewHolder.Number1TextView.Text = item.Number1.ToString();
                viewHolder.Number2TextView.Text = item.Number2.ToString();
                viewHolder.Number3TextView.Text = item.Number3.ToString();
                viewHolder.Number4TextView.Text = item.Number4.ToString();
                viewHolder.Number5TextView.Text = item.Number5.ToString();
                viewHolder.Number6TextView.Text = item.Number6.ToString();
                viewHolder.BonusBallTextView.Text = item.BonusBall.ToString();
                viewHolder.TopPrizeTextView.Text = $"£{item.TopPrize:n0}".ToString();

                // Attach swipe gesture listener to the item view
                var swipeListener = new OnSwipeTouchListener(_context);
                swipeListener.OnSwipeLeft += () =>
                {
                    NavigateToDetailPage(item);
                };
                viewHolder.ItemView.SetOnTouchListener(swipeListener);

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
            intent.PutExtra("TopPrize", draw.TopPrize.ToString());
            _context.StartActivity(intent);
        }
    }

    public class LotteryDrawViewHolder : RecyclerView.ViewHolder
    {
        public TextView DrawDateTextView { get; }
        public TextView Number1TextView { get; }
        public TextView Number2TextView { get; }
        public TextView Number3TextView { get; }
        public TextView Number4TextView { get; }
        public TextView Number5TextView { get; }
        public TextView Number6TextView { get; }
        public TextView BonusBallTextView { get; }
        public TextView TopPrizeTextView { get; }

        public LotteryDrawViewHolder(View itemView) : base(itemView)
        {
            DrawDateTextView = itemView.FindViewById<TextView>(Resource.Id.drawDateTextView);
            Number1TextView = itemView.FindViewById<TextView>(Resource.Id.number1TextView);
            Number2TextView = itemView.FindViewById<TextView>(Resource.Id.number2TextView);
            Number3TextView = itemView.FindViewById<TextView>(Resource.Id.number3TextView);
            Number4TextView = itemView.FindViewById<TextView>(Resource.Id.number4TextView);
            Number5TextView = itemView.FindViewById<TextView>(Resource.Id.number5TextView);
            Number6TextView = itemView.FindViewById<TextView>(Resource.Id.number6TextView);
            BonusBallTextView = itemView.FindViewById<TextView>(Resource.Id.bonusBallTextView);
            TopPrizeTextView = itemView.FindViewById<TextView>(Resource.Id.topPrizeTextView);
        }
    }
}
