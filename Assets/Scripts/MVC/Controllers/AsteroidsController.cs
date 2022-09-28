using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class AsteroidsController: UfoController
    {
        private AsteroidsModel _asteroidsModel;
        private List<BaseUnitView> _fragmets = new List<BaseUnitView>();
        private List<Vector3> _fragmentsDirections = new List<Vector3>();
        
        public AsteroidsController(UfoModel model, MonoBehaviour monoBehaviour, GameManager gameManager) : base(model, monoBehaviour, gameManager)
        {
            _asteroidsModel = (AsteroidsModel)_model;
            for (var i = 0; i < _settings.amount; ++i)
            {
                _asteroidsModel.AddDirection(Random.insideUnitCircle.normalized);
            }
        }
        public override void Update()
        {
            for (int i = 0, len = _asteroidsModel.Units.Count; i < len; ++i)
            {
                _moveControl.TeleportOrDisappearEffect(_asteroidsModel.Units[i]);
                _moveControl.Move(_asteroidsModel.AsteroidsDirection[i], _asteroidsModel.Units[i], _settings.speed);
            }

            for (int i = 0, len = _fragmets.Count; i < len; ++i)
            {
                _moveControl.TeleportOrDisappearEffect(_fragmets[i]);
                _moveControl.Move(_fragmentsDirections[i], _fragmets[i], 
                   _asteroidsModel.GameSettings.smallAsteroidSettings.speed);
            }
        }
        
        protected override void SetSettings()
        {
            _settings = _model.GameSettings.asteroidSettings;
        }

        protected override void OnEnemyTriggerEnter(BaseUnitView view)
        {
            var fragments = CreateFragments(view.transform.position);
           
            for (int i = 0, len = fragments.Count; i < fragments.Count; ++i)
            {
                fragments[i].OnEnemyTriggerEnter += OnEnemyTriggerFragmentEnter;
            }
            base.OnEnemyTriggerEnter(view);
        }

        private void OnEnemyTriggerFragmentEnter(BaseUnitView obj)
        {
            obj.OnEnemyTriggerEnter -= OnEnemyTriggerFragmentEnter;
            var index = _fragmets.IndexOf(obj);
            _fragmets.RemoveAt(index);
            _fragmentsDirections.RemoveAt(index);
            Object.Destroy(obj.gameObject);
        }

        private List<BaseUnitView> CreateFragments(Vector3 pos)
        {
            var createdObj = new List<BaseUnitView>();
            for (var i = 0; i < _model.GameSettings.smallAsteroidSettings.amount; ++i)
            {
                var obj = Object.Instantiate(_model.GameSettings.smallAsteroidSettings.unitView, 
                    _model.Parent);
                obj.transform.position = pos;
                _fragmets.Add(obj);
                _fragmentsDirections.Add(Random.insideUnitCircle.normalized);
                createdObj.Add(obj);
            }
            return createdObj;
        }
    }
}