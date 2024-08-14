using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using LotteryApp.Core.Models;
using LotteryApp.Core.Services;
using LotteryApp.Core.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LotteryApp.Android
{
    public class NumberGeneratorFragment : Fragment
    {
        private LinearLayout numberContainer;
        private Button generateButton;
        private TextView resultTextView;
        private LotteryPageViewModel _viewModel;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var lotteryDataService = new LotteryDataService(Activity);
            _viewModel = new LotteryPageViewModel(lotteryDataService);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_random_number, container, false);

            numberContainer = view.FindViewById<LinearLayout>(Resource.Id.numberContainer);
            generateButton = view.FindViewById<Button>(Resource.Id.generateButton);
            resultTextView = view.FindViewById<TextView>(Resource.Id.resultTextView);

            // Generate and display random numbers initially
            _ = GenerateAndCompareNumbersAsync();

            // Set up button click event to regenerate numbers
            generateButton.Click += async (sender, e) =>
            {
                await GenerateAndCompareNumbersAsync();
            };

            return view;
        }

        private async Task GenerateAndCompareNumbersAsync()
        {
            // Clear previous numbers
            numberContainer.RemoveAllViews();

            // Generate new random numbers
            var randomNumbers = GenerateRandomNumbers(7, 1, 59);

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

            // Compare with lottery data
            var isWinner = await CompareWithLotteryDataAsync(randomNumbers);
            resultTextView.Text = isWinner ? "Winner!" : "No wins today";
        }

        private int[] GenerateRandomNumbers(int count, int min, int max)
        {
            var random = new Random();
            return Enumerable.Range(min, max - min + 1)
                             .OrderBy(x => random.Next())
                             .Take(count)
                             .ToArray();
        }

        private async Task<bool> CompareWithLotteryDataAsync(int[] generatedNumbers)
        {
            await _viewModel.LoadLotteryDrawsAsync();
            var lotteryDraws = _viewModel.LotteryDraws;

            foreach (var draw in lotteryDraws)
            {
                var drawNumbers = new[]
                {
                    int.Parse(draw.Number1.ToString()),
                    int.Parse(draw.Number2.ToString()),
                    int.Parse(draw.Number3.ToString()),
                    int.Parse(draw.Number4.ToString()),
                    int.Parse(draw.Number5.ToString()),
                    int.Parse(draw.Number6.ToString()),
                    int.Parse(draw.BonusBall.ToString())
                };

                // Compare generated numbers with draw numbers
                if (generatedNumbers.OrderBy(n => n).SequenceEqual(drawNumbers.OrderBy(n => n)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
