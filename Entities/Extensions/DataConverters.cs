using System.Globalization;

namespace WizardAPI.Entities.Extensions;

public static class DataConverters
{
    public static DateTime StringToDateTime(string dateTime)
    {
        DateTime.TryParseExact(dateTime, "dd/MM HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None,
            out var dateTimeConverted);
        return dateTimeConverted;
    }
}