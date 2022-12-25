using System;
using System.ComponentModel;

namespace Bad.Code.BadSmells._14SpeculativeGenerality.enums;

public enum EidiWorkedDaysType
{
    YearlyNormalWorkingDays = 0,
    YearlyNormalWorkingDaysWithSeekLeave = 1,
    YearlyNormalWorkingDaysWithSeekLeaveAndAbsence = 2,
    YearlyNormalWorkingDaysWithAbsence = 3,
}


/// just used in test, dead end code 
public class EidiWorkedDaysTypeEnum
{
    public EidiWorkedDaysType EidiWorkedDaysType { get; set; }
    public EidiWorkedDaysTypeEnum(short id)
    {
        EidiWorkedDaysType = (EidiWorkedDaysType)id;

    }
    public static EidiWorkedDaysType Parse(string title)
        => new EidiWorkedDaysTypeEnum(title).EidiWorkedDaysType;

    public static EidiWorkedDaysType Get(string description)
    {
        var @enum = description.GetEnumFromDescription(typeof(EidiWorkedDaysType));

        return (EidiWorkedDaysType)@enum;
    }

    private EidiWorkedDaysTypeEnum(string title)
    {
        EidiWorkedDaysType = (EidiWorkedDaysType)System.Enum.Parse(typeof(EidiWorkedDaysType), title);
    }
}

public static class EnumExtension
{
    public static short GetEnumFromDescription(this string description, Type enumType)
    {
        foreach (var field in enumType.GetFields())
        {
            DescriptionAttribute attribute
                = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attribute == null)
                continue;
            if (attribute.Description == description)
            {
                return (short)field.GetValue(null);
            }
        }
        return 0;
    }
}