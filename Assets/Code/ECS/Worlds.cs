using ModestTree;
using System.Collections.Generic;
using System.Text;
using Unity.Entities;
using UnityEngine;

namespace Code.ECS
{
    public static class Worlds
    {
        public static World Gameplay { get; private set; }
        public static EntityManager GameplayEntityMgr { get; private set; }

        private const string NameGameplay = "Gameplay World";

        [RuntimeInitializeOnLoadMethod( RuntimeInitializeLoadType.BeforeSceneLoad )]
        private static void OnPlay()
        {
            Gameplay = null;
            GameplayEntityMgr = default;
        }

        public static World Create( string name )
        {
            foreach( var w in World.All )
                if( w.Name == name )
                {
                    Debug.LogError( $"Worlds - World already exists: '{name}'" );
                    return w;
                }

            DefaultWorldInitialization.Initialize( name, false );

            foreach( var w in World.All )
                if( w.Name == name )
                    return w;

            return null;
        }

        public static void Destroy( World _world )
        {
            World toDestroy = null;

            foreach( var w in World.All )
                if( w.Name == _world.Name )
                {
                    toDestroy = w;
                    break;
                }

            if( toDestroy != null && toDestroy.IsCreated )
            {
                //EcsBridge.ClearArchetypeCache();
                toDestroy.Dispose();
                ScriptBehaviourUpdateOrder.UpdatePlayerLoop( null );
            }
            else
            {
                Debug.LogError( $"World not found: '{_world?.Name ?? "[null]"}'" );
            }
        }

        public static void CreateGameplayWorld()
        {
            Gameplay = Create( NameGameplay );
            GameplayEntityMgr = Gameplay.EntityManager;

            ValidateSystems( Gameplay );
        }

        public static void DestroyGameplayWorld()
        {
            Destroy( Gameplay );
            GameplayEntityMgr = default;
        }

        private static void ValidateSystems( World world )
        {
            var typeUpdateInGroup = typeof( UpdateInGroupAttribute );
            var nameUpdateInGroup = typeUpdateInGroup.Name;
            var typeSystemGroup = typeof( ComponentSystemGroup );
            var sb = new StringBuilder();
            bool any = false;

            var ignored = new HashSet<string>()
            {
                nameof( CompanionGameObjectUpdateTransformSystem ),
                "CompanionGameObjectUpdateSystem"
            };

            foreach( var sys in world.Systems )
                if( sys != null )
                {
                    var typeSys = sys.GetType();

                    if( ignored.Contains( typeSys.Name ) )
                        continue;

                    if( typeSystemGroup.IsAssignableFrom( typeSys ) || typeSys.HasAttribute( typeUpdateInGroup ) )
                        continue;

                    sb.AppendLine( typeSys.Name );
                    any = true;
                }

            if( any )
                Debug.LogWarning( $"For Coders: Some systems have no '{nameUpdateInGroup}' set:\n{sb.ToString()}" );
        }
    }
}
