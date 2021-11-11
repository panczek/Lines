using System.Runtime.CompilerServices;
using UnityEngine;

public static class BoundsEx
{
    public static readonly Bounds Empty = new Bounds( Vector3.zero, Vector3.zero );

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static bool IsValid( this Bounds bounds )
    {
        var lenSq = bounds.size.sqrMagnitude;

        if( lenSq.Approx( 0f ) || lenSq.Approx( Mathf.Infinity ) )
            return false;

        return true;
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector3 BottomCenter( this Bounds bounds )
    {
        return new Vector3( bounds.center.x, bounds.min.y, bounds.min.z );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector3 TopCenter( this Bounds bounds )
    {
        return new Vector3( bounds.center.x, bounds.max.y, bounds.min.z );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static bool Approx( this Bounds bounds, Bounds other )
    {
        return bounds.center.Approx( other.center ) && bounds.size.Approx( other.size );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static bool Contains2d( this Bounds _bounds, Vector2 point )
    {
        return point.x >= _bounds.min.x
            && point.x <= _bounds.max.x
            && point.y >= _bounds.min.y
            && point.y <= _bounds.max.y;
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static bool ContainsBounds( this Bounds _bounds, Bounds other )
    {
        //right
        if( !_bounds.Contains( other.center.WithX( other.center.x + other.extents.x ) ) )
            return false;

        //left
        if( !_bounds.Contains( other.center.WithX( other.center.x - other.extents.x ) ) )
            return false;

        //up
        if( !_bounds.Contains( other.center.WithY( other.center.y + other.extents.y ) ) )
            return false;

        //down
        if( !_bounds.Contains( other.center.WithY( other.center.y - other.extents.y ) ) )
            return false;

        return true;
    }

    private static readonly Vector2[] corners2d = new Vector2[4];

    public static Vector2[] GetCornersXZ( this Bounds bounds )
    {
        var min = bounds.min;
        var max = bounds.max;

        corners2d[0] = new Vector2( min.x, min.z );
        corners2d[1] = new Vector2( min.x, max.z );
        corners2d[2] = new Vector2( max.x, min.z );
        corners2d[3] = new Vector2( max.x, max.z );

        return corners2d;
    }

    private static readonly Vector3[] corners = new Vector3[8];

    public static Vector3[] GetCorners( this Bounds bounds )
    {
        var min = bounds.min;
        var max = bounds.max;

        corners[0] = new Vector3( min.x, min.y, min.z );
        corners[1] = new Vector3( min.x, max.y, min.z );
        corners[2] = new Vector3( max.x, min.y, min.z );
        corners[3] = new Vector3( max.x, max.y, min.z );

        corners[4] = new Vector3( min.x, min.y, max.z );
        corners[5] = new Vector3( min.x, max.y, max.z );
        corners[6] = new Vector3( max.x, min.y, max.z );
        corners[7] = new Vector3( max.x, max.y, max.z );

        return corners;
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Bounds Translate( this Bounds bounds, Vector3 translation )
    {
        return new Bounds( bounds.center + translation, bounds.size );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Bounds WithHeight( this Bounds bounds, float height ) => new Bounds( bounds.center, bounds.size = bounds.size.WithY( height ) );
}
