// #define TEST_DIRTY

using UnityEngine;

public static class SetDirtyEx
{
    [System.Diagnostics.Conditional( "UNITY_EDITOR" )]
    public static void SetDirtySmart( this Component cmp )
    {
#if UNITY_EDITOR
        if( Application.isPlaying )
            return;

        if( cmp == null )
            return;

#if TEST_DIRTY
        Debug.Log( $"{cmp.GetType().Name} at {cmp.gameObject.GetFullPath()}", cmp.gameObject );
#endif

        UnityEditor.EditorUtility.SetDirty( cmp );
        UnityEditor.EditorUtility.SetDirty( cmp.gameObject );
#endif
    }

    [System.Diagnostics.Conditional( "UNITY_EDITOR" )]
    public static void SetDirtySmart( this GameObject gob )
    {
#if UNITY_EDITOR
        if( Application.isPlaying )
            return;

#if TEST_DIRTY
        Debug.Log( $"GameObject at {gob.GetFullPath()}", gob );
#endif

        UnityEditor.EditorUtility.SetDirty( gob );
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty( gob.scene );
#endif
    }

    [System.Diagnostics.Conditional( "UNITY_EDITOR" )]
    public static void SetDirtySmart( this Object obj )
    {
#if UNITY_EDITOR
#if TEST_DIRTY
        Debug.Log( $"{obj.GetType().Name} with name {obj.nameSafe()}", obj );
#endif

        UnityEditor.EditorUtility.SetDirty( obj );
#endif
    }

}
