using UnityEngine;

public static class DebugEx
{
    private static readonly Vector3[] verticesLocalSpace = new Vector3[8];
    private static readonly Vector3[] verticesWorldSpace = new Vector3[8];

    public static void DrawBoxCast( Vector3 origin, Vector3 extents, Vector3 direction, Quaternion orientation, float maxDistance, Color color, float duration )
    {
        direction.Normalize();

        // var left = Vector3.Cross( direction, Vector3.up );
        // Debug.DrawLine( origin, origin + direction * maxDistance, Color.blue );
        // Debug.DrawLine( origin, Vector3.up * maxDistance, Color.red );
        // Debug.DrawLine( origin, origin + left * maxDistance, Color.magenta );

        // bottom
        verticesLocalSpace[0] = new Vector3( -extents.x, -extents.y, -extents.z );
        verticesLocalSpace[1] = new Vector3( +extents.x, -extents.y, -extents.z );
        verticesLocalSpace[2] = new Vector3( -extents.x, -extents.y, +extents.z );
        verticesLocalSpace[3] = new Vector3( +extents.x, -extents.y, +extents.z );

        // top
        verticesLocalSpace[4] = new Vector3( -extents.x, +extents.y, -extents.z );
        verticesLocalSpace[5] = new Vector3( +extents.x, +extents.y, -extents.z );
        verticesLocalSpace[6] = new Vector3( -extents.x, +extents.y, +extents.z );
        verticesLocalSpace[7] = new Vector3( +extents.x, +extents.y, +extents.z );

        for( int v = 0; v < verticesLocalSpace.Length; v++ )
            verticesWorldSpace[v] = origin + ( orientation * verticesLocalSpace[v] );

        for( int v = 0; v < verticesWorldSpace.Length; v++ )
            Debug.DrawRay( verticesWorldSpace[v], direction * maxDistance, color, duration );
    }

    public static void DrawBox( Vector3 origin, Vector3 extents, Quaternion orientation, Color color, float duration )
    {
        verticesLocalSpace[0] = new Vector3( -extents.x, -extents.y, -extents.z );
        verticesLocalSpace[1] = new Vector3( +extents.x, -extents.y, -extents.z );
        verticesLocalSpace[2] = new Vector3( -extents.x, -extents.y, +extents.z );
        verticesLocalSpace[3] = new Vector3( +extents.x, -extents.y, +extents.z );

        // top
        verticesLocalSpace[4] = new Vector3( -extents.x, +extents.y, -extents.z );
        verticesLocalSpace[5] = new Vector3( +extents.x, +extents.y, -extents.z );
        verticesLocalSpace[6] = new Vector3( -extents.x, +extents.y, +extents.z );
        verticesLocalSpace[7] = new Vector3( +extents.x, +extents.y, +extents.z );

        for( int v = 0; v < verticesLocalSpace.Length; v++ )
            verticesWorldSpace[v] = origin + ( orientation * verticesLocalSpace[v] );

        // bottom
        Debug.DrawLine( verticesWorldSpace[0], verticesWorldSpace[1], color, duration );
        Debug.DrawLine( verticesWorldSpace[0], verticesWorldSpace[2], color, duration );
        Debug.DrawLine( verticesWorldSpace[2], verticesWorldSpace[3], color, duration );
        Debug.DrawLine( verticesWorldSpace[1], verticesWorldSpace[3], color, duration );

        // top
        Debug.DrawLine( verticesWorldSpace[4 + 0], verticesWorldSpace[4 + 1], color, duration );
        Debug.DrawLine( verticesWorldSpace[4 + 0], verticesWorldSpace[4 + 2], color, duration );
        Debug.DrawLine( verticesWorldSpace[4 + 2], verticesWorldSpace[4 + 3], color, duration );
        Debug.DrawLine( verticesWorldSpace[4 + 1], verticesWorldSpace[4 + 3], color, duration );

        // sides
        Debug.DrawLine( verticesWorldSpace[0], verticesWorldSpace[4 + 0], color, duration );
        Debug.DrawLine( verticesWorldSpace[1], verticesWorldSpace[4 + 1], color, duration );
        Debug.DrawLine( verticesWorldSpace[2], verticesWorldSpace[4 + 2], color, duration );
        Debug.DrawLine( verticesWorldSpace[3], verticesWorldSpace[4 + 3], color, duration );
    }
}
