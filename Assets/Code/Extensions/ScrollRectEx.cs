using UnityEngine;
using UnityEngine.UI;

public static class ScrollRectEx
{
    // It assumes that content is anchored and pivoted at the center.
    public static void SnapToX( this ScrollRect scroll, Transform element )
    {
        Canvas.ForceUpdateCanvases();

        var content = scroll.content;
        var rect = scroll.transform as RectTransform;

        content.anchoredPosition = content.anchoredPosition.WithX(
            rect.InverseTransformPoint( content.position ).x
          - rect.InverseTransformPoint( element.position ).x
        );

        var posOld = scroll.horizontalNormalizedPosition;
        scroll.horizontalNormalizedPosition = 0.5f;
        scroll.horizontalNormalizedPosition = Mathf.Clamp( posOld, 0f, 1f );
    }

    public static void SnapToY( this ScrollRect scroll, Transform element )
    {
        Canvas.ForceUpdateCanvases();

        var content = scroll.content;
        var rect = scroll.transform as RectTransform;

        content.anchoredPosition = content.anchoredPosition.WithY(
            rect.InverseTransformPoint( content.position ).y
          - rect.InverseTransformPoint( element.position ).y
        );

        var posOld = scroll.verticalNormalizedPosition;
        scroll.verticalNormalizedPosition = 0.5f;
        scroll.verticalNormalizedPosition = Mathf.Clamp( posOld, 0f, 1f );
    }

    public static void SnapToXY( this ScrollRect scroll, Transform element )
    {
        scroll.SnapToX( element );
        scroll.SnapToY( element );
    }
}
