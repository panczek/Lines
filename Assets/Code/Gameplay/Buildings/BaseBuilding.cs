using Code.Gameplay.Entities;
using Code.Gameplay.LevelObjects.Buildings;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Code.Gameplay.Buildings
{
    public class BaseBuilding : HybridEntity
    {
        [SerializeField] private BuildingSettings buildingSetting;
        
        protected override List<ComponentType> GetArchetypeTypes()
        {
            var archetypes = new List<ComponentType>();

            return archetypes;
        }
    }
}
