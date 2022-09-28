using UnityEngine;

namespace MVC
{
    public class ShipInfoUiController: BaseController
    {
        private ShipInfoUiView _shipInfoUiView;
        private UserModel _userModel;
        public ShipInfoUiController(UserModel userModel,ShipInfoUiView shipInfoUiView, Transform _parent)
        {
            _userModel = userModel;
            _shipInfoUiView = Object.Instantiate(shipInfoUiView, _parent, false);
            _userModel.LaserAmountUpdate += OnLaserAmountUpdate;
            _userModel.ScoreAmountUpdate += OnScoreAmountUpdate;
        }

        private void OnRestart()
        {
            _shipInfoUiView.gameObject.SetActive(true);
        }

        public override void Dispose()
        {
            _userModel.LaserAmountUpdate -= OnLaserAmountUpdate;
            _userModel.ScoreAmountUpdate -= OnScoreAmountUpdate;
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
            SetShipSpeed();
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
            _shipInfoUiView.UpdateShipPositionText(_userModel.ShipPos);
        }
        
        private void UpdateShipRotation()
        {
            _shipInfoUiView.UpdateShipRotationText((int)_userModel.ShipAngle);
        }

        private void SetShipSpeed(/*float currentSpeed*/)
        {
            _shipInfoUiView.SetShipSpeed(/*currentSpeed*/_userModel.ShipSpeed);
        }
    }
}