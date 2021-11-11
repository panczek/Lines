using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public static class RectTransformEx
{
    private static readonly Vector3[] corners = new Vector3[4];
    private static readonly Vector3[] cornersParent = new Vector3[4];

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static void SetAnchoredPositionX( this RectTransform rt, float newX )
    {
        rt.anchoredPosition = rt.anchoredPosition.WithX( newX );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static void SetAnchoredPositionY( this RectTransform rt, float newY )
    {
        rt.anchoredPosition = rt.anchoredPosition.WithY( newY );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static void SetAnchorsBoth( this RectTransform rt, Vector2 newVal )
    {
        rt.anchorMin = newVal;
        rt.anchorMax = newVal;
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static void SetAnchorsX( this RectTransform rt, float newX )
    {
        rt.anchorMin = rt.anchorMin.WithX( newX );
        rt.anchorMax = rt.anchorMax.WithX( newX );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static void SetAnchorsY( this RectTransform rt, float newY )
    {
        rt.anchorMin = rt.anchorMin.WithY( newY );
        rt.anchorMax = rt.anchorMax.WithY( newY );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static void SetAnchorsMin( this RectTransform rt, float newMin )
    {
        rt.anchorMin = new Vector2( newMin, newMin );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static void SetAnchorsMax( this RectTransform rt, float newMax )
    {
        rt.anchorMax = new Vector2( newMax, newMax );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static Vector2 GetCenterInWorld( this RectTransform rt )
    {
        rt.GetWorldCorners( corners );

        float x = 0f;
        float y = 0f;

        for( int v = 0; v < corners.Length; v++ )
        {
            var c = corners[v];

            x += c.x;
            y += c.y;
        }

        float l = corners.Length;

        return new Vector2( x / l, y / l );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static void SetOffsets( this RectTransform rt, float offset )
    {
        rt.offsetMin = new Vector2( offset, offset );
        rt.offsetMax = new Vector2( -offset, -offset );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static void SetOffsets( this RectTransform rt, float horizontal, float vertical )
    {
        rt.offsetMin = new Vector2( horizontal, vertical );
        rt.offsetMax = new Vector2( -horizontal, -vertical );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static void SetOffsets( this RectTransform rt, float left, float right, float top, float bottom )
    {
        rt.offsetMin = new Vector2( left, bottom );
        rt.offsetMax = new Vector2( -right, -top );
    }

    // returns normalized rect relative to given relativeRect, if relativeRect is null then parent will be used
    // this will work very very wrong with rotated transforms and when transforms are not on exacly the same plane
    public static Rect GetNormalizedRect( this RectTransform rt, RectTransform relativeRect = null )
    {
        if( relativeRect == null ) relativeRect = rt.parent as RectTransform;

        if( relativeRect )
        {
            // It starts bottom left and rotates to top left
            rt.GetWorldCorners( corners );
            relativeRect.GetWorldCorners( cornersParent );

            Vector3 myMin = corners[0];
            Vector3 myMax = corners[2];

            Vector3 relativeMin = cornersParent[0];
            Vector3 relativeMax = cornersParent[2];

            float x = Mathf.InverseLerp( relativeMin.x, relativeMax.x, myMin.x );
            float y = Mathf.InverseLerp( relativeMin.y, relativeMax.y, myMin.y );

            float myWidth = Vector3.Distance( corners[1], corners[2] );
            float myHeight = Vector3.Distance( corners[0], corners[1] );

            float relWidth = Vector3.Distance( cornersParent[1], cornersParent[2] );
            float relHeight = Vector3.Distance( cornersParent[0], cornersParent[1] );

            return new Rect( x, y, myWidth / relWidth, myHeight / relHeight );
        }
        else
        {
            Debug.LogWarning( "Parent is null or not RectTransform" );
            return default;
        }
    }

    public static void LayoutImmediate( this RectTransform rt )
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate( rt );
    }
}
