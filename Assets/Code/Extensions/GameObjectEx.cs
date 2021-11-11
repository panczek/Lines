using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

public static class GameObjectEx
{
    public static void EnsureLayer( this GameObject gob, int layer )
    {
        if( gob == null )
        {
            Debug.LogError( "EnsureLayer: gob == null" );
            return;
        }

        if( gob.layer != layer )
        {
            Debug.LogWarningFormat( gob, "EnsureLayer: Setting '{0}' Layer from '{1}' to '{2}'", gob.name, LayerMask.LayerToName( gob.layer ), LayerMask.LayerToName( layer ) );
            gob.layer = layer;
            gob.SetDirtySmart();
        }
    }

    public static void EnsureTag( this GameObject gob, string tag )
    {
        if( gob == null )
        {
            Debug.LogError( "EnsureTag: gob == null" );
            return;
        }

        if( !gob.CompareTag( tag ) )
        {
            Debug.LogWarning( $"EnsureTag: Setting '{gob.name}' Tag from '{gob.tag}' to '{tag}'", gob );
            gob.tag = tag;
            gob.SetDirtySmart();
        }
    }

    public static T EnsureComponent<T>( this GameObject gob )
        where T : Component
    {
        if( gob == null )
        {
            Debug.LogError( "EnsureBehaviour: gob == null" );
            return null;
        }

        if( !gob.TryGetComponent<T>( out var result ) )
        {
            Debug.LogWarning( $"EnsureBehaviour: Adding Behaviour {typeof( T ).Name}", gob );
            result = gob.AddComponent<T>();
            gob.SetDirtySmart();
        }

        return result;
    }

    public static void SetLayerRecursively( this GameObject gob, int layer )
    {
        gob.layer = layer;
        var tr = gob.transform;

        for( int i = 0; i < tr.childCount; i++ )
            SetLayerRecursively( tr.GetChild( i ).gameObject, layer );
    }

    public static bool IsPrefabAsset( this GameObject gob )
    {
        if( gob == null )
            return false;

#if UNITY_EDITOR
        // Debug.Log( "Gob IsPrefab", gob );
        if( UnityEditor.PrefabUtility.IsPartOfPrefabAsset( gob ) )
        {
            return true;
        }

        return UnityEditor.Experimental.SceneManagement.PrefabStageUtility.GetPrefabStage( gob ) != null;
#else
        return false;
#endif
    }

    public static string GetFullPath( this GameObject gob, bool includeScene = true )
    {
        var sb = new StringBuilder( 128 );

        if( !gob )
        {
            sb.Append( "[Destroyed]" );
            return sb.ToString();
        }

#if UNITY_EDITOR
        if( !gob.scene.IsValid() )
        {
            sb.Append( UnityEditor.AssetDatabase.GetAssetPath( gob ) );
            sb.Append( $" (gob:{gob.name})" );
            return sb.ToString();
        }
#endif

        var gobToUse = gob;

        while( gobToUse != null )
        {
            sb.Insert( 0, gobToUse.name );
            sb.Insert( 0, "/" );

            var parent = gobToUse.transform.parent;

            gobToUse = parent
                       ? parent.gameObject
                       : null;
        }

        if( includeScene && gobToUse == null )
        {
            if( gob.scene.IsValid() )
                sb.Insert( 0, gob.scene.name );
            else
                sb.Insert( 0, "Assets/(...)" );
        }

        return sb.ToString();
    }

    public static T GetInParent<T>( this GameObject gob, bool includeInactive )
        where T : Component
    {
        return FindCmpHelper<T>.GetInParent( gob, includeInactive );
    }

    [Obsolete( "Use TryGetComponent instead." )]
    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    [System.ComponentModel.EditorBrowsable( System.ComponentModel.EditorBrowsableState.Never )]
    public static T GetComponentNonAlloc<T>( this GameObject gob )
        where T : Component
    {
        if( gob.TryGetComponent<T>( out var result ) )
            return result;
        else
            return null;
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static GameObject GetParent( this GameObject gob ) => gob.Ref()?.transform.parent.Ref()?.gameObject;
}

public static class GameObjectEx<T>
{
    private static List<T> cmps = new List<T>();

    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static T GetComponentInChildrenExcludingSelf( GameObject gob, bool includeInactive )
    {
        cmps.Clear();

        gob.GetComponentsInChildren<T>( includeInactive, cmps );

        T result = default;

        foreach( var c in cmps )
        {
            if( ( c as Component )?.gameObject != gob )
            {
                result = c;
                break;
            }
        }

        cmps.Clear();

        return result;
    }
}
