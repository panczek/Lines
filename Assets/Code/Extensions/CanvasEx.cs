using UnityEngine;

public static class CanvasEx
{
    /// <summary>
    /// Bottom Left; Top Left; Top Right; Bottom Right
    /// </summary>
    private readonly static Vector3[] corners = new Vector3[4];

    /// <summary>
    /// Check if Canvas rect contains ANY of the item corners
    /// </summary>
    /// <param name="canvas"></param>
    /// <param name="rt"></param>
    /// <returns></returns>
    public static bool OverlapRectTransform( this Canvas canvas, RectTransform rt )
    {
        var canvasRt = canvas.transform as RectTransform;

        rt.GetWorldCorners( corners );

        for( int i = 0; i < 4; i++ )
        {
            Vector2 cornerInCanvasSpace = canvasRt.InverseTransformPoint( corners[i] );

            bool contains = canvasRt.rect.Contains( cornerInCanvasSpace );

            if( contains )
                return true;
        }

        return false;
    }

    /// <summary>
    /// Check if Canvas rect contains ALL of the item corners
    /// </summary>
    /// <param name="canvas"></param>
    /// <param name="rt"></param>
    /// <returns></returns>
    public static bool ContainsRectTransform( this Canvas canvas, RectTransform rt )
    {
        var canvasRt = canvas.transform as RectTransform;

        rt.GetWorldCorners( corners );

        for( int i = 0; i < 4; i++ )
        {
            Vector2 cornerInCanvasSpace = canvasRt.InverseTransformPoint( corners[i] );

            bool contains = canvasRt.rect.Contains( cornerInCanvasSpace );

            if( !contains )
                return false;
        }

        return true;
    }

    public static Vector2 WorldToCanvasSpace( this Canvas canvas, Transform target )
    {
        // first you need the RectTransform component of your canvas
        RectTransform CanvasRect = canvas.GetComponent<RectTransform>();

        /*  then you calculate the position of the UI element, 0,0 for the canvas is at the center of
            the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this,
            you need to subtract the height / width of the canvas * 0.5 to get the correct position.
        */
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint( target.position );

        // clamp to the screen boundaries
        ViewportPosition.x = Mathf.Clamp01( ViewportPosition.x );
        ViewportPosition.y = Mathf.Clamp01( ViewportPosition.y );

        Vector2 WorldObject_ScreenPosition = new Vector2( ( ( ViewportPosition.x * CanvasRect.sizeDelta.x ) - ( CanvasRect.sizeDelta.x * 0.5f ) ),
                ( ( ViewportPosition.y * CanvasRect.sizeDelta.y ) /*- ( CanvasRect.sizeDelta.y * 0.5f )*/ ) );

        // now you can set the position of the ui element
        return WorldObject_ScreenPosition;
    }
}
