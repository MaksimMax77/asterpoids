using System;
using Controls;
using Settings;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MVC
{
    public class UserModel : BaseModel
    {
        private const float _maxLaserValue = 100;
        public event Action ShipDestroyedEvent; 
        public event Action<float> LaserAmountUpdate;
        public event Action<int> ScoreAmountUpdate;
        private GameSettings.ShipSettings _shipSettings;
        private BaseUnitView _shipView;
        private float _laserAmount;
        private int _score;
        private Vector3 _startPosition;
        private Transform _patent;
        private BattlefieldModel _battlefieldModel;
        private MotionWithInertia _motionWithInertia;
        public BaseUnitView ShipView => _shipView;
        public float MaxLaserValue => _maxLaserValue;
        public float LaserAmount => _laserAmount;
        public GameSettings.ShipSettings ShipSettings => _shipSettings;
        public int Score => _score;
        public MotionWithInertia MotionWithInertia => _motionWithInertia;
        
        public UserModel(GameSettings gameSettings, BattlefieldModel battlefieldModel)
        {
            _patent = Main.Instance.UnitsParent;
            _shipSettings = gameSettings.shipSettings;
            _laserAmount = _shipSettings.laserAmount;
            _battlefieldModel = battlefieldModel;
            _battlefieldModel.RestartEvent += OnRespawn;
            _motionWithInertia = new MotionWithInertia(_battlefieldModel.Camera);
            CreteUnitView();
        }

        public override void Dispose()
        {
            base.Dispose();
            _battlefieldModel.RestartEvent -= OnRespawn;
        }

        private void CreteUnitView()
        {
            _shipView = Object.Instantiate(_shipSettings.unitView, _patent);
            _startPosition = _shipView.transform.position;
        }

        public void UseLaserAmount()
        {
            _laserAmount = Mathf.Clamp(_laserAmount -= Time.deltaTime * 20, 0, _maxLaserValue);
            LaserAmountUpdate?.Invoke(_laserAmount);
        }

        public void AddLaserAmount()
        {
            if (_laserAmount >= _maxLaserValue)
            {
                return;
            }
            _laserAmount = Mathf.Clamp(_laserAmount += Time.deltaTime * 20, 0, _maxLaserValue);
            LaserAmountUpdate?.Invoke(_laserAmount);
        }

        public float SetLaserAmountZero()
        {
            _laserAmount = 0;
            LaserAmountUpdate?.Invoke(_laserAmount);
            return _laserAmount;
        }

        public void AddScore()
        {
            ++_score;
            ScoreAmountUpdate?.Invoke(_score);
        }

        public void DestroyShip()
        {
            if (_battlefieldModel.IsGameOver)
            {
                return;
            }
            _shipView.gameObject.SetActive(false);
            ShipDestroyedEvent?.Invoke();
        }

        private void OnRespawn()
        {
            _score = 0;
            _shipView.transform.position = _startPosition;
            _shipView.gameObject.SetActive(true);
        }
    }
}
