using System.Runtime.CompilerServices;
using UnityEngine;

public static class RectEx
{
    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Rect Enlarge( this Rect rect, float n )
    {
        rect.xMin -= n;
        rect.yMin -= n;
        rect.xMax += n;
        rect.yMax += n;

        return rect;
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Rect WithCenter( this Rect rect, in Vector2 posNew )
    {
        rect.center = posNew;
        return rect;
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Rect WithPosition( this Rect rect, in Vector2 posNew )
    {
        rect.position = posNew;
        return rect;
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Rect WithSize( this Rect rect, in Vector2 sizeNew )
    {
        rect.size = sizeNew;
        return rect;
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector2 ClampPoint( this Rect rect, in Vector2 point )
    {
        var result = point;

        if( result.x < rect.xMin ) result.x = rect.xMin;
        if( result.x > rect.xMax ) result.x = rect.xMax;

        if( result.y < rect.yMin ) result.y = rect.yMin;
        if( result.y > rect.yMax ) result.y = rect.yMax;

        return result;
    }
}
