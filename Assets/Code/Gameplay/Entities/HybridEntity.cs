using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Code.Gameplay.Entities
{
    public abstract class HybridEntity : MonoBehaviour
    {
        [SerializeField] private EcsBridge bridge;

        public World World => bridge.Ref()?.MyWorld;
        public EntityManager EntityMgr => World?.EntityManager ?? default;
        public Entity EntityId => ( bridge.Ref()?.Entity ) ?? Entity.Null;

        public EcsBridge Bridge => bridge;
        
        protected void CreateEntity( World world )
        {
            bridge.CreateEntity( GetArchetypeTypes(), world );
            SetComponents();
        }

        protected virtual void SetComponents() { }

        protected abstract List<ComponentType> GetArchetypeTypes();
    }
}
