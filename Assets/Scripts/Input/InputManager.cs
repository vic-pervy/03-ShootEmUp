using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Zenject;

namespace ShootEmUp
{
    public sealed class InputManager : ITickable
    {
        [Inject]
        IInputConfig inputConfig;
        
        [Inject]
        private GameObject character;

        [Inject]
        private CharacterController characterController;

        void ITickable.Tick()
        {
            if (inputConfig.FireButtonDown)
            {
                characterController.FireRequired = true;
            }

            if (inputConfig.LeftButton)
            {
                characterController.HorizontalDirection = -1;
            }
            else if (inputConfig.RightButton)
            {
                characterController.HorizontalDirection = 1;
            }
            else
            {
                characterController.HorizontalDirection = 0;
            }
        }

        
    }
}