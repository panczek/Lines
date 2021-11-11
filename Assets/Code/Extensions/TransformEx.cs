using UnityEngine;
using System.Collections.Generic;

public static class TransformEx
{
    /// <summary>
    /// Resets Transform, Rotation, Scale
    /// </summary>
    /// <param name="tr"></param>
    public static void ResetTRS( this Transform tr )
    {
        tr.SetPositionAndRotation( Vector3.zero, Quaternion.identity );
        tr.localScale = Vector3.one;
    }

    public static void LookAt2d( this Transform tr, Vector2 target, float angleDelta = -90f )
    {
        tr.rotation = Vector2Ex.LookAt2DRotation( tr.position, target, angleDelta );
    }

    public static void SetGlobalScale( this Transform tr, Vector3 globalScale )
    {
        tr.localScale = Vector3.one;
        tr.localScale = new Vector3( globalScale.x / tr.lossyScale.x, globalScale.y / tr.lossyScale.y, globalScale.z / tr.lossyScale.z );
    }

    public static void DestroyChildrenSmart( this Transform tr )
    {
        for( int i = tr.childCount - 1; i >= 0; i-- )
        {
            ObjectEx.DestroySmart( tr.GetChild( i ).gameObject );
        }
    }

    public static void DestroyChildrenImmediate(this Transform tr)
    {
        for( int i = tr.childCount - 1; i >= 0; i-- )
        {
            Object.DestroyImmediate( tr.GetChild( i ).gameObject );
        }
    }

    public static void SortChildren( this Transform tr, System.Comparison<Transform> comparison )
    {
        List<Transform> children = new List<Transform>( tr.childCount );
        foreach( Transform child in tr ) children.Add( child );
        children.Sort( comparison );
        for( int i = 0; i < children.Count; ++i )
        {
            children[i].SetSiblingIndex( i );
        }
    }

    public static List<Transform> GetChildrenRecursively( this Transform tr )
    {
        List<Transform> children = new List<Transform>();
        foreach( Transform child in tr )
        {
            children.Add( child );
            children.AddRange( GetChildrenRecursively( child ) );
        }
        return children;
    }
}
