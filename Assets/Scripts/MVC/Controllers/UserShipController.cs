using System.Collections.Generic;
using Controls;
using Settings;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MVC
{
    public class UserShipController : BaseController
    {
        private UserModel _userModel;
        private ShipView _shipView;
        private MotionWithInertia _motionWithInertia;
        private MoveControl _moveControl;
        private List<BaseUnitView> _projectiles = new List<BaseUnitView>();
        private List<BaseUnitView> _projectilesPool = new List<BaseUnitView>();
        private bool _laserEnabled;
        private Camera _camera;
        private GameSettings _settings;
        private Transform _parent;
        private BaseUnitView _laserView;
        private GameManager _gameManager;

        public UserShipController(GameManager gameManager, UserModel userModel, ShipView shipView, Camera camera, GameSettings settings)
        {
            _userModel = userModel;
            _shipView = shipView;
            _camera = camera;
            _settings = settings;
            _gameManager = gameManager;
            _motionWithInertia = new MotionWithInertia(_camera);
            _moveControl = new MoveControl(_camera);
            _laserView = _shipView.LaserView;
            _laserView.OnEnemyTriggerEnter += OnLaserTriggerEnter;
            _shipView.OnEnemyTriggerEnter += OnEnemyTriggerEnter;
            _shipView.MoveButtonClicked += OnMoveButtonClicked;
            _shipView.MoveButtonUp += OnMoveButtonUp;
            _shipView.OnEnemyTriggerEnter += OnEnemyTriggerEnter;
            _shipView.ShootButtonClicked += OnShootButtonClicked;
            _shipView.LaserButtonClicked += OnLaserButtonClicked;
            _shipView.LaserButtonUp += OnLaserButtonUp;
            _gameManager.RestartEvent += OnRestart;
            _parent = Main.Instance.ProjectilesParent;
            _userModel.SetStartPosition(shipView.transform.position);
            _userModel.SetShipView(_shipView);
            for (var i = 0; i < 10; ++i)
            {
                var obj = Object.Instantiate(_settings.shipSettings.projectilePrefab, _parent);
                _projectilesPool.Add(obj);
                obj.gameObject.SetActive(false);
            }
        }

        public override void Dispose()
        {
            _laserView.OnEnemyTriggerEnter -= OnLaserTriggerEnter;
            _shipView.OnEnemyTriggerEnter -= OnEnemyTriggerEnter;
            _shipView.MoveButtonClicked -= OnMoveButtonClicked;
            _shipView.MoveButtonUp -= OnMoveButtonUp;
            _shipView.OnEnemyTriggerEnter -= OnEnemyTriggerEnter;
            _shipView.ShootButtonClicked -= OnShootButtonClicked;
            _shipView.LaserButtonClicked -= OnLaserButtonClicked;
            _shipView.LaserButtonUp -= OnLaserButtonUp;
            _gameManager.RestartEvent -= OnRestart;
        }

        public override void Update()
        {
            _motionWithInertia.TeleportOrDisappearEffect(_shipView);
            _userModel.SetShipPosAngle(_shipView.gameObject.transform.position,
                _shipView.gameObject.transform.localEulerAngles.z);
            
            for (var i = _projectiles.Count-1; i >= 0; --i)
            {
                _moveControl.Move(_projectiles[i].transform.up, _projectiles[i], 10);
                _moveControl.TeleportOrDisappearEffect(_projectiles[i], true, () =>
                {
                    _projectiles.Remove(_projectiles[i]);
                    _projectiles[i].gameObject.SetActive(false);
                });
            }
            if (_userModel.ShipDestroyed)
            {
                return;
            }
            if (!_laserEnabled)
            {
                _userModel.ChangeLaserAmount(true);
            }
            Rotate();
        }
        
        private void Rotate()
        {
            var mousePos = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            _shipView.transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - _shipView.transform.position);
        }
        
        protected void OnEnemyTriggerEnter(BaseUnitView view)
        { 
            _motionWithInertia.CurrentSpeedSetZero();
            if (_userModel.ShipDestroyed)
            {
                return;
            }
            _shipView.gameObject.SetActive(false);
            view.gameObject.SetActive(false);
            _userModel.DestroyShip();
        }
        
        private void OnMoveButtonClicked()
        {
            _motionWithInertia.InertiaMove(_shipView.transform.up, _shipView, _settings.shipSettings.speed);
            _userModel.ShipSpeedSet(_motionWithInertia.CurrenSpeed);
        }
        private void OnMoveButtonUp()
        {
            _motionWithInertia.MoveEnd(_shipView);
            _userModel.ShipSpeedSet(_motionWithInertia.CurrenSpeed);
        }
        private void OnShootButtonClicked(Transform pos)
        {
            if (_userModel.ShipDestroyed)
            {
                return;
            }
            var obj = ProjectileCreateOrEnable();
            obj.gameObject.SetActive(true);
            obj.OnEnemyTriggerEnter += ProjectileOnTriggerEnter;
            obj.transform.position = pos.position;
            obj.transform.rotation = pos.rotation;
            _projectiles.Add(obj);
        }
        
        private BaseUnitView ProjectileCreateOrEnable()
        {
            for (var i = _projectilesPool.Count - 1; i >= 0; --i)
            {
                if (!_projectilesPool[i].gameObject.activeInHierarchy)
                {
                    return _projectilesPool[i];
                }
            }
            var obj = Object.Instantiate(_settings.shipSettings.projectilePrefab, _parent);
            _projectilesPool.Add(obj);
            return obj;
        }
        
        private void OnLaserButtonClicked(GameObject laserObject)
        {
            if (_userModel.LaserAmount <= 5)
            {
                _laserEnabled = false;
                _userModel.SetLaserAmountZero();
                _laserView.gameObject.SetActive(false);
                return;
            }
            _laserEnabled = true;
            _laserView.gameObject.SetActive(true);
            _userModel.ChangeLaserAmount(false);
        }
        
        private void OnLaserButtonUp(GameObject laserObject)
        {
            _laserEnabled = false;
            _laserView.gameObject.SetActive(false);
        }

        private void ProjectileOnTriggerEnter(BaseUnitView view)
        {
            view.OnEnemyTriggerEnter -= ProjectileOnTriggerEnter;
            _userModel.AddScores();
            _projectiles.Remove(view);
            view.gameObject.SetActive(false);
        }
        private void OnLaserTriggerEnter(BaseUnitView unitView)
        {
            _userModel.AddScores();
        }
        
        private void OnRestart()
        {
            _userModel.RespawnShip();
            _userModel.ClearScores();
            _shipView.transform.position = _userModel.StartPosition;
            _shipView.gameObject.SetActive(true);
        }
    }
}