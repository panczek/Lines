using Code.ECS;
using UnityEngine;

namespace Game.ECS
{
    public class ECSGameplayWorld : MonoBehaviour
    {
        private void Awake()
        {
            Worlds.CreateGameplayWorld();
        }
        
        private void OnDestroy()
        {
            if( Worlds.Gameplay != null )
                EndOfFrameInvoker.OnOnce.AddListener( Worlds.DestroyGameplayWorld );
        }
    }
}
