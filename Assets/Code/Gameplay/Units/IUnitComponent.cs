using System.Collections.Generic;
using Unity.Entities;

namespace Code.Gameplay.Units
{
    public interface IUnitComponent
    {
        void ApplyArchetype( List<ComponentType> types, UnityEngine.Object context );

        void SetComponents( EntityManager mgr, Entity e, UnityEngine.Object context );
    }
}
