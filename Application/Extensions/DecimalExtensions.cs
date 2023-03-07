namespace Application.Extensions;

public static class DecimalExtensions
{
    public static T RoundDecimalProperties<T>(this T obj, int places = 2) where T : class
    {
        var decimalProperties = typeof(T).GetProperties()
            .Where(p => p.PropertyType == typeof(decimal));

        foreach (var property in decimalProperties)
        {
            var value = (decimal)(property.GetValue(obj) ?? decimal.Zero);
            var roundedValue = Math.Round(value, places);
            property.SetValue(obj, roundedValue);
        }

        return obj;
    }
}