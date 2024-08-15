using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using LotteryApp.Core.Models;
using LotteryApp.Core.Services;
using LotteryApp.Core.Utils;
using LotteryApp.Core.Utils.LotteryApp.Core.Utils;
using LotteryApp.Core.ViewModels;
using System.Drawing;
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
            var preferencesService = ServiceLocator.Resolve<IPreferencesService>();
            var connectivityService = ServiceLocator.Resolve<IConnectivityService>();
            var lotteryDataService = new LotteryDataService(Activity);
            _viewModel = new LotteryPageViewModel(lotteryDataService, preferencesService, connectivityService);
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
            numberContainer.RemoveAllViews();

            var textColor = Context.GetColorStateList(Resource.Color.secondaryTextColor);

            // Generate new random numbers.
            var randomNumbers = LotteryNumberGenerator.GenerateRandomNumbers(7, 1, 59);

            // Get the first 6 numbers, as the last is the bonus.
            for (int i = 0; i < randomNumbers.Length - 1; i++)
            {
                var number = randomNumbers[i];

                var textView = new TextView(Context)
                {
                    Text = number.ToString(),
                    TextSize = 16f,
                    Gravity = GravityFlags.Center
                };

                var layoutParams = new LinearLayout.LayoutParams(
                    120,
                    120)
                {
                    MarginStart = 16,
                    MarginEnd = 16,
                    Gravity = GravityFlags.CenterHorizontal
                };

                textView.SetBackgroundResource(Resource.Drawable.circle_background);
                textView.SetTextColor(textColor);

                numberContainer.AddView(textView, layoutParams);
            }

            // Display the bonus number with a red circle.
            var lastNumber = randomNumbers.Last();

            var lastNumberTextView = new TextView(Context)
            {
                Text = lastNumber.ToString(),
                TextSize = 16f,
                Gravity = GravityFlags.Center
            };

            var lastNumberLayoutParams = new LinearLayout.LayoutParams(
                120,
                120)
            {
                TopMargin = 24,
                Gravity = GravityFlags.CenterHorizontal
            };

            lastNumberTextView.SetBackgroundResource(Resource.Drawable.circle_red_background);

            lastNumberTextView.SetTextColor(textColor);

            numberContainer.AddView(lastNumberTextView, lastNumberLayoutParams);

            // Compare with lottery data.
            await _viewModel.LoadLotteryDrawsAsync();
            var isWinner = _viewModel.LotteryDraws.Any(draw => LotteryNumberComparer.AreNumbersMatching(randomNumbers, draw));

            resultTextView.Text = isWinner ? "Winner!" : "No wins today";
            resultTextView.SetTextColor(isWinner ? Context.GetColorStateList(Resource.Color.winnerTextColor) : Context.GetColorStateList(Resource.Color.loserTextColor));
        }
    }
}
