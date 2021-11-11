using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.LevelObjects;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Controllers
{
    public class LevelController : MonoBehaviour
    {
        [Inject] private LevelObjectsController objCtrl;
        [Inject] private WorldGenerator worldGenerator;

        public void LevelInit()
        {
            var objs = FindObjectsOfType<MonoBehaviour>().OfType<ILevelObject>().ToList();
            objCtrl.OnLevelInit( objs );
            Debug.Log( "Level Init" );
            
            worldGenerator.Generate();
        }

        public void LevelStart()
        {
            objCtrl.OnLevelStart();
            Debug.Log( "Level Start" );
        }

        public void LevelStop()
        {
            objCtrl.OnLevelStop();
            Debug.Log( "Level Stop" );
        }

        // Start is called before the first frame update
        void Start()
        {
            LevelInit();
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}
