using System.Runtime.CompilerServices;
using UnityEngine;

public static class MathEx
{
    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static int Repeat( int t, int length )
    {
        return t - Mathf.FloorToInt( t / (float)length ) * length;
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static int SignI( float val, float epsilon = 0.001f )
    {
        if( val.Approx( 0f, epsilon ) )
            return 0;

        if( val < 0 )
            return -1;

        return 1;
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static float SignF( float val, float epsilon = 0.001f )
    {
        if( val.Approx( 0f, epsilon ) )
            return 0f;

        if( val < 0f )
            return -1f;

        return 1f;
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static float ClampWithDeadzone( float val, float epsilon = 0.001f )
    {
        if( val.Approx( 0f, epsilon ) )
            return 0f;

        return Mathf.Clamp( val, -1f, 1f );
    }

    /// <summary>
    /// Make sure angle is within 0,360 range
    /// </summary>
    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static float WrapAngle( float angle )
    {
        // If its negative rotate until its positive
        while( angle < 0 )
            angle += 360;

        // If its to positive rotate until within range
        return Mathf.Repeat( angle, 360 );
    }

    public static float TriangleArea( Vector3 p1, Vector3 p2, Vector3 p3 )
    {
        return Vector3.Cross( p1 - p2, p1 - p3 ).magnitude * 0.5f;
    }

    public static float Atan2Wrapped360( float x, float y )
    {
        float value = ( ( Mathf.Atan2( x, y ) / Mathf.PI ) * 180f );
        if( value < 0 ) value += 360f;

        return value;
    }
}
