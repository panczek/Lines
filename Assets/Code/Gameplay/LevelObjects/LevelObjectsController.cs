using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.LevelObjects
{
    public class LevelObjectsController
    {
        private List<ILevelObject> levelObjs;

        public void OnLevelInit( List<ILevelObject> objs )
        {
            levelObjs = objs;

            foreach( var levelObject in levelObjs )
                levelObject.LevelInit();
        }

        public void OnLevelStart()
        {
            foreach( var levelObject in levelObjs )
                levelObject.LevelStarting();
        }

        public void OnLevelStop()
        {
            foreach( var levelObject in levelObjs )
                levelObject.LevelStopping();
        }

        public void OnLevelUnload()
        {
            foreach( var levelObject in levelObjs )
                levelObject.LevelUnloading();
        }
    }
}
