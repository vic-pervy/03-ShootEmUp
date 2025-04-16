using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class PlayingGameState : BaseGameState
    {
        
        private IUpdate[] gameLoopUpdateListeners;
        private IFixedUpdate[] gameLoopFixedUpdatesListeners;
        
        public override void EnterState(GameManager gameManager)
        {
            Time.timeScale = 1;
            gameManager.uiPresenter.CountDownText.gameObject.SetActive(false);
            gameManager.uiPresenter.PauseButton.gameObject.SetActive(true);
            gameManager.uiPresenter.PlayButton.gameObject.SetActive(false);
            // gameLoopUpdateListeners = Resources.FindObjectsOfTypeAll<MonoBehaviour>().Where(c => c is IUpdate)
            //     .Cast<IUpdate>().ToArray();
            // gameLoopFixedUpdatesListeners = Resources.FindObjectsOfTypeAll<MonoBehaviour>()
            //     .Where(c => c is IFixedUpdate).Cast<IFixedUpdate>().ToArray();
            //
            // foreach (var c in Resources.FindObjectsOfTypeAll<MonoBehaviour>().Where(c => c is IStartGame).Cast<IStartGame>())
            // {
            //    c.OnStart();
            // }
            
            gameLoopUpdateListeners = gameManager.gameEvents.Where(e=>e is IUpdate).Cast<IUpdate>().ToArray();
            gameLoopFixedUpdatesListeners = gameManager.gameEvents.Where(e=>e is IFixedUpdate).Cast<IFixedUpdate>().ToArray();

            foreach (var gameEvent in gameManager.gameEvents)
            {
                if (gameEvent is IStartGame startGame)
                {
                    startGame.OnStart();
                }
            }
        }

        public override void ExitState(GameManager gameManager)
        {
            // foreach (var c in Resources.FindObjectsOfTypeAll<MonoBehaviour>().Where(c => c is IPauseGame).Cast<IPauseGame>())
            // {
            //     c.OnPause();
            // }
            
            foreach (var gameEvent in gameManager.gameEvents)
            {
                if (gameEvent is IPauseGame pauseGame)
                {
                    pauseGame.OnPause();
                }
            }
        }

        public override void Update()
        {
            foreach (var c in gameLoopUpdateListeners)
            {
                if (c is MonoBehaviour monoBehaviour)
                {
                    if (!monoBehaviour || !monoBehaviour.enabled ||
                        !monoBehaviour.gameObject.activeInHierarchy) continue;
                }
                c.Update();
            }
        }

        public override void FixedUpdate()
        {
            foreach (var c in gameLoopFixedUpdatesListeners)
            {
                if (c is MonoBehaviour monoBehaviour)
                {
                    if (!monoBehaviour || !monoBehaviour.enabled ||
                        !monoBehaviour.gameObject.activeInHierarchy) continue;
                }
                c.FixedUpdate();
            }
        }
    }
}