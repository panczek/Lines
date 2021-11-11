using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class Vector3Ex
{
    public static Vector3 Incorrect => new Vector3( -10000f, -10000f, -10000f );

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector3 AddX( this Vector3 v, float diffX )
    {
        return new Vector3( v.x + diffX, v.y, v.z );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector3 AddY( this Vector3 v, float diffY )
    {
        return new Vector3( v.x, v.y + diffY, v.z );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector3 AddZ( this Vector3 v, float diffZ )
    {
        return new Vector3( v.x, v.y, v.z + diffZ );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector3 AddXY( this Vector3 v, float diffX, float diffY )
    {
        return new Vector3( v.x + diffX, v.y + diffY, v.z );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector3 AddXY( this Vector3 v, Vector2 diff )
    {
        return new Vector3( v.x + diff.x, v.y + diff.y, v.z );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static bool Approx( this Vector3 v1, Vector3 v2 )
    {
        return Vector3.Distance( v1, v2 ) < 0.01f;
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static bool Approx2d( this Vector3 v1, Vector3 v2 )
    {
        return Vector2.Distance( v1, v2 ) < 0.01f;
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector3 Div( this Vector3 a, Vector3 b )
    {
        return new Vector3( a.x / b.x, a.y / b.y, a.z / b.z );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static bool IsNaN( this Vector3 a )
    {
        return float.IsNaN( a.x ) || float.IsNaN( a.y ) || float.IsNaN( a.z );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector3 Mul( this Vector3 a, Vector3 b )
    {
        return new Vector3( a.x * b.x, a.y * b.y, a.z * b.z );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector3 RotateAround( this Vector3 point, Vector3 pivot, Quaternion angles )
    {
        Vector3 dir = point - pivot; // get point direction relative to pivot
        dir = angles * dir; // rotate it
        point = dir + pivot; // calculate rotated point
        return point; // return it
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector3 ToVec3( this float v )
    {
        return new Vector3( v, v, v );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector3 WithX( this Vector3 v, float x )
    {
        return new Vector3( x, v.y, v.z );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector3 WithY( this Vector3 v, float y )
    {
        return new Vector3( v.x, y, v.z );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector3 WithZ( this Vector3 v, float z )
    {
        return new Vector3( v.x, v.y, z );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector2 ToVec2XZ( this Vector3 v )
    {
        return new Vector2( v.x, v.z );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector2 ToVec2XY( this Vector3 v )
    {
        return new Vector2( v.x, v.y );
    }

    /// <summary>
    /// consistent with scaling of Agent avoidance radius
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static float GetMaxXZ( this Vector3 v )
    {
        return v.x >= v.z
            ? v.x
            : v.z;
    }

    private static float[] scalars = new float[3];

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static float GetMax( this Vector3 v )
    {
        scalars[0] = v.x;
        scalars[1] = v.y;
        scalars[2] = v.z;

        return Mathf.Max( scalars );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static float GetMin( this Vector3 v )
    {
        scalars[0] = v.x;
        scalars[1] = v.y;
        scalars[2] = v.z;

        return Mathf.Min( scalars );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector3 ProjectPointOnLine( Vector3 origin, Vector3 end, Vector3 toProject )
    {
        var ap = toProject - origin;
        var ab = end - origin;

        if( ap.Approx( Vector3.zero ) || ab.Approx( Vector3.zero ) )
            return origin;

        return origin + Vector3.Dot( ap, ab ) / Vector3.Dot( ab, ab ) * ab;
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector3 ProjectPointOnSegment( Vector3 origin, Vector3 end, Vector3 toProject )
    {
        var ap = toProject - origin;
        var ab = end - origin;

        if( ap.Approx( Vector3.zero ) || ab.Approx( Vector3.zero ) )
            return origin;

        return origin + Mathf.Clamp01( Vector3.Dot( ap, ab ) / Vector3.Dot( ab, ab ) ) * ab;
    }

    public class Comparer3d : EqualityComparer<Vector3>
    {
        public override bool Equals( Vector3 a, Vector3 b )
        {
            return a.x.Approx( b.x ) && a.y.Approx( b.y ) && a.z.Approx( b.z );
        }

        public override int GetHashCode( Vector3 obj )
        {
            return obj.GetHashCode();
        }
    }
}
