using UnityEngine;
using UnityEngine.PlayerLoop;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour, IUpdate, IFixedUpdate
    {
        public float HorizontalDirection { get; private set; }

        [SerializeField]
        private GameObject character;

        [SerializeField]
        private CharacterController characterController;

        void IUpdate.Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                characterController._fireRequired = true;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.HorizontalDirection = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                this.HorizontalDirection = 1;
            }
            else
            {
                this.HorizontalDirection = 0;
            }
        }
        
        void IFixedUpdate.FixedUpdate()
        {
            this.character.GetComponent<MoveComponent>().MoveByRigidbodyVelocity(new Vector2(this.HorizontalDirection, 0) * Time.fixedDeltaTime);
        }
    }
}