using UnityEngine;

namespace Code.Shared
{
    public static class Layers
    {
        public static class Int
        {
            public const int Default = 0;
            public const int TransparentFX = 1;
            public const int Ignore_Raycast = 2;
            public const int Water = 4;
            public const int UI = 5;
            public const int Chunks = 6;
            public const int Buildings = 7;

        }

        public static LayerMask GetMask( int i )
        {
            return LayerMaskEx.Create( i );
        }
    }
}
