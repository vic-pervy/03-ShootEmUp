using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour, IUpdate
    {
        [Inject] private EnemySpawner enemySpawner;
        [Inject] private BulletSystem bulletSystem;

        //private readonly HashSet<GameObject> m_activeEnemies = new();

        /*private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                var enemy = this._enemyPool.SpawnEnemy();
                if (enemy != null)
                {
                    if (this.m_activeEnemies.Add(enemy))
                    {
                        enemy.GetComponent<HitPointsComponent>().hpEmpty += this.OnDestroyed;
                        enemy.GetComponent<EnemyAttackAgent>().OnFire += this.OnFire;
                    }
                }
            }
        }*/

        private float time = 1;

        void IUpdate.Update()
        {
            time -= Time.deltaTime;
            if (time > 0) return;
            time = 1;
            var enemy = this.enemySpawner.SpawnEnemy();
            
            if (!enemy) return;
            
            enemy.gameObject.GetComponent<HitPointsComponent>().hpEmpty += this.OnDestroyed;
            enemy.gameObject.GetComponent<EnemyAttackAgent>().OnFire += this.OnFire;
        }

        private void OnDestroyed(GameObject enemy)
        {
            enemy.GetComponent<HitPointsComponent>().hpEmpty -= this.OnDestroyed;
            enemy.GetComponent<EnemyAttackAgent>().OnFire -= this.OnFire;

            enemySpawner.UnspawnEnemy(enemy.GetComponent<EnemyMoveAgent>());
        }

        private void OnFire(GameObject enemy, Vector2 position, Vector2 direction)
        {
            bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                IsPlayer = false,
                PhysicsLayer = (int)PhysicsLayer.ENEMY_BULLET,
                Color = Color.red,
                Damage = 1,
                Position = position,
                Velocity = direction * 2.0f
            });
        }
    }
}