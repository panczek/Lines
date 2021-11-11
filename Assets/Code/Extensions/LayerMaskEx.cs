using System.Collections.Generic;
using UnityEngine;

public static class LayerMaskEx
{
    public const int Nothing = 0;
    public const int Everything = int.MaxValue;

    public static bool HasLayer( this LayerMask mask, int layer )
    {
        return ( ( mask.value & ( 1 << layer ) ) != 0 );
    }

    public static LayerMask Create( params int[] layers )
    {
        LayerMask result = 0;

        for( int i = 0; i < layers.Length; i++ )
            result |= ( 1 << layers[i] );

        return result;
    }

    public static LayerMask EnableLayer( this LayerMask mask, int layerID )
    {
        layerID = 1 << layerID;

        LayerMask newMask = new LayerMask
        {
            value = mask.value | layerID
        };

        return newMask;
    }

    public static LayerMask DisableLayer( this LayerMask mask, int layerID )
    {
        layerID = 1 << layerID;

        LayerMask newMask = new LayerMask
        {
            value = mask.value & ~layerID
        };

        return newMask;
    }

    public static string[] LayerNames( string keyword = null )
    {
        var layerNames = new List<string>();

        for( int i = 8; i <= 31; i++ ) // user defined layers start with layer 8 and unity supports 31 layers
        {
            var layerN = LayerMask.LayerToName( i ); // get the name of the layer
            if( layerN.Length > 0 ) // only add the layer if it has been named (comment this line out if you want every layer)
            {
                if( !string.IsNullOrEmpty( keyword ) )
                {
                    if( layerN.Contains( keyword ) )
                        layerNames.Add( layerN );
                }
                else
                    layerNames.Add( layerN );
            }
        }

        return layerNames.ToArray();
    }

    public static void FindObjectsThatMatch( this LayerMask mask )
    {
        GameObject[] gos = Object.FindObjectsOfType<GameObject>(); // will return an array of all GameObjects in the scene
        foreach( GameObject go in gos )
        {
            if( mask.HasLayer( go.layer ) )
            {
                Debug.Log( go.name );
            }
        }
    }
}
