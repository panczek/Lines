using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Code.Gameplay.Entities
{
    public class EcsBridge : MonoBehaviour
    {
        private Entity entityId;
        
        public World MyWorld { get; private set; }
        public EntityManager EntityManager => MyWorld?.EntityManager ?? default;
        public Entity Entity => entityId;
        
        public void CreateEntity( List<ComponentType> archetypeTypes, World world )
        {
            MyWorld = world;

            if( MyWorld == null )
            {
                Debug.LogError( "World == null", gameObject );
                return;
            }
            
            var archetype = EntityManager.CreateArchetype( archetypeTypes.ToArray() );
            var entity = EntityManager.CreateEntity( archetype );
            
            #if UNITY_EDITOR
            if( Application.isPlaying )
                EntityManager.SetName( entity, gameObject.name );
            #endif
        }
        
    }
}
