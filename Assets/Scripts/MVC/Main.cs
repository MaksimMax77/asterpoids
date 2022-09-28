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
    
    private ShipInfoUiController _shipInfoUiController;
    private GameManager _gameManager;
    public Transform UiParent => _uiParent;
    public Transform UnitsParent => _unitsParent;
    public Transform ProjectilesParent => _projectilesParent;
    public GameManager GameManager => _gameManager;
    public static Main Instance { get; private set; }

    private void Start()
    {
        Instance = this;
        _gameManager = new GameManager(_camera, _settings);
        _gameManager.SetGuiViews(_shipInfoUiView, _gameEndUIView);
        _gameManager.CreateObjects(_unitsParent, _uiParent, this);
    }

    private void Update()
    {
        _gameManager.UpdateControllers();
    }

    private void OnDestroy()
    {
        /*_battlefieldModel.Dispose();*/
    }
}
