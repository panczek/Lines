using UnityEngine;

namespace Code.Gameplay.Pools
{
    public interface IPoolable
    {
        bool IsSpawned { get; set; }

        void PoolMe();
    }

    public interface IPoolable<TContext> : IPoolable
        where TContext : struct
    {
        Event Recycle { get; set; }

        void OnCreated();
        void OnPooled();
        void OnBeforeSpawned( TContext context );
        void OnAfterSpawned( TContext context );
    }
}
