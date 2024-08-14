using Android.App;
using Android.OS;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.Fragment.App;

namespace LotteryApp.Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            // Set up the toolbar
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "ConneLotto";

            // Inflate and add the LotteryDrawFragment
            if (savedInstanceState == null)
            {
                var fragment = new LotteryDrawFragment();
                SupportFragmentManager.BeginTransaction()
                    .Replace(Resource.Id.fragment_container, fragment)
                    .Commit();

                // Load the random number fragment at the bottom
                var numberGeneratorFragment = new NumberGeneratorFragment();
                SupportFragmentManager.BeginTransaction()
                    .Replace(Resource.Id.random_number_fragment_container, numberGeneratorFragment)
                    .Commit();
            }
        }
    }
}
