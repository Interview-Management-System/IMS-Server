using System.ComponentModel;
using System.Reflection;

namespace InterviewManagementSystem.Domain.Enums.Extensions;

public static class EnumExtension
{

    private static readonly Dictionary<RoleEnum, string> RoleIdEnumMap = new()
    {
        { RoleEnum.Admin, "5900eab6-e2e1-4160-83ee-b985ac709f46" },
        { RoleEnum.Candidate, "83926601-feef-4371-8fbe-40a68fbe8ef6" },
        { RoleEnum.Manager, "60de2025-9b80-4ce2-920c-4628860ccfce" },
        { RoleEnum.Recruiter, "b13ce32b-05a7-480b-817f-099106c463a7" },
        { RoleEnum.Interviewer, "9360eb9d-3aa2-4448-ba57-920cb69043af" },
    };

    /// <summary>
    /// Get RoleId for RoleEnum
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string GetRoleId(this RoleEnum role)
    {
        if (RoleIdEnumMap.TryGetValue(role, out var id))
        {
            return id;
        }

        throw new ArgumentException($"No GUID mapping found for role {role}");
    }

    /// <summary>
    /// Get RoleName by RoleId (GUID)
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string GetRoleNameById(this Guid roleId)
    {
        foreach (var (roleEnum, guidString) in RoleIdEnumMap)
        {
            if (Guid.TryParse(guidString, out var guid) && guid == roleId)
            {
                return roleEnum.GetDescription();
            }
        }

        throw new ArgumentException($"Invalid role ID {roleId}");
    }


    /// <summary>
    /// Get enum description (annotation)
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetDescription<TEnum>(this TEnum value) where TEnum : Enum
    {
        FieldInfo? field = value.GetType().GetField(value.ToString());
        DescriptionAttribute? attribute = field?.GetCustomAttribute<DescriptionAttribute>();

        return attribute?.Description ?? value.ToString();
    }


    public static bool IsNotDefault<TEnum>(this TEnum enumValue) where TEnum : Enum
    {
        return !enumValue.Equals((TEnum)Enum.ToObject(typeof(TEnum), 0));
    }
}
