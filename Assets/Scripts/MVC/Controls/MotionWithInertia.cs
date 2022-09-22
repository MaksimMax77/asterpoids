using System;
using MVC;
using UnityEngine;

namespace Controls
{
    public class MotionWithInertia : MoveControl
    {
        public event Action<float> SpeedChanged;
        private float _currentSpeed = 0f;
        private Vector3 _inertiaDirection;
        public MotionWithInertia(Camera camera) : base(camera)
        {
            
        }

        public void CurrentSpeedSetZero()
        {
            _currentSpeed = 0;
            SpeedChanged?.Invoke(_currentSpeed);
        }
        public void InertiaMove(Vector3 direction, BaseView view, float maxSpeed)
        {
            if (_currentSpeed < maxSpeed)
            {
                _currentSpeed += Time.deltaTime * 5;
                SpeedChanged?.Invoke(_currentSpeed);
            }
            _inertiaDirection = direction;
            Move(direction, view, _currentSpeed);
        }

        public void MoveEnd(BaseUnitView view)
        {
            if (_currentSpeed > 0)
            {
                _currentSpeed -= Time.deltaTime * 2;
                SpeedChanged?.Invoke(_currentSpeed);
                Move(_inertiaDirection, view, _currentSpeed);
            }
        }
    }
}