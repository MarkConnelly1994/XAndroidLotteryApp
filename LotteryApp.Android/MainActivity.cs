using Android.App;
using Android.OS;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.Fragment.App;
using LotteryApp.Android.Services;
using LotteryApp.Core.Services;

namespace LotteryApp.Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            // Register the service and DI for preferences.
            var preferencesService = new PreferencesService(this);
            ServiceLocator.Register<IPreferencesService>(preferencesService);

            // Register the servie and DI for connectivity.
            var connectivityService = new ConnectivityService(this);
            ServiceLocator.Register<IConnectivityService>(connectivityService);


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
