namespace Services.Extensions;

public static class StringExtensions
{
    public static bool IsNotNullOrEmpty(this string value)
        => !(string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(value));

}