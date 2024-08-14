using Android.Content;
using LotteryApp.Core.Services;

namespace LotteryApp.Android.Services
{
    /// <summary>
    /// Save preferences for android.
    /// </summary>
    public class PreferencesService : IPreferencesService
    {
        private readonly Context _context;

        public PreferencesService(Context context)
        {
            _context = context;
        }

        public void SaveList(string key, string jsonString)
        {
            var prefs = _context.GetSharedPreferences("DevicePreferences", FileCreationMode.Private);
            var editor = prefs.Edit();
            editor.PutString(key, jsonString);
            editor.Apply();
        }

        public string GetList(string key)
        {
            var prefs = _context.GetSharedPreferences("DevicePreferences", FileCreationMode.Private);
            return prefs.GetString(key, string.Empty);
        }
    }
}
