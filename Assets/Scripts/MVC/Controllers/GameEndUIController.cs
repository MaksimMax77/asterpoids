using UnityEngine;

namespace MVC
{
    public class GameEndUIController: BaseController
    {
        private BattlefieldModel _battlefieldModel;
        private GameEndUIView _gameEndUIView;
        private Transform _patent;
        private UserModel _userModel;
        
        public GameEndUIController(BattlefieldModel battlefieldModel, GameEndUIView view)
        {
            _patent = Main.Instance.UiParent;
            _userModel = Main.Instance.UserModel;
            _battlefieldModel = battlefieldModel;
            _gameEndUIView = view;
            _battlefieldModel.GameOverEvent += CreateWindow;
        }

        private void CreateWindow()
        {
            var obj = Object.Instantiate(_gameEndUIView, _patent, false);
            obj.SetScoreText(_userModel.Score);
            obj.ButtonClicked += OnRestartButtonClick;
        }

        private void OnRestartButtonClick(GameEndUIView view)
        {
            view.ButtonClicked -= OnRestartButtonClick;
            Object.Destroy(view.gameObject);
            _battlefieldModel.Restart();
        }
    }
}