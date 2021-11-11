using System.Runtime.CompilerServices;
using UnityEngine;

public static class FloatEx
{
    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static bool Approx( this float a, float b )
    {
        return Mathf.Approximately( a, b );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static bool Approx( this float a, float b, float epsilon )
    {
        return Mathf.Abs( a - b ) <= epsilon;
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static float Remap( this float value, float rangeFromMin, float rangeFromMax, float rangeToMin, float rangeToMax )
    {
        return ( value - rangeFromMin ) / ( rangeFromMax - rangeFromMin ) * ( rangeToMax - rangeToMin ) + rangeToMin;
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static float RemapClamped( this float value, float rangeFromMin, float rangeFromMax, float rangeToMin, float rangeToMax )
    {
        var result = ( value - rangeFromMin ) / ( rangeFromMax - rangeFromMin ) * ( rangeToMax - rangeToMin ) + rangeToMin;

        if( result < rangeToMin )
            result = rangeToMin;

        if( result > rangeToMax )
            result = rangeToMax;

        return result;
    }
}
