using System;
using System.Collections.Generic;
using Settings;
using UnityEngine;

namespace MVC
{
    public class BattlefieldModel : BaseModel
    {
        public event Action GameOverEvent;
        public event Action RestartEvent;
        private GameSettings _gameSettings;
        private List<BaseController> _controllers = new List<BaseController>();
        private Camera _camera;
        private EnemiesModel _enemiesModel;
        private UserModel _userModel;
        private GameEndUIController _gameEndUIController;
        private GameEndUIView _gameEndUIView;
        private Transform _uiParent;
        private List<IDisposable> _disposables = new List<IDisposable>();
        private bool _isGameOver;
        public List<BaseController> Controllers => _controllers;
        public GameSettings GameSettings => _gameSettings;
        public Camera Camera => _camera;
        public EnemiesModel EnemiesModel => _enemiesModel;
        public UserModel UserModel => _userModel;
        public bool IsGameOver => _isGameOver;
        public BattlefieldModel(Camera camera, GameSettings gameSettings, MonoBehaviour monoBehaviour, GameEndUIView gameEndUIView)
        {
            _camera = camera;
            _gameSettings = gameSettings;
            
            _userModel = new UserModel(gameSettings, this);
            _userModel.ShipDestroyedEvent += GameOver;
            
            _enemiesModel = new EnemiesModel(gameSettings.asteroidSettings, gameSettings.ufoSettings,gameSettings.smallAsteroidSettings, 
                monoBehaviour, this);
            _gameEndUIView = gameEndUIView;
            
            _disposables.Add(_userModel);
            _disposables.Add(_enemiesModel);
        }
        public void CreateControllers()
        {
            var shipController = new ShipController(this);
            _controllers.Add(shipController);
            
            var ufosController = new UfosController(this);
            _controllers.Add(ufosController);

            var asteroidsController = new AsteroidsController(this);
            _controllers.Add(asteroidsController);

            _gameEndUIController = new GameEndUIController(this, _gameEndUIView);
            _controllers.Add(_gameEndUIController);
            _disposables.AddRange(_controllers);
        }
        public void Restart()
        {
            _isGameOver = false;
            RestartEvent?.Invoke();
        }
        private void GameOver()
        {
            GameOverEvent?.Invoke();
            _isGameOver = true;
        }

        public override void Dispose()
        {
            _userModel.ShipDestroyedEvent -= GameOver;
            for(int i = 0, len = _disposables.Count; i < len; ++i)
            {
                _disposables[i].Dispose();
            }
            base.Dispose();
        }
    }
}