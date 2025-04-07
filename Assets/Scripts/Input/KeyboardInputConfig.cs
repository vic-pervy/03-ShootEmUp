using System;
using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu]
    public class KeyboardInputConfig : ScriptableObject, IInputConfig
    {
        [SerializeField]
        KeyboardInputKeyCodes _keyCodes = new KeyboardInputKeyCodes();

        public bool FireButtonDown => Input.GetKeyDown(_keyCodes.FireButton);
        public bool LeftButton => Input.GetKey(_keyCodes.LeftButton);
        public bool RightButton  => Input.GetKey(_keyCodes.RightButton);
    }

    [Serializable]
    struct KeyboardInputKeyCodes
    {
        public KeyCode FireButton;
        public KeyCode LeftButton;
        public KeyCode RightButton;
    }
}