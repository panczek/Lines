using UnityEngine;

namespace Code.Gameplay.LevelObjects
{
    public interface ILevelObject
    {
        Behaviour Cmp { get; }
        void LevelInit();
        void LevelUnloading();
        void LevelStopping();
        void LevelStarting();
    }
}