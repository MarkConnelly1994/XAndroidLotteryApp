using System;
using System.Globalization;

public static class DateTimeFormatter
{
    public static string FormatDate(string dateString)
    {
        DateTime date = DateTime.Parse(dateString);

        int day = date.Day;
        string dayWithSuffix = day + GetOrdinalSuffix(day);
        string formattedDate = $"{date:dddd} {dayWithSuffix} of {date:MMMM}";

        return formattedDate;
    }

    private static string GetOrdinalSuffix(int day)
    {
        switch (day % 10)
        {
            case 1 when day != 11: return "st";
            case 2 when day != 12: return "nd";
            case 3 when day != 13: return "rd";
            default: return "th";
        }
    }
}
