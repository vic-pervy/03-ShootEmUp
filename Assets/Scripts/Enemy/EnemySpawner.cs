using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemySpawner
    {
        public class Pool : MemoryPool<EnemyMoveAgent>
        {
            [Inject] private UpdateSystem updateSystem;
            [Inject] WorldData data;
            public readonly HashSet<EnemyMoveAgent> Active = new();
        
            protected override void OnCreated(EnemyMoveAgent item)
            {
                item.transform.SetParent(this.data.PoolContainer);
                base.OnCreated(item);
            }
        
            protected override void OnSpawned(EnemyMoveAgent item)
            {
                item.transform.SetParent(this.data.WorldContainer);
                Active.Add(item);
                foreach (var gameEvent in item.GetComponents(typeof(IGameEvent)))
                {
                    updateSystem.SubscribeObject((IGameEvent)gameEvent);
                }
                base.OnSpawned(item);
            }
        
            protected override void OnDespawned(EnemyMoveAgent item)
            {
                item.transform.SetParent(this.data.PoolContainer);
                Active.Remove(item);
                foreach (var gameEvent in item.GetComponents(typeof(IGameEvent)))
                {
                    updateSystem.UnsubscribeObject((IGameEvent)gameEvent);
                }
                base.OnDespawned(item);
            }
        }

        [Inject] private WorldData data;
        [Inject] private GameObject character;
        [Inject] private EnemySpawner.Pool pool;

        //private readonly Queue<GameObject> enemyPool = new();

        /*private void Awake()
        {
            for (var i = 0; i < 7; i++)
            {
                var enemy = Instantiate(this.prefab, this.container);
                this.enemyPool.Enqueue(enemy);
            }
        }*/

        public EnemyMoveAgent SpawnEnemy()
        {
            if (pool.Active.Count >= 7) return null;

            // if (!this.enemyPool.TryDequeue(out var enemy))
            // {
            //     return null;
            // }
            var enemy = pool.Spawn();

            //enemy.transform.SetParent(this.worldTransform);

            var spawnPosition = data.EnemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;

            var attackPosition = data.EnemyPositions.RandomAttackPosition();
            enemy.gameObject.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);

            enemy.gameObject.GetComponent<EnemyAttackAgent>().SetTarget(this.character);
            return enemy;
        }

        public void UnspawnEnemy(EnemyMoveAgent enemy)
        {
            //enemy.transform.SetParent(this.container);
            //this.enemyPool.Enqueue(enemy);
            pool.Despawn(enemy);
        }
    }
}