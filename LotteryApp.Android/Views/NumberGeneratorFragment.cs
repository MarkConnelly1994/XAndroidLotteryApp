using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using LotteryApp.Core.Models;
using LotteryApp.Core.Services;
using LotteryApp.Core.Utils;
using LotteryApp.Core.Utils.LotteryApp.Core.Utils;
using LotteryApp.Core.ViewModels;
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
            // Clear previous numbers
            numberContainer.RemoveAllViews();

            // Generate new random numbers using shared utility
            var randomNumbers = LotteryNumberGenerator.GenerateRandomNumbers(7, 1, 59);

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

            // Compare with lottery data using shared ViewModel method
            await _viewModel.LoadLotteryDrawsAsync();
            var isWinner = _viewModel.LotteryDraws.Any(draw => LotteryNumberComparer.AreNumbersMatching(randomNumbers, draw));

            resultTextView.Text = isWinner ? "Winner!" : "No wins today";
        }
    }
}
