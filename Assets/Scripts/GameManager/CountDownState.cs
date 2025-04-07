using UnityEngine;

namespace ShootEmUp
{
    public class CountDownState : BaseGameState
    {
        private GameManager gameManager;
        private float countDownTime;

        public override void EnterState(GameManager gameManager)
        {
            Time.timeScale = 0;
            this.gameManager = gameManager;
            countDownTime = 3;
            gameManager.uiPresenter.CountDownText.gameObject.SetActive(true);
            gameManager.uiPresenter.PauseButton.gameObject.SetActive(false);
            gameManager.uiPresenter.PlayButton.gameObject.SetActive(false);
            Update();
        }

        public override void ExitState(GameManager gameManager)
        {
        }

        public override void Update()
        {
            countDownTime -= Time.unscaledTime;
            if (countDownTime <= 0)
            {
                countDownTime = 0;
                gameManager.ChangeGameState(new PlayingGameState());
            }

            gameManager.uiPresenter.CountDownText.text = (Mathf.FloorToInt(countDownTime) + 1).ToString();
        }

        public override void FixedUpdate()
        {
        }
    }
}