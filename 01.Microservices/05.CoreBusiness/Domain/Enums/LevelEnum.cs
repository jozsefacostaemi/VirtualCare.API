using System.Runtime.Serialization;

namespace Domain.Enums
{
    public enum LevelEnum
    {
        [EnumMember(Value = "CITY")]
        CITY = 1,
        [EnumMember(Value = "DEPARTMENT")]
        DEPARTMENT = 2,
        [EnumMember(Value = "COUNTRY")]
        COUNTRY = 3,
        [EnumMember(Value = "SERVICE")]
        SERVICE = 4,
    }
}
