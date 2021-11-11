using System;
using UnityEngine;

namespace Code.Gameplay.Player
{
    public class Selector : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Transform mainPos;
        [SerializeField] private Transform direction;
        [SerializeField] private float maxDistance;

        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material buildingMaterial;
        [SerializeField] private Material chunkMaterial;

        private void Update()
        {
            RaycastHit hit;
            var dir = Vector3.Normalize( direction.position - mainPos.position );
            if( Physics.Raycast( mainPos.position, dir, out hit, maxDistance, Shared.Layers.GetMask( Shared.Layers.Int.Buildings ), QueryTriggerInteraction.Ignore ) )
            {
                UpdateLineRenderer( buildingMaterial, hit.point );
            }
            else if( Physics.Raycast( mainPos.position, dir, out hit, maxDistance, Shared.Layers.GetMask( Shared.Layers.Int.Chunks ), QueryTriggerInteraction.Ignore ) )
            {
                UpdateLineRenderer( chunkMaterial, hit.point );
            }
            else
            {
                UpdateLineRenderer( defaultMaterial, dir * maxDistance );
            }
        }

        private void UpdateLineRenderer( Material material, Vector3 endPoint )
        {
            lineRenderer.material = material;

            lineRenderer.SetPosition( 0, mainPos.position );
            lineRenderer.SetPosition( 1, endPoint );
        }
    }
}
