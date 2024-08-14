
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using LotteryApp.Android;

namespace LotteryApp
{
    public class LotteryDrawViewHolder : RecyclerView.ViewHolder
    {
        public TextView DrawDateTextView { get; private set; }
        public TextView DrawNumbersTextView { get; private set; }
        public TextView TopPrizeTextView { get; private set; }
        public TextView BonusBallTextView { get; private set; }

        public LotteryDrawViewHolder(View itemView) : base(itemView)
        {
            DrawDateTextView = itemView.FindViewById<TextView>(Resource.Id.drawDateTextView);
            DrawNumbersTextView = itemView.FindViewById<TextView>(Resource.Id.drawNumbersTextView);
            TopPrizeTextView = itemView.FindViewById<TextView>(Resource.Id.topPrizeTextView);
            BonusBallTextView = itemView.FindViewById<TextView>(Resource.Id.bonusBallTextView);
        }
    }
}
