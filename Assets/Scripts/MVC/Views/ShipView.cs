using System;
using UnityEngine;

namespace MVC
{
    public class ShipView: BaseUnitView
    {
        public event Action MoveButtonClicked;
        public event Action MoveButtonUp;
        public event Action< Transform> ShootButtonClicked; 
        public event Action<GameObject> LaserButtonClicked;
        public event Action<GameObject> LaserButtonUp;

        [SerializeField] private Transform _projectileStartPos;
        [SerializeField] private GameObject _laser;
        [SerializeField] private BaseUnitView _laserView;
        private MyInputSystem _inputSystem;
        public BaseUnitView LaserView => _laserView;
        private void Awake()
        {
            _inputSystem = new MyInputSystem();
            _inputSystem.StandartInput.Shoot.performed += context => OnShootButton();
            _inputSystem.StandartInput.Move.performed += context => OnMoveButton();
        }

        public override void Dispose()
        {
            MoveButtonClicked = null;
            MoveButtonUp = null;
            ShootButtonClicked = null;
            LaserButtonClicked = null;
            LaserButtonUp = null;
            _inputSystem.Dispose();
            base.Dispose();
        }

        protected override void OnEnable()
        {
            _inputSystem.Enable();
            base.OnEnable();
        }
        protected override void OnDisable()
        {
            _inputSystem.Disable();
            base.OnDisable();
        }
        private void OnShootButton()
        {
            ShootButtonClicked?.Invoke(_projectileStartPos);
        }
        private void OnMoveButton()
        {
            MoveButtonClicked?.Invoke();
        }
        private void Update()
        {
            if (!gameObject.activeInHierarchy)
            {
                return;
            }
            
            if (_inputSystem.StandartInput.Laser.ReadValue<float>() > 0.1f)
            {
                LaserButtonClicked?.Invoke(_laser);
            }
            else
            {
                LaserButtonUp?.Invoke(_laser);
            }
            
            if (_inputSystem.StandartInput.Move.ReadValue<float>() > 0.1f)
            {
                MoveButtonClicked?.Invoke();
            }
            else
            {
                MoveButtonUp?.Invoke();
            }
            
        }
    }
}