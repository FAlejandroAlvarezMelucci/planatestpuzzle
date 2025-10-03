using PuzzleTest.View;

namespace PuzzleTest.Controller
{
    public class GameOverState : IGameState
    {
        private readonly UIManager _uiManager;

        public GameOverState(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        public void Enter()
        {
            _uiManager.ShowGameOverView(showView: true);
        }

        public void Exit()
        {
            _uiManager.ShowGameOverView(showView: false);
        }
    }
}