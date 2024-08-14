using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using System;
using System.Linq;

namespace LotteryApp.Android
{
    public class NumberGeneratorFragment : Fragment
    {
        private LinearLayout numberContainer;
        private Button generateButton;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_random_number, container, false);

            numberContainer = view.FindViewById<LinearLayout>(Resource.Id.numberContainer);
            generateButton = view.FindViewById<Button>(Resource.Id.generateButton);

            // Generate and display random numbers initially
            GenerateAndDisplayNumbers();

            // Set up button click event to regenerate numbers
            generateButton.Click += (sender, e) =>
            {
                GenerateAndDisplayNumbers();
            };

            return view;
        }

        private void GenerateAndDisplayNumbers()
        {
            // Clear previous numbers
            numberContainer.RemoveAllViews();

            // Generate new random numbers
            var randomNumbers = GenerateRandomNumbers(7, 1, 59); // Generate 7 random numbers between 1 and 59

            // Display the numbers
            foreach (var number in randomNumbers)
            {
                var textView = new TextView(Context)
                {
                    Text = number.ToString(),
                    TextSize = 24f,
                    Gravity = GravityFlags.Center
                };

                var layoutParams = new LinearLayout.LayoutParams(
                    ViewGroup.LayoutParams.WrapContent,
                    ViewGroup.LayoutParams.WrapContent
                )
                {
                    MarginStart = 16,
                    MarginEnd = 16
                };

                numberContainer.AddView(textView, layoutParams);
            }
        }

        private int[] GenerateRandomNumbers(int count, int min, int max)
        {
            var random = new Random();
            return Enumerable.Range(min, max - min + 1)
                             .OrderBy(x => random.Next())
                             .Take(count)
                             .ToArray();
        }
    }
}
