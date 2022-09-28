using System.Collections;
using Controls;
using Settings;
using UnityEngine;

namespace MVC
{
    public class UfoController: BaseController
    {
        protected UfoModel _model;
        protected MoveControl _moveControl;
        protected MonoBehaviour _monoBehaviour;
        protected GameSettings.AsteroidSettings _settings;
        private BaseUnitView _target;
        private GameManager _gameManager;

        public UfoController(UfoModel model, MonoBehaviour monoBehaviour, GameManager gameManager)
        {
            _model = model;
            _moveControl = new MoveControl(_model.Camera);
            _monoBehaviour = monoBehaviour;
            _gameManager = gameManager;
            _gameManager.RestartEvent += OnRestart;
            SetSettings();
            CreateUnits();
            SetTarget();
        }

        public override void Dispose()
        {
            _gameManager.RestartEvent -= OnRestart;
        }

        protected virtual void SetSettings()
        {
            _settings = _model.GameSettings.ufoSettings;
        }
        public override void Update()
        {
            for(int i = 0, len = _model.Units.Count; i < len; ++i)
            {
                _moveControl.TeleportOrDisappearEffect(_model.Units[i]);
                _moveControl.Move(_target.gameObject.transform.position - _model.Units[i].gameObject.transform.position,_model.Units[i],
                    _settings.speed);
            }
        }
        protected void CreateUnits()
        {
            for (var i = 0; i < _settings.amount; ++i)
            {
                var unit = Object.Instantiate(_settings.unitView, 
                    _model.Parent);
                unit.OnEnemyTriggerEnter += OnEnemyTriggerEnter;
                _model.AddUnit(unit);
            }
        }
        protected virtual void OnEnemyTriggerEnter(BaseUnitView obj)
        {
            obj.gameObject.SetActive(false);
            _model.RemoveUnit(obj);
            _monoBehaviour.StartCoroutine(RespawnTimer(3f, obj));
        }
        
        private void OnRestart()
        {
            foreach (var ufo in _model.Units)
            {
                ufo.transform.position = ufo.StartPos;
            }
        }
        private IEnumerator RespawnTimer(float time, BaseUnitView view)
        {
            yield return new WaitForSeconds(time);
            _model.AddUnit(view);
            view.gameObject.SetActive(true);
            view.transform.position = view.StartPos;
        }
        private void SetTarget()
        {
            _target = Main.Instance.GameManager.UserModel.Ship;
        }
    }
}