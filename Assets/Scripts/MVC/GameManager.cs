using System;
using System.Collections.Generic;
using MVC;
using Settings;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameManager: IDisposable
{
    public event Action GameOverEvent;
    public event Action RestartEvent;
    private GameSettings _gameSettings;
    private List<BaseController> _controllers = new List<BaseController>();
    private Camera _camera; 
    
    private UserModel _userModel;
    private AsteroidsModel _asteroidsModel;
    private UfoModel _ufoModel;
    
    private ShipInfoUiController _shipInfoUiController;
    private Transform _unitsParent;
    private ShipInfoUiView _shipInfoUiView;
    private GameEndUIView _gameEndUIView;
    private UserShipController _shipController;
    private GameEndUIController _gameEndUIController;
    private AsteroidsController _asteroidsController;
    private UfoController _ufoController;

    public UserModel UserModel => _userModel;
    public GameManager(Camera camera, GameSettings gameSettings)
    {
        _camera = camera;
        _gameSettings = gameSettings;
    }

    public void SetGuiViews(ShipInfoUiView shipInfoUiView, GameEndUIView gameEndUIView)
    {
        _shipInfoUiView = shipInfoUiView;
        _gameEndUIView = gameEndUIView;
    }
    public void CreateObjects(Transform unitsParent, Transform guiParent, MonoBehaviour monoBehaviour)
    {
        _userModel = new UserModel();
        _userModel.ShipDestroyedEvent += GameOver;
        _asteroidsModel = new AsteroidsModel(_gameSettings, _camera, _unitsParent);
        _ufoModel = new UfoModel(_gameSettings, _camera, _unitsParent);
        

        _shipController = new UserShipController(this, _userModel, 
            (ShipView)Object.Instantiate(_gameSettings.shipSettings.unitView, unitsParent), _camera, _gameSettings);
        
        _shipInfoUiController = new ShipInfoUiController(_userModel, _shipInfoUiView, guiParent);
        _gameEndUIController = new GameEndUIController(this, _gameEndUIView, guiParent, _userModel);
        _asteroidsController = new AsteroidsController(_asteroidsModel, monoBehaviour,this);
        _ufoController = new UfoController(_ufoModel, monoBehaviour, this);
            
        _controllers.Add(_shipController);
        _controllers.Add(_shipInfoUiController);
        _controllers.Add(_gameEndUIController);
        _controllers.Add(_asteroidsController);
        _controllers.Add(_ufoController);
    }

    public void RestartGame()
    {
        RestartEvent?.Invoke();
    }

    private void GameOver()
    {
        GameOverEvent?.Invoke();
    }

    public void UpdateControllers()
    {
        for (int i = 0, len = _controllers.Count; i < len; ++i)
        {
            _controllers[i].Update();
        }
    }

    public void Dispose()
    {
        _userModel.ShipDestroyedEvent -= GameOverEvent;
        _userModel?.Dispose();
        _shipInfoUiController?.Dispose();
        _shipInfoUiView?.Dispose();
        _gameEndUIView?.Dispose();
        _shipController?.Dispose();
        _gameEndUIController?.Dispose();
    }
}
