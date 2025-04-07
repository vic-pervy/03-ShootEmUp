using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class CharacterController : IFixedTickable, IInitializable, IDisposable
    {
        [Inject] private GameObject character; 
        [Inject] private GameManager gameManager;
        [Inject] private BulletSystem _bulletSystem;
        [Inject] private BulletConfig _bulletConfig;
        
        public bool FireRequired;
        public float HorizontalDirection;

        
        public void Initialize()
        {
            this.character.GetComponent<HitPointsComponent>().hpEmpty += this.OnCharacterDeath;
        }

        public void Dispose()
        {
            if (!this.character) return;
            this.character.GetComponent<HitPointsComponent>().hpEmpty -= this.OnCharacterDeath;
        }
       
        private void OnCharacterDeath(GameObject _) => this.gameManager.FinishGame();

        public void FixedTick()
        {
            if (this.FireRequired)
            {
                this.OnFlyBullet();
                this.FireRequired = false;
            }
            
            this.character.GetComponent<MoveComponent>().MoveByRigidbodyVelocity(new Vector2(this.HorizontalDirection, 0) * Time.fixedDeltaTime);
        }

        private void OnFlyBullet()
        {
            var weapon = this.character.GetComponent<WeaponComponent>();
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                IsPlayer = true,
                PhysicsLayer = (int) this._bulletConfig.physicsLayer,
                Color = this._bulletConfig.color,
                Damage = this._bulletConfig.damage,
                Position = weapon.Position,
                Velocity = weapon.Rotation * Vector3.up * this._bulletConfig.speed
            });
        }


    }
}