using UnityEngine;

public static class Vector4Ex
{
    public static Vector4 WithX( this Vector4 v, float x )
    {
        return new Vector4( x, v.y, v.z, v.w );
    }

    public static Vector4 WithY( this Vector4 v, float y )
    {
        return new Vector4( v.x, y, v.z, v.w );
    }

    public static Vector4 WithZ( this Vector4 v, float z )
    {
        return new Vector4( v.x, v.y, z, v.w );
    }

    public static Vector4 WithW( this Vector4 v, float w )
    {
        return new Vector4( v.x, v.y, v.z, w );
    }
}
