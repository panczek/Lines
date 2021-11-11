using Code.Gameplay.LevelObjects;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Controllers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private LevelController levelCtrl;
        [SerializeField] private WorldGenerator worldGenerator;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelObjectsController>().AsSingle();

            Container.BindInstance( levelCtrl ).AsSingle();
            Container.BindInstance( worldGenerator ).AsSingle();
        }
    }
}
