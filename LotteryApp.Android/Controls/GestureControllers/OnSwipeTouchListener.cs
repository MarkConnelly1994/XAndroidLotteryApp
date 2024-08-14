using Android.Content;
using Android.Views;
using System;

namespace LotteryApp.Android
{
    public class OnSwipeTouchListener : Java.Lang.Object, View.IOnTouchListener
    {
        private readonly GestureDetector gestureDetector;
        public event Action OnSwipeLeft;
        public event Action OnSwipeRight;

        public OnSwipeTouchListener(Context context)
        {
            gestureDetector = new GestureDetector(context, new GestureListener(this));
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            return gestureDetector.OnTouchEvent(e);
        }

        private class GestureListener : GestureDetector.SimpleOnGestureListener
        {
            private const int SwipeThreshold = 100;
            private const int SwipeVelocityThreshold = 100;
            private readonly OnSwipeTouchListener listener;

            public GestureListener(OnSwipeTouchListener listener)
            {
                this.listener = listener;
            }

            public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
            {
                float diffY = e2.GetY() - e1.GetY();
                float diffX = e2.GetX() - e1.GetX();
                if (Math.Abs(diffX) > Math.Abs(diffY))
                {
                    if (Math.Abs(diffX) > SwipeThreshold && Math.Abs(velocityX) > SwipeVelocityThreshold)
                    {
                        if (diffX > 0)
                        {
                            listener.OnSwipeRight?.Invoke();
                        }
                        else
                        {
                            listener.OnSwipeLeft?.Invoke();
                        }
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
