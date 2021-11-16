using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Gameplay.Units
{
    public class UnitDefinition : SerializedScriptableObject 
    {
        [SerializeField] private string unitName;
        [SerializeField] private float hp;
        
    }
}
