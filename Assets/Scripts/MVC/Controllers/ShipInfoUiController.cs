using UnityEngine;

namespace MVC
{
    public class ShipInfoUiController: BaseController
    {
        private ShipInfoUiView _shipInfoUiView;
        private UserModel _userModel;
        private BaseUnitView _shipView;
        private BattlefieldModel _battlefieldModel;

        public ShipInfoUiController(ShipInfoUiView shipInfoUiView, Transform _parent)
        {
            _shipInfoUiView = Object.Instantiate(shipInfoUiView, _parent, false);
            _userModel = Main.Instance.UserModel;
            _battlefieldModel = Main.Instance.BattlefieldModel; 
            _userModel.LaserAmountUpdate += OnLaserAmountUpdate;
            _userModel.ScoreAmountUpdate += OnScoreAmountUpdate;
            _battlefieldModel.GameOverEvent += OnGameOver;
            _battlefieldModel.RestartEvent += OnRestart;
            _shipView = _userModel.ShipView;
            SetShipSpeed();
        }

        private void OnRestart()
        {
            _shipInfoUiView.gameObject.SetActive(true);
        }

        public override void Dispose()
        {
            _userModel.LaserAmountUpdate -= OnLaserAmountUpdate;
            _userModel.ScoreAmountUpdate -= OnScoreAmountUpdate;
            _battlefieldModel.GameOverEvent -= OnGameOver;
            _battlefieldModel.RestartEvent -= OnRestart;
        }

        private void OnGameOver()
        {
            _shipInfoUiView.UpdateLaserBar(_userModel.MaxLaserValue);
            _shipInfoUiView.gameObject.SetActive(false);
        }

        public override void Update()
        {
            UpdateShipPos();
            UpdateShipRotation();
        }

        private void OnLaserAmountUpdate(float value)
        {
            _shipInfoUiView.UpdateLaserBar(value / _userModel.MaxLaserValue);
        }
        
        private void OnScoreAmountUpdate(int value)
        {
            _shipInfoUiView.UpdateScoreText(value);
        }

        private void UpdateShipPos()
        {
            _shipInfoUiView.UpdateShipPositionText(_shipView.transform.position);
        }
        
        private void UpdateShipRotation()
        {
            _shipInfoUiView.UpdateShipRotationText((int)_shipView.transform.localEulerAngles.z);
        }

        private void SetShipSpeed()
        {
            _shipInfoUiView.SetShipSpeed(_userModel.ShipSettings.speed);
        }
    }
}