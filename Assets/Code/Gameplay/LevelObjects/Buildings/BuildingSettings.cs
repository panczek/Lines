using Code.Gameplay.Factions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Gameplay.LevelObjects.Buildings
{
    public class BuildingSettings : SerializedScriptableObject 
    {
        public Texture2D Icon;
        public EFaction Faction;
    }
}
