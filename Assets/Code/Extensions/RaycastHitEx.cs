using System.Runtime.CompilerServices;
using UnityEngine;

public static class RaycastHitEx
{
    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public static GameObject GetGameObject( this RaycastHit hit )
    {
        if( hit.rigidbody )
            return hit.rigidbody.gameObject;

        if( hit.collider )
            return hit.collider.gameObject;

        return null;
    }
}
