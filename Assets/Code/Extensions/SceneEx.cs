using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneEx
{
    private static readonly List<GameObject> rootObjects = new List<GameObject>();

    public static T FindObjectInSceneRoot<T>( this Scene scene, bool includeInactive = true )
        where T : Component
    {
        if( !scene.IsValid() )
        {
            Debug.LogWarning( $"Scene is not valid. {scene.name}" );
            return null;
        }

        if( !scene.isLoaded )
            return null;

        rootObjects.Clear();
        scene.GetRootGameObjects( rootObjects );

        foreach( var rootObj in rootObjects )
        {
            if( rootObj.TryGetComponent<T>( out var cmp ) )
            {
                if( !includeInactive )
                    if( cmp is MonoBehaviour mb )
                        if( !mb.isActiveAndEnabled )
                            continue;

                rootObjects.Clear();
                return cmp;
            }
        }

        rootObjects.Clear();
        return null;
    }

    public static void FindObjectsInScene<T>( this Scene scene, ref List<T> objectsFound, bool clear = true, bool includeInactive = true )
    {
        if( clear )
            objectsFound.Clear();

        if( !scene.IsValid() )
        {
            Debug.LogWarning( $"Scene is not valid. {scene.name}" );
            return;
        }

        // if( !scene.isLoaded )
        // {
        //     Debug.LogWarning( $"Scene is not loaded. {scene.name}" );
        //     return;
        // }

        rootObjects.Clear();
        scene.GetRootGameObjects( rootObjects );

        var resultsTmp = new List<T>();

        foreach( var rootObj in rootObjects )
        {
            if( !includeInactive && !rootObj.activeSelf )
                continue;

            rootObj.GetComponentsInChildren( includeInactive, resultsTmp );
            objectsFound.AddRange( resultsTmp );
        }

        rootObjects.Clear();
    }
}
