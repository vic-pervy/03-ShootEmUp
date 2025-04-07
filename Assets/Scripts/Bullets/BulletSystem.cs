using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour, IFixedUpdate
    {
        public class Pool : MemoryPool<Bullet>
        {
            [Inject(Id = "bulletContainer")] private Transform container;
            [Inject(Id = "worldTransform")] Transform worldTransform;
            public readonly HashSet<Bullet> ActiveBullets = new();

            protected override void OnCreated(Bullet item)
            {
                item.transform.SetParent(this.container);
                base.OnCreated(item);
            }

            protected override void OnSpawned(Bullet item)
            {
                item.transform.SetParent(this.worldTransform);
                ActiveBullets.Add(item);
                base.OnSpawned(item);
            }

            protected override void OnDespawned(Bullet item)
            {
                item.transform.SetParent(this.container);
                ActiveBullets.Remove(item);
                base.OnDespawned(item);
            }
        }

        //[SerializeField] private Transform container;

        //[SerializeField] private Bullet prefab;
        //[SerializeField] private Transform worldTransform;
        [SerializeField] private LevelBounds levelBounds;

        [Inject] private BulletSystem.Pool pool;

        //private readonly Queue<Bullet> m_bulletPool = new();
        //private readonly HashSet<Bullet> m_activeBullets = new();
        private readonly List<Bullet> cache = new();

        // private void Awake()
        // {
        //     for (var i = 0; i < this.initialCount; i++)
        //     {
        //         var bullet = Instantiate(this.prefab, this.container);
        //         this.m_bulletPool.Enqueue(bullet);
        //     }
        // }

        void IFixedUpdate.FixedUpdate()
        {
            this.cache.Clear();
            this.cache.AddRange(pool.ActiveBullets);

            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var bullet = this.cache[i];
                if (!this.levelBounds.InBounds(bullet.transform.position))
                {
                    this.RemoveBullet(bullet);
                }
            }
        }

        public void FlyBulletByArgs(Args args)
        {
            // if (this.m_bulletPool.TryDequeue(out var bullet))
            // {
            //     bullet.transform.SetParent(this.worldTransform);
            // }
            // else
            // {
            //     bullet = Instantiate(this.prefab, this.worldTransform);
            // }
            var bullet = pool.Spawn();

            bullet.SetPosition(args.Position);
            bullet.SetColor(args.Color);
            bullet.SetPhysicsLayer(args.PhysicsLayer);
            bullet.damage = args.Damage;
            bullet.isPlayer = args.IsPlayer;
            bullet.SetVelocity(args.Velocity);

            bullet.OnCollisionEntered += this.OnBulletCollision;
        }

        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletUtils.DealDamage(bullet, collision.gameObject);
            this.RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            bullet.OnCollisionEntered -= this.OnBulletCollision;
            //bullet.transform.SetParent(this.container);
            //this.m_bulletPool.Enqueue(bullet);
            pool.Despawn(bullet);
        }

        public struct Args
        {
            public Vector2 Position;
            public Vector2 Velocity;
            public Color Color;
            public int PhysicsLayer;
            public int Damage;
            public bool IsPlayer;
        }
    }
}