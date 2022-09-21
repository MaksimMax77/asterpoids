using Settings;
using UnityEngine;

namespace MVC
{
    public class UfosController: UnitsController
    {
        private BaseUnitView _targetView;
        private GameSettings.UfoSettings _ufoSettings;
        public UfosController(BattlefieldModel battlefieldModel) : base(battlefieldModel)
        {
            _ufoSettings = battlefieldModel.GameSettings.ufoSettings;
            _targetView = Main.Instance.UserModel.ShipView;
            
            for (int i = 0, len = _enemiesModel.Ufos.Count; i < len; ++i)
            {
                _enemiesModel.Ufos[i].OnEnemyTriggerEnter += OnEnemyTriggerEnter;
            }
        }

        public override void Dispose()
        {
            for (int i = 0, len = _enemiesModel.Ufos.Count; i < len; ++i)
            {
                _enemiesModel.Ufos[i].OnEnemyTriggerEnter -= OnEnemyTriggerEnter;
            }
        }

        public override void Update()
        {
            base.Update();
            if (_battlefieldModel.IsGameOver)
            {
                return;
            }
            for (int i = 0, len = _enemiesModel.Ufos.Count; i < len; ++i)
            {
                var dir = _targetView.transform.position - _enemiesModel.Ufos[i].transform.position;
                Move(_enemiesModel.Ufos[i],dir);
                Rotate(_enemiesModel.Ufos[i]);
            }
        }
        private void Rotate(BaseUnitView unitView)
        {
            unitView.transform.rotation = Quaternion.LookRotation(Vector3.forward,  _targetView.transform.position - unitView.transform.position);
        }

        protected override void Move(BaseUnitView baseUnitView, Vector3 direction)
        {
            base.Move(baseUnitView, direction);
            _moveControl.Move(direction, baseUnitView, _ufoSettings.speed);
        }

        protected void OnEnemyTriggerEnter(BaseUnitView view)
        {
            _enemiesModel.DisableUfo(view);
            _enemiesModel.StartRespawnTimer(3, view, true);
        }
    }
}