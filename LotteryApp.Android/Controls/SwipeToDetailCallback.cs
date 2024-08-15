using Android.Content;
using Android.Graphics;
using AndroidX.RecyclerView.Widget;
using LotteryApp.Core.Models;

namespace LotteryApp.Android
{
    public class SwipeToDetailCallback : ItemTouchHelper.SimpleCallback
    {
        private readonly LotteryDrawAdapter _adapter;
        private readonly Context _context;

        public SwipeToDetailCallback(Context context, LotteryDrawAdapter adapter)
            : base(0, ItemTouchHelper.Left)
        {
            _context = context;
            _adapter = adapter;
        }

        public override bool OnMove(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, RecyclerView.ViewHolder target)
        {
            // We do not want move support in this case
            return false;
        }

        public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction)
        {
            // Handle the swipe
            var position = viewHolder.AdapterPosition;
            var item = _adapter.GetItem(position);
            NavigateToDetailPage(item);

            _adapter.NotifyItemChanged(position);
        }

        /// <summary>
        /// Method called when user is swiping an item.
        /// </summary>
        /// <param name="c">Canvas</param>
        /// <param name="recyclerView"></param>
        /// <param name="viewHolder"></param>
        /// <param name="dX"></param>
        /// <param name="dY"></param>
        /// <param name="actionState"></param>
        /// <param name="isCurrentlyActive"></param>
        public override void OnChildDraw(Canvas c, RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, float dX, float dY, int actionState, bool isCurrentlyActive)
        {
            base.OnChildDraw(c, recyclerView, viewHolder, dX, dY, actionState, isCurrentlyActive);
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
}
