using System;
using UnityEditor;
using UnityEngine;

namespace MVC
{
    public class UserModel : BaseModel
    {
        public event Action<float> LaserAmountUpdate;
        public event Action<int> ScoreAmountUpdate;
        public event Action ShipDestroyedEvent;
        private const float _maxLaserValue = 100;
        private float _laserAmount = _maxLaserValue;
        private int _scores;
        private float _shipSpeed;
        private Vector3 _startPosition;
        private bool _shipDestroyed;
        private Vector3 _shipPos;
        private float _shipAngle;
        private BaseUnitView _ship;
        public float MaxLaserValue => _maxLaserValue;
        public float LaserAmount => _laserAmount;
        public int Scores => _scores;
        public float ShipSpeed => _shipSpeed;
        public Vector3 StartPosition => _startPosition;
        public bool ShipDestroyed => _shipDestroyed;
        public Vector3 ShipPos => _shipPos;
        public float ShipAngle => _shipAngle;
        public BaseUnitView Ship => _ship;

        public void ChangeLaserAmount(bool AddLaserValue)
        {
            if (!AddLaserValue)
            {
                _laserAmount = Mathf.Clamp(_laserAmount -= Time.deltaTime * 20, 0, _maxLaserValue);
                LaserAmountUpdate?.Invoke(_laserAmount);
            }
            else
            {
                if (_laserAmount >= _maxLaserValue)
                {
                    return;
                }

                _laserAmount = Mathf.Clamp(_laserAmount += Time.deltaTime * 20, 0, _maxLaserValue);
                LaserAmountUpdate?.Invoke(_laserAmount);
            }
        }

        public void SetLaserAmountZero()
        {
            _laserAmount = 0;
            LaserAmountUpdate?.Invoke(_laserAmount);
        }

        public void AddScores()
        {
            ++_scores;
            ScoreAmountUpdate?.Invoke(_scores);
        }

        public void ClearScores()
        {
             _scores = 0;
            ScoreAmountUpdate?.Invoke(_scores);
        }

        public void DestroyShip()
        {
            ShipDestroyedEvent?.Invoke();
            _shipDestroyed = true;
        }

        public void RespawnShip()
        {
            _shipDestroyed = false;
        }
        public void SetStartPosition(Vector3 value)
        {
            _startPosition = value;
        }

        public void ShipSpeedSet(float value)
        {
            _shipSpeed = value;
        }

        public void SetShipPosAngle(Vector3 pos, float angle)
        {
            _shipPos = pos;
            _shipAngle = angle;
        }

        public void SetShipView(BaseUnitView view)
        {
            _ship = view;
        }
    }
}