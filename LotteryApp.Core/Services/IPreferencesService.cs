namespace LotteryApp.Core.Services
{
    public interface IPreferencesService
    {
        void SaveList(string key, string jsonString);
        string GetList(string key);
    }
}
