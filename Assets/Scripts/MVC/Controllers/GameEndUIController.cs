using UnityEngine;

namespace MVC
{
    public class GameEndUIController: BaseController
    {
        /*private BattlefieldModel _battlefieldModel;*/
        private GameEndUIView _gameEndUIView;
        private Transform _patent;
        /*private UserModel _userModel;*/
        private UserModel _userModel;
        private GameManager _gameManager;
        
        public GameEndUIController(/*BattlefieldModel battlefieldModel*/GameManager gameManager, GameEndUIView view, Transform guiParent, UserModel userModel)
        {
            _patent = guiParent;
            _userModel = userModel;
            _gameManager = gameManager;
            /*_battlefieldModel = battlefieldModel;*/
            _gameEndUIView = view;
            gameManager.GameOverEvent += CreateWindow;
            /*_battlefieldModel.GameOverEvent += CreateWindow;*/
        }

        private void CreateWindow()
        {
            var obj = Object.Instantiate(_gameEndUIView, _patent, false);
            obj.SetScoreText(/*_userModel.Score*/ _userModel.Scores);
            obj.ButtonClicked += OnRestartButtonClick;
        }

        private void OnRestartButtonClick(GameEndUIView view)
        {
            view.ButtonClicked -= OnRestartButtonClick;
            Object.Destroy(view.gameObject);
            /*_battlefieldModel.Restart();*/
            _gameManager.RestartGame();
        }
    }
}