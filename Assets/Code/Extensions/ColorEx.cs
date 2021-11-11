using System.Collections.Generic;
using UnityEngine;

public static class ColorEx
{
    public static Color WithAlpha( this Color col, float a )
    {
        col.a = a;
        return col;
    }

    public static Color Darker( this Color col, float mul = 0.65f )
    {
        return new Color( col.r * mul, col.g * mul, col.b * mul, col.a );
    }

    private readonly static EqualityComparer comparer = new EqualityComparer();

    public static bool Approx( this Color a, Color b )
    {
        return comparer.Equals( a, b );
    }

    public static bool Approx( this Color32 a, Color32 b )
    {
        return comparer.Equals( a, b );
    }

    public class LightnessComparer : IComparer<Color>
    {
        public int Compare( Color x, Color y )
        {
            float ix = .2126f * x.r + .7152f * x.g + .0722f * x.b;
            float iy = .2126f * y.r + .7152f * y.g + .0722f * y.b;

            return ix.CompareTo( iy );
        }
    }

    public class EqualityComparer : IEqualityComparer<Color>
    {
        public float Epsilon = 0.001f;

        public bool Equals( Color x, Color y )
        {
            return x.r.Approx( y.r, Epsilon )
                   && x.g.Approx( y.g, Epsilon )
                   && x.b.Approx( y.b, Epsilon )
                   && x.a.Approx( y.a, Epsilon );
        }

        public int GetHashCode( Color obj )
        {
            return obj.GetHashCode();
        }
    }
}
