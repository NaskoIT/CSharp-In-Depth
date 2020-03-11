using System;

namespace CombinableEnumValues
{
    [Flags]
    public enum Margins
    {
        None = 0,
        Top = 1,
        Left = 2,
        Bottom = 4,
        Right = 8
    }
}
