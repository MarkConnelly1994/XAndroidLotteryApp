using System;
using System.Globalization;

public static class DateTimeFormatter
{
    public static string FormatDate(string dateString)
    {
        // Parse the input date string to a DateTime object
        DateTime date = DateTime.Parse(dateString);

        // Get the day of the month and its ordinal suffix
        int day = date.Day;
        string dayWithSuffix = day + GetOrdinalSuffix(day);

        // Format the date as "DayOfWeek dayWithSuffix of Month"
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
