using System.Collections.Generic;
using Settings;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MVC
{
    public class ShipController: UnitsController
    {
        private ShipView _shipView;
        private Vector3 _direction;
        private BattlefieldModel _battlefieldModel;
        private UserModel _userModel;
        private bool _laserEnabled;
        private BaseUnitView _laserView;
        private List<BaseUnitView> _projectiles = new List<BaseUnitView>();
        private List<BaseUnitView> _projectilesPool = new List<BaseUnitView>();
        private Transform _parent;
        
        private GameSettings.ShipSettings _shipSettings;
        public ShipController(BattlefieldModel battlefieldModel) : base(battlefieldModel)
        {
            _userModel = Main.Instance.UserModel;
            _shipView = (ShipView) _userModel.ShipView;
            _laserView = _shipView.LaserView;
            _laserView.OnEnemyTriggerEnter += OnLaserTriggerEnter;
            _battlefieldModel = battlefieldModel;
            _shipSettings = battlefieldModel.GameSettings.shipSettings;
            _shipView.OnEnemyTriggerEnter += OnEnemyTriggerEnter;
            _shipView.MoveButtonClicked += OnMoveButtonClicked;
            _shipView.MoveButtonUp += OnMoveButtonUp;
            _shipView.OnEnemyTriggerEnter += OnEnemyTriggerEnter;
            _shipView.ShootButtonClicked += OnShootButtonClicked;
            _shipView.LaserButtonClicked += OnLaserButtonClicked;
            _shipView.LaserButtonUp += OnLaserButtonUp;
            _parent = Main.Instance.ProjectilesParent;
            for (var i = 0; i < 10; ++i)
            {
                var obj = Object.Instantiate(_shipSettings.projectilePrefab, _parent);
                _projectilesPool.Add(obj);
                obj.gameObject.SetActive(false);
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            _laserView.OnEnemyTriggerEnter -= OnLaserTriggerEnter;
            _shipView.OnEnemyTriggerEnter -= OnEnemyTriggerEnter;
            _shipView.MoveButtonClicked -= OnMoveButtonClicked;
            _shipView.MoveButtonUp -= OnMoveButtonUp;
            _shipView.OnEnemyTriggerEnter -= OnEnemyTriggerEnter;
            _shipView.ShootButtonClicked -= OnShootButtonClicked;
            _shipView.LaserButtonClicked -= OnLaserButtonClicked;
            _shipView.LaserButtonUp -= OnLaserButtonUp;
        }

        public override void Update()
        {
            for (var i = _projectiles.Count-1; i >= 0; --i)
            {
                ProjectileMove(_projectiles[i].transform.up, _projectiles[i], 10);
            }
            if (_battlefieldModel.IsGameOver)
            {
                return;
            }
            if (!_laserEnabled)
            {
                _userModel.AddLaserAmount();
            }
            Rotate();
        }
        
        private void Rotate()
        {
            var mousePos = _battlefieldModel.Camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            _shipView.transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - _shipView.transform.position);
        }
        protected void OnEnemyTriggerEnter(BaseUnitView view)
        {
            _userModel.DestroyShip();
            view.gameObject.SetActive(false);
        }

        protected override void Move(BaseUnitView baseUnitView, Vector3 direction)
        {
            base.Move(baseUnitView, direction);
            _moveControl.Move(direction, baseUnitView, _shipSettings.speed);
        }

        private void OnMoveButtonClicked()
        {
            SetDirection(_shipView.transform.up);
            Move(_shipView, _direction);
        }
        private void OnMoveButtonUp()
        {
            _direction = Vector3.zero;
        }
        private void SetDirection(Vector3 dir)
        {
            _direction = dir;
        }
        private void OnShootButtonClicked(Transform pos)
        {
            if (_battlefieldModel.IsGameOver)
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
            var obj = Object.Instantiate(_shipSettings.projectilePrefab, _parent);
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
            _userModel.UseLaserAmount();
        }
        
        private void OnLaserButtonUp(GameObject laserObject)
        {
            _laserEnabled = false;
            _laserView.gameObject.SetActive(false);
        }

        private void ProjectileMove(Vector3 dir, BaseUnitView view, float speed)
        {
            _moveControl.Move(dir, view, speed);
            _moveControl.TeleportOrDisappearEffect(view, true, () =>
            {
                _projectiles.Remove(view);
                view.gameObject.SetActive(false);
            });
           
        }

        private void ProjectileOnTriggerEnter(BaseUnitView view)
        {
            view.OnEnemyTriggerEnter -= ProjectileOnTriggerEnter;
            _userModel.AddScore();
            _projectiles.Remove(view);
            view.gameObject.SetActive(false);
        }
        private void OnLaserTriggerEnter(BaseUnitView unitView)
        {
            _userModel.AddScore();
        }
    }
}