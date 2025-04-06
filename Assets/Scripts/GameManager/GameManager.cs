using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
        public Button PlayButton, PauseButton;
        public TMP_Text CountDownText;
        
        private void Awake()
        {
            PlayButton.onClick.AddListener(StartGame);
            PauseButton.onClick.AddListener(PauseGame);
            ChangeGameState(new PauseGameState());
        }

        [ContextMenu("Start Game")]
        public void StartGame()
        {
            ChangeGameState(new CountDownState());
        }

        [ContextMenu("Pause Game")]
        public void PauseGame()
        {
           ChangeGameState(new PauseGameState());
        }

        public void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
        
        
        private BaseGameState currentGameState;
        public void ChangeGameState(BaseGameState newState)
        {
            if (currentGameState != null) currentGameState.ExitState(this);
            currentGameState = newState;
            currentGameState.EnterState(this);
        }

        void Update()
        {
            if (currentGameState != null) currentGameState.Update();
        }

        void FixedUpdate()
        {
            if (currentGameState != null) currentGameState.FixedUpdate();
        }
    }

   

  
}