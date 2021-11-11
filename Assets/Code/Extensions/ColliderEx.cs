using System;
using UnityEngine;

public static class ColliderEx
{
    public static GameObject GetGameObject( this Collider col )
    {
        if( col.attachedRigidbody )
            return col.attachedRigidbody.gameObject;
        else
            return col.gameObject;
    }

    /// <summary>
    /// Scene: Test ColliderOverlap
    /// </summary>
    /// <param name="col"></param>
    /// <param name="hits"></param>
    /// <param name="mask"></param>
    /// <param name="triggerMode"></param>
    /// <returns></returns>
    public static int OverlapNonAlloc( this Collider col, Collider[] hits, LayerMask mask, QueryTriggerInteraction triggerMode = QueryTriggerInteraction.Collide )
    {
        var tr = col.transform;

        {
            var box = col as BoxCollider;
            if( box != null )
            {
                var pos = tr.TransformPoint( box.center );
                var size = tr.lossyScale.Mul( box.size );

                // DebugEx.DrawBox( pos, size / 2f, tr.rotation, Color.red, 1f );

                return Physics.OverlapBoxNonAlloc( pos, size / 2f, hits, tr.rotation, mask.value, triggerMode );
            }
        }

        {
            var sphere = col as SphereCollider;
            if( sphere != null )
            {
                var pos = tr.TransformPoint( sphere.center );
                var radius = tr.lossyScale.GetMax() * sphere.radius;

                return Physics.OverlapSphereNonAlloc( pos, radius, hits, mask.value, triggerMode );
            }
        }

        {
            var capsule = col as CapsuleCollider;
            if( capsule != null )
            {
                Quaternion rot = Quaternion.identity;

                switch( capsule.direction )
                {
                    case 0: rot = Quaternion.Euler( 0f, 0f, 90f ); break; // x
                    case 1: break; // y
                    case 2: rot = Quaternion.Euler( 90f, 0f, 0f ); break; // z
                }

                var radius = tr.lossyScale.GetMax() * capsule.radius;

                var a = tr.TransformPoint( rot * capsule.center.AddY( -capsule.height / 2f + radius ) );
                var b = tr.TransformPoint( rot * capsule.center.AddY( +capsule.height / 2f - radius ) );

                // Debug.DrawLine( tr.position, a, Color.magenta );
                // Debug.DrawLine( tr.position, b, Color.yellow );

                return Physics.OverlapCapsuleNonAlloc( a, b, radius, hits, mask.value, triggerMode );
            }
        }

        Debug.LogError( $"Not supported collider type: {col.GetType().Name}", col.gameObject );
        return 0;
    }

    public static bool Cast( this Collider col, Vector3 direction, float maxDistance, out RaycastHit hit, LayerMask layerMask, QueryTriggerInteraction triggerMode = QueryTriggerInteraction.Collide )
    {
        var transform = col.transform;

        switch( col )
        {
            case BoxCollider boxCollider:
                {
                    var origin = transform.TransformPoint( boxCollider.center );
                    var halfSize = transform.lossyScale.Mul( boxCollider.size ) * 0.5f;
                    if( Physics.BoxCast( origin, halfSize, direction, out hit, transform.rotation, maxDistance, layerMask, triggerMode ) )
                    {
                        return true;
                    }
                    break;
                }

            case SphereCollider sphereCollider:
                {
                    var origin = transform.TransformPoint( sphereCollider.center );
                    var radius = transform.lossyScale.GetMax() * sphereCollider.radius;
                    if( Physics.SphereCast( origin, radius, direction, out hit, maxDistance, layerMask, triggerMode ) )
                    {
                        return true;
                    }
                    break;
                }

            case CapsuleCollider capsuleCollider:
                {
                    Quaternion rot = Quaternion.identity;

                    switch( capsuleCollider.direction )
                    {
                        case 0: rot = Quaternion.Euler( 0f, 0f, 90f ); break; // x
                        case 1: break; // y
                        case 2: rot = Quaternion.Euler( 90f, 0f, 0f ); break; // z
                    }

                    var radius = transform.lossyScale.GetMax() * capsuleCollider.radius;

                    var a = transform.TransformPoint( rot * capsuleCollider.center.AddY( -capsuleCollider.height / 2f + radius ) );
                    var b = transform.TransformPoint( rot * capsuleCollider.center.AddY( +capsuleCollider.height / 2f - radius ) );

                    if( Physics.CapsuleCast( a, b, radius, direction, out hit, maxDistance, layerMask, triggerMode ) )
                    {
                        return true;
                    }
                    break;
                }

            default:
                throw new NotImplementedException( "Casting " + col.GetType() + " is not implemented yet." );
        }

        hit = new RaycastHit();
        return false;
    }

    public static int CastAllNonAlloc( this Collider col, RaycastHit[] results, Vector3 origin, Vector3 direction, float maxDistance, LayerMask layerMask, QueryTriggerInteraction triggerMode = QueryTriggerInteraction.Collide )
    {
        var transform = col.transform;

        switch( col )
        {
            case BoxCollider boxCollider:
                {
                    var halfSize = transform.lossyScale.Mul( boxCollider.size ) * 0.5f;
                    return Physics.BoxCastNonAlloc( origin, halfSize, direction, results, transform.rotation, maxDistance, layerMask, triggerMode );
                }

            case SphereCollider sphereCollider:
                {
                    var radius = transform.lossyScale.GetMax() * sphereCollider.radius;
                    return Physics.SphereCastNonAlloc( origin, radius, direction, results, maxDistance, layerMask, triggerMode );
                }

            case CapsuleCollider capsuleCollider:
                {
                    Quaternion rot = Quaternion.identity;

                    switch( capsuleCollider.direction )
                    {
                        case 0: rot = Quaternion.Euler( 0f, 0f, 90f ); break; // x
                        case 1: break; // y
                        case 2: rot = Quaternion.Euler( 90f, 0f, 0f ); break; // z
                    }

                    var radius = transform.lossyScale.GetMax() * capsuleCollider.radius;

                    var offsetA = transform.TransformPoint( rot * capsuleCollider.center.AddY( -capsuleCollider.height / 2f + radius ) ) - transform.position;
                    var offsetB = transform.TransformPoint( rot * capsuleCollider.center.AddY( +capsuleCollider.height / 2f - radius ) ) - transform.position;

                    return Physics.CapsuleCastNonAlloc( origin + offsetA, origin + offsetB, radius, direction, results, maxDistance, layerMask, triggerMode );
                }

            default:
                throw new NotImplementedException( "Casting " + col.GetType() + " is not implemented yet." );
        }
    }

    public static Vector3 Center( this Collider col )
    {
        switch( col )
        {
            case BoxCollider boxCollider:
                return boxCollider.center;

            case SphereCollider sphereCollider:
                return sphereCollider.center;

            case CapsuleCollider capsuleCollider:
                return capsuleCollider.center;

            default:
                throw new NotImplementedException( "Getting Center from  " + col.GetType() + " is not implemented yet." );
        }
    }
}
