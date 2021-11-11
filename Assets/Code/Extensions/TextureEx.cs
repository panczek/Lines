using UnityEditor;
using UnityEngine;

public static class TextureEx
{
#if UNITY_EDITOR
    /// <summary>
    /// https://twitter.com/einWikinger/status/1264944326657937408
    /// </summary>
    /// <param name="tex"></param>
    public static void MarkAsNormalMap( this Texture tex )
    {
        var so = new SerializedObject( tex );
        so.FindProperty( "m_LightmapFormat" ).intValue = 3;
        so.ApplyModifiedProperties();
    }
#endif
}
