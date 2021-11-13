using Code.ECS;
using Code.Gameplay.Entities;
using Code.Gameplay.Factions;
using Code.Gameplay.LevelObjects.Buildings;
using Code.Gameplay.Pools;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Code.Gameplay.Buildings
{
    public class BaseBuilding : HybridEntity, IPoolable<BaseBuilding.SpawnContext>
    {
        [SerializeField] private BuildingSettings buildingSetting;
        
        protected override List<ComponentType> GetArchetypeTypes()
        {
            var archetypes = new List<ComponentType>();

            archetypes = new List<ComponentType>()
            {
                typeof( Translation ),
                typeof( LocalToWorld ),
                typeof( Rotation ),
                
                typeof( FactionCmp )

                
            };
            
            return archetypes;
        }

        protected override void SetComponents()
        {
            base.SetComponents();
            
            EntityMgr.SetComponentData( EntityId, new FactionCmp{ Faction = buildingSetting.Faction} );
        }

       

        public bool IsSpawned { get; set; }
        public void PoolMe()
        {
            throw new System.NotImplementedException();
        }

        public Event Recycle { get; set; }
        public void OnCreated()
        {
            throw new System.NotImplementedException();
        }

        public void OnPooled()
        {
            throw new System.NotImplementedException();
        }

        public void OnBeforeSpawned( SpawnContext context )
        {
            CreateEntity( Worlds.Gameplay );
        }

        public void OnAfterSpawned( SpawnContext context )
        {
            throw new System.NotImplementedException();
        }
        
        public struct SpawnContext
        {
            
        }
    }
}
