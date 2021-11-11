using Code.Gameplay.Controllers;
using UnityEngine;

namespace Code.Gameplay.LevelObjects
{
    public class Chunk : MonoBehaviour
    {
        private Line myLine;
        private WorldGenerator owner;
        private bool isFree;

        public void CreateChunk( WorldGenerator generator, Line line )
        {
            myLine = line;
            owner = generator;
            isFree = false;
        }
    }
}
