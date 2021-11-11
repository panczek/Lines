using Code.Gameplay.LevelObjects;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Controllers
{
    public class WorldGenerator : MonoBehaviour
    {
        [SerializeField] private float linesDistance = 10f;
        [SerializeField] private int lineLenght = 25;

        [SerializeField] private GameObject mainChunk;
        [SerializeField] private float chunkXSize = 2f;
        [SerializeField] private float chunkZSize = 2f;

        [ShowInInspector, ReadOnly] private List<Line> gameLines;
        
        public void Generate()
        {
            gameLines = new List<Line>();

            var parent = Instantiate( new GameObject(), this.transform );
            parent.name = "Chunks";
            var zOffset = chunkZSize / 2f + linesDistance;
                
            int lineNo = 1;
            for( float z = -zOffset; z < zOffset * 2f; z += zOffset )
            {
                var newLine = new Line
                {
                    Index = lineNo,
                    Chunks = new List<Chunk>()
                };
                for( int x = 0; x < lineLenght; x++ )
                {
                    var chunk = Instantiate( mainChunk, new Vector3( x * chunkXSize, 0f, z ), Quaternion.identity, parent.transform );
                    chunk.name = $"Chunk {lineNo} / {x}";
                    var chunkRenderer = chunk.GetComponent<MeshRenderer>();
                    chunkRenderer.material.color = Color.Lerp( Color.green, Color.red, (float)x / (float)lineLenght );

                    if( chunk.TryGetComponent( out Chunk chunkCmp ) )
                    {
                        chunkCmp.CreateChunk( this, newLine );
                        newLine.Chunks.Add( chunkCmp );
                    }
                }
                    
                gameLines.Add( newLine );

                lineNo++;
            }
        }
    }
}
