using System.ComponentModel;

namespace Services.Extensions;

public static class EnumExtensions
{
    public static string GetDescription<T>(this T value) where T : Enum
    {
        if (value == null) throw new ArgumentNullException(nameof(value));

        var field = value
            .GetType()
            .GetField(value.ToString())!;

        var attributes = (DescriptionAttribute[]) field.GetCustomAttributes(typeof(DescriptionAttribute), false);
        var attribute = attributes.FirstOrDefault();

        if (attribute == null)
            throw new ArgumentException($"Failed to find description for enum: '{value}'");

        return attribute.Description;
    }

    public static bool TryFromDescription<T>(this string value, out T? enumValue) where T : Enum
    {
        enumValue = default;
        
        if (string.IsNullOrWhiteSpace(value)) return false;
        
        var allowedEnumValues = Enum.GetValues(typeof(T)).Cast<T>().ToArray();
        
        if (allowedEnumValues.All(x => x.GetDescription() != value)) return false;
        
        enumValue = allowedEnumValues.FirstOrDefault(x => x.GetDescription() == value);
        
        return enumValue != null;
    }
    
    public static T FromDescription<T>(this string value) where T : Enum
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));

        var enumValue = Enum
            .GetValues(typeof(T))
            .Cast<T>()
            .FirstOrDefault(x => x.GetDescription() == value);

        return enumValue;
    }
}