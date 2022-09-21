
using System;
using MVC;
using Settings;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] 
    private Camera _camera;
    [SerializeField] private Transform _uiParent;
    [SerializeField] private Transform _unitsParent;
    [SerializeField] private Transform _projectilesParent;
    [SerializeField] private GameSettings _settings;
    [SerializeField] private ShipInfoUiView _shipInfoUiView;
    [SerializeField] private GameEndUIView _gameEndUIView;
    
    private BattlefieldModel _battlefieldModel;
    private EnemiesModel _enemiesModel;
    private UserModel _userModel;
    private ShipInfoUiController _shipInfoUiController;
    public BattlefieldModel BattlefieldModel => _battlefieldModel;
    public EnemiesModel EnemiesModel => _enemiesModel;
    public UserModel UserModel => _userModel;
    public Transform UiParent => _uiParent;
    public Transform UnitsParent => _unitsParent;
    public Transform ProjectilesParent => _projectilesParent;
    public static Main Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        _battlefieldModel = new BattlefieldModel(_camera, _settings, this, _gameEndUIView);
        _enemiesModel = _battlefieldModel.EnemiesModel;
        _userModel = _battlefieldModel.UserModel;
        _battlefieldModel.CreateControllers();

        _shipInfoUiController = new ShipInfoUiController(_shipInfoUiView, _uiParent);
    }

    private void Update()
    {
        for (int i = 0, len = _battlefieldModel.Controllers.Count; i < len; ++i)
        {
            _battlefieldModel.Controllers[i].Update();
        }
        _shipInfoUiController.Update();
    }

    private void OnDestroy()
    {
        _battlefieldModel.Dispose();
    }
}
