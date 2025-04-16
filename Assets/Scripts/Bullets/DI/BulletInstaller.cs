using System.Linq;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class BulletInstaller : MonoInstaller
    {
        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private Bullet bulletPrefab;

        public override void InstallBindings()
        {
            Container.BindInstance(bulletConfig).AsCached();
            Container.BindInterfacesAndSelfTo<BulletSystem>().AsSingle().NonLazy();

            const int initialPoolSize = 50;
            Container.BindMemoryPool<Bullet, BulletSystem.Pool>().WithInitialSize(initialPoolSize).FromComponentInNewPrefab(bulletPrefab);
        }
    }
}