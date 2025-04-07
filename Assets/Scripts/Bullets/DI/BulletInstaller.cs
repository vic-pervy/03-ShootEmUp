using System.Linq;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class BulletInstaller : MonoInstaller
    {
        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private BulletSystem bulletSystem;
        [SerializeField] private Bullet bulletPrefab;

        [SerializeField] private Transform container;

        public override void InstallBindings()
        {
            Container.BindInstance(bulletConfig).AsCached();
            Container.BindInstance(bulletSystem).AsCached();

            Container.BindInstance(container).WithId("bulletContainer");

            const int initialPoolSize = 50;
            Container.BindMemoryPool<Bullet, BulletSystem.Pool>().WithInitialSize(initialPoolSize).FromComponentInNewPrefab(bulletPrefab);
        }
    }
}