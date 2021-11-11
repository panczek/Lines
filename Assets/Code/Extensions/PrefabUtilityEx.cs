#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;

namespace UnityEditor
{
    public static class PrefabUtilityEx
    {
        /// <summary>
        /// Ugh
        /// IsOutermostPrefabInstanceRoot works fine in Scene and Prefab Stage with Regular Prefabs.
        /// However in Prefab Stage with Prefab Variants, root of the prefab itself reports as Outermost.
        /// So you have to use hack like this to find Prefabs nested in it.
        /// </summary>
        /// <param name="gob"></param>
        /// <param name="stage"></param>
        /// <returns></returns>
        public static bool IsOutermostPrefabInstanceRootIgnoringPrefabStageRootInVariants( GameObject gob, PrefabStage stage )
        {
            if( stage == null || PrefabUtility.GetPrefabAssetType( AssetDatabase.LoadAssetAtPath<GameObject>( stage.assetPath ) ) != PrefabAssetType.Variant )
                return PrefabUtility.IsOutermostPrefabInstanceRoot( gob );

            return PrefabUtility.IsAnyPrefabInstanceRoot( gob ) && gob.transform.parent != null && PrefabUtility.GetNearestPrefabInstanceRoot( gob.transform.parent ) == stage.prefabContentsRoot;
        }

        private static readonly List<Transform> assetChildren = new List<Transform>();
        private static readonly List<Transform> children = new List<Transform>();

        public static GameObject FindObjectFromAssetInPrefabStage( GameObject gob, PrefabStage stage )
        {
            if( !gob.IsPrefabAsset() )
            {
                Debug.LogError( "!gob.IsPrefabAsset()" );
                return null;
            }

            if( AssetDatabase.GetAssetPath( gob.transform.root ) != stage.assetPath )
            {
                Debug.LogError( "AssetDatabase.GetAssetPath( gob.transform.root ) != stage.assetPath" );
                return null;
            }

            assetChildren.Clear();
            children.Clear();

            var assetRoot = gob.transform.root.gameObject;
            assetRoot.GetComponentsInChildren( true, assetChildren );
            int assetIdx = assetChildren.IndexOf( gob.transform );

            stage.prefabContentsRoot.GetComponentsInChildren( true, children );

            var result = children[assetIdx].gameObject;

            assetChildren.Clear();
            children.Clear();

            return result;
        }
    }
}

#endif
