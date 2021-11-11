using UnityEngine;

public static class ObjectEx
{
    public static string nameSafe( this Object obj )
    {
        return obj.Ref()?.name ?? "[null]";
    }

    public static string ToStringSafe( this object obj )
    {
        return obj?.ToString() ?? "[null]";
    }

    public static string ToStringSafe( this Object obj )
    {
        return obj.Ref()?.ToString() ?? "[null]";
    }

    public static void DestroySmart( Object obj )
    {
        #if UNITY_EDITOR
        if( !Application.isPlaying )
        {
            Object.DestroyImmediate( obj );
            return;
        }
        #endif

        Object.Destroy( obj );
    }

    public static GameObject InstantiateSmart( GameObject prefab )
    {
        #if UNITY_EDITOR
        if( !Application.isPlaying )
            return UnityEditor.PrefabUtility.InstantiatePrefab( prefab ) as GameObject;
        #endif

        return Object.Instantiate( prefab );
    }

    /// <summary>
    /// Returns a reference to the Unity Object or null if the object is "fake null" or null.
    /// By using this, the null conditional operator can be safely used as follows: obj.Ref()?.transform;
    /// https://github.com/Haapavuo/UnityExtensions/blob/master/UnityObjectExtensions.cs
    /// </summary>
    /// <typeparam name="T">Type of the object instance.</typeparam>
    /// <param name="o">The object instance.</param>
    /// <returns>Null or the object instance reference.</returns>
    public static T Ref<T>( this T o )
        where T : Object
    {
        // May look like nothing but Unity has overridden the == operator
        return o == null
            ? null
            : o;
    }
}
