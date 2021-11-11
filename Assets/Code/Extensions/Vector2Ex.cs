using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class Vector2Ex
{
    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector2 AddX( this Vector2 v, float diffX )
    {
        return v.WithX( v.x + diffX );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector2 AddY( this Vector2 v, float diffY )
    {
        return v.WithY( v.y + diffY );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static bool Approx( this Vector2 v1, Vector2 v2 )
    {
        return Vector2.Distance( v1, v2 ) < 0.01f;
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector2 Div( this Vector2 a, Vector2 b )
    {
        return new Vector2( a.x / b.x, a.y / b.y );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static bool IsNaN( this Vector2 a )
    {
        return float.IsNaN( a.x ) || float.IsNaN( a.y );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector2 Rotate( this Vector2 v, float angle )
    {
        float radians = angle * Mathf.Deg2Rad;
        float sin = Mathf.Sin( radians );
        float cos = Mathf.Cos( radians );

        return new Vector2( cos * v.x - sin * v.y, sin * v.x + cos * v.y );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static float LookAt2dAngle( Vector2 from, Vector2 to, float angleDelta = -90f )
    {
        var diff = to - from;
        diff.Normalize();

        float rot_z = Mathf.Atan2( diff.y, diff.x ) * Mathf.Rad2Deg;
        return rot_z + angleDelta;
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Quaternion LookAt2DRotation( Vector2 from, Vector2 to, float angleDelta = -90f )
    {
        float angle = LookAt2dAngle( from, to, angleDelta );
        return Quaternion.AngleAxis( angle, Vector3.forward );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static float Max( this Vector2 v )
    {
        return Mathf.Max( v.x, v.y );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector2 Max( Vector2 v1, Vector2 v2 )
    {
        return new Vector2()
        {
            x = Mathf.Max( v1.x, v2.x ),
            y = Mathf.Max( v1.y, v2.y )
        };
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static float Min( this Vector2 v )
    {
        return Mathf.Min( v.x, v.y );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector2 Min( Vector2 v1, Vector2 v2 )
    {
        return new Vector2()
        {
            x = Mathf.Min( v1.x, v2.x ),
            y = Mathf.Min( v1.y, v2.y )
        };
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector2 Mul( this Vector2 a, Vector2 b )
    {
        return new Vector2( a.x * b.x, a.y * b.y );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector2 Perpendicular( this Vector2 v )
    {
        return new Vector2( -v.y, v.x );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector2 Perpendicular( this Vector2 v, float length )
    {
        return new Vector2( -v.y, v.x ).normalized * length;
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector3 ToVec3XY( this Vector2 v, float z = 0f )
    {
        return new Vector3( v.x, v.y, z );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector3 ToVec3XZ( this Vector2 v, float y = 0f )
    {
        return new Vector3( v.x, y, v.y );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector2 ToVec2( this float v )
    {
        return new Vector2( v, v );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector2 WithX( this Vector2 v, float x )
    {
        return new Vector2( x, v.y );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector2 WithY( this Vector2 v, float y )
    {
        return new Vector2( v.x, y );
    }

    /// <summary>
    /// 0 deg is on right
    /// </summary>
    /// <returns>Angle in degrees</returns>
    public static float AngleBetweenPoints( Vector2 from, Vector2 to )
    {
        var delta = to - from;
        var angleRad = Mathf.Atan2( delta.y, delta.x );
        return angleRad * Mathf.Rad2Deg;
    }

    public class Comparer2d : EqualityComparer<Vector2>
    {
        public override bool Equals( Vector2 a, Vector2 b )
        {
            return a.x.Approx( b.x ) && a.y.Approx( b.y );
        }

        public override int GetHashCode( Vector2 obj )
        {
            return obj.GetHashCode();
        }
    }
}
