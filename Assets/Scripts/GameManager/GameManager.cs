using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ShootEmUp
{
    public sealed class GameManager : ITickable, IFixedTickable, IInitializable
    {
        [Inject] public List<IGameEvent> gameEvents;
        [Inject] public UIPresenter uiPresenter;

        public void Initialize()
        {
            uiPresenter.PlayButton.onClick.AddListener(StartGame);
            uiPresenter.PauseButton.onClick.AddListener(PauseGame);
            ChangeGameState(new PauseGameState());
        }

        public void StartGame()
        {
            ChangeGameState(new CountDownState());
        }

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

        public void Tick()
        {
            if (currentGameState != null) currentGameState.Update();
        }

        public void FixedTick()
        {
            if (currentGameState != null) currentGameState.FixedUpdate();
        }
    }
}