using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp
{
    public class PauseGameState: BaseGameState
    {
        public override void EnterState(GameManager gameManager)
        {
            Time.timeScale = 0;
            gameManager.uiPresenter.CountDownText.gameObject.SetActive(false);
            gameManager.uiPresenter.PauseButton.gameObject.SetActive(false);
            gameManager.uiPresenter.PlayButton.gameObject.SetActive(true);
        }

        public override void ExitState(GameManager gameManager)
        {
        }

        public override void Update()
        {
        }

        public override void FixedUpdate()
        {
        }
    }
}