using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace ShootEmUp.DI
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private EnemyMoveAgent enemyPrefab;

        [SerializeField] private Transform container;

        public override void InstallBindings()
        {
            Container.BindInstance(enemySpawner).AsCached();

            Container.BindInstance(container).WithId("enemyContainer");

            const int initialPoolSize = 10;
            Container.BindMemoryPool<EnemyMoveAgent, EnemySpawner.Pool>().WithInitialSize(initialPoolSize).FromComponentInNewPrefab(enemyPrefab);
        }
    }
}