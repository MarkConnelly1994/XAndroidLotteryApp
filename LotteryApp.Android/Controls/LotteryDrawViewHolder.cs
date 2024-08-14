
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using LotteryApp.Android;

namespace LotteryApp
{
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