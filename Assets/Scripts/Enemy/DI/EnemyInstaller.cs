using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace ShootEmUp.DI
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private EnemyMoveAgent enemyPrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EnemyManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle().NonLazy();

            const int initialPoolSize = 10;
            Container.BindMemoryPool<EnemyMoveAgent, EnemySpawner.Pool>().WithInitialSize(initialPoolSize).FromComponentInNewPrefab(enemyPrefab);
        }
    }
}