using System.Reflection;

namespace InterviewManagementSystem.Application.CustomClasses.Extensions;

public static class EnumExtension
{
    internal static bool HasValidValue(this Enum @enum)
    {
        if (@enum == null)
            return false;

        var enumValue = Convert.ToInt16(@enum);
        return enumValue != 0;
    }


    private static string GetNameByEnumStatus<TEnum>(this TEnum @enum) where TEnum : Enum
    {
        var enumType = typeof(TEnum);
        var enumName = @enum.ToString();


        // Use reflection to find a dictionary associated with the enum type, if any
        var extensionType =
            enumType.Assembly.GetType($"{enumType.Namespace}.{enumType.Name}Extension")
            ?? throw new ArgumentException($"No extension class found for enum type {enumType}");



        var bindingAttribute = BindingFlags.Static | BindingFlags.NonPublic;

        // Find the corresponding dictionary in the extension class
        var fieldInfo =
            extensionType.GetField($"{enumType.Name}Map", bindingAttribute)
            ?? throw new ArgumentException($"No mapping dictionary found for enum type {enumType}");


        // Get the dictionary and cast it
        var enumMap = (Dictionary<TEnum, string>)fieldInfo.GetValue(null)!;
        if (enumMap != null && enumMap.TryGetValue(@enum, out string? name))
            return name;


        throw new ArgumentException($"No name mapping found for enum value {enumName}");
    }



    public static string GetNameByValueNumber<TEnum>(this short number) where TEnum : Enum
    {
        var enumType = typeof(TEnum);

        // Check if the provided number is a valid enum value
        if (Enum.IsDefined(enumType, number))
        {
            var enumValue = (TEnum)Enum.ToObject(enumType, number);
            return enumValue.GetNameByEnumStatus();
        }

        throw new ArgumentException($"No enum mapping found for the numeric value {number}");
    }



    public static string GetStatusName<TEnum>(this TEnum benefitEnum) where TEnum : Enum
    {
        var enumType = typeof(TEnum);
        var numericValue = Convert.ToInt16(benefitEnum);

        // Check if the numeric value is defined in the enum
        if (Enum.IsDefined(enumType, numericValue))
        {
            var enumValue = (TEnum)Enum.ToObject(enumType, numericValue);
            return enumValue.GetNameByEnumStatus();
        }

        throw new ArgumentException($"No enum mapping found for the numeric value {numericValue}");
    }
}
