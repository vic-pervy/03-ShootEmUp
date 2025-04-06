namespace ShootEmUp
{
    public abstract class BaseGameState
    {
        public abstract void EnterState(GameManager gameManager);
        public abstract void ExitState(GameManager gameManager);
        public abstract void Update();
        public abstract void FixedUpdate();
    }
}