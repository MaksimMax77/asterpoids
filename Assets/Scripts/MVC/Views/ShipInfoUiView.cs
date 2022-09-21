using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVC
{
    public class ShipInfoUiView : BaseView
    {
        [SerializeField] private Image _laserBar;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _shipPosition;
        [SerializeField] private TextMeshProUGUI _shipRotation;
        [SerializeField] private TextMeshProUGUI _shipSpeed;
        public void UpdateLaserBar(float value)
        {
            _laserBar.fillAmount = value;
        }
        public void UpdateScoreText(int value)
        {
            _scoreText.text = value.ToString();
        }
        
        public void UpdateShipPositionText(Vector3 value)
        {
            _shipPosition.text = value.ToString();
        }
        
        public void UpdateShipRotationText(float value)
        {
            _shipRotation.text = value.ToString();
        }

        public void SetShipSpeed(float value)
        {
            _shipSpeed.text = value.ToString();
        }
    }
}