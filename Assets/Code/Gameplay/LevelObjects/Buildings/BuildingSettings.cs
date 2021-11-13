using Code.Gameplay.Factions;
using UnityEditor;
using UnityEngine;

namespace Code.Gameplay.LevelObjects.Buildings
{
    [CreateAssetMenu(fileName = "Building Settings", menuName = "ScriptableObjects/Buildings/Settings", order = 1)]
    public class BuildingSettings : ScriptableObject
    {
        public Texture2D Icon;
        public EFaction Faction;
    }
}
