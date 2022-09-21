using System.Collections;
using System.Collections.Generic;
using Codice.Client.BaseCommands;
using Settings;
using UnityEngine;

namespace MVC
{
    public class EnemiesModel: BaseModel
    {
        private GameSettings.AsteroidSettings _asteroisSettings;
        private GameSettings.UfoSettings _ufoSettings;
        private GameSettings.SmallAsteroidSettings _fragmentsSettings;
        
        private List<BaseUnitView> _ufos = new List<BaseUnitView>();
        private List<BaseUnitView> _asteroids = new List<BaseUnitView>();
        private List<BaseUnitView> _fragmets = new List<BaseUnitView>();
        private List<BaseUnitView> _disabledAsteroids = new List<BaseUnitView>();
        private List<BaseUnitView> _disabledUfos = new List<BaseUnitView>();

        private List<Vector3> _asteroidsDirections = new List<Vector3>();
        private List<Vector3> _fragmentsDirections = new List<Vector3>();

        private MonoBehaviour _monoBehaviour;
        private BattlefieldModel _battlefieldModel;
        private Transform _unitsParent;

        public List<BaseUnitView> Ufos => _ufos;
        public List<BaseUnitView> Asteroids => _asteroids;
        public List<BaseUnitView> Fragmets => _fragmets;
        public List<Vector3> AsteroidsDirections => _asteroidsDirections;
        public List<Vector3> FragmentsDirections => _fragmentsDirections;

        public EnemiesModel(GameSettings.AsteroidSettings asteroidsSettings, GameSettings.UfoSettings ufoSettings, 
            GameSettings.SmallAsteroidSettings fragmentsSettings, 
            MonoBehaviour monoBehaviour, BattlefieldModel battlefieldModel)
        {
            _asteroisSettings = asteroidsSettings;
            _ufoSettings = ufoSettings;
            _fragmentsSettings = fragmentsSettings;
            _monoBehaviour = monoBehaviour;
            _battlefieldModel = battlefieldModel;
            _unitsParent = Main.Instance.UnitsParent;
            _battlefieldModel.RestartEvent += OnRestart;
            for (int i = 0, len = _asteroisSettings.amount; i<len; ++i)
            {
                CreateUnit(_asteroisSettings.unitView, _asteroids);
                _asteroidsDirections.Add(Random.insideUnitCircle.normalized);
            }
            for (int i = 0, len = _ufoSettings.amount; i<len; ++i)
            {
                CreateUnit(_ufoSettings.unitView, _ufos);
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            _battlefieldModel.RestartEvent -= OnRestart;
        }

        private void OnRestart()
        {
            foreach (var ufo in _ufos)
            {
                ufo.transform.position = ufo.StartPos;
            }
        }
        public void StartRespawnTimer(float time, BaseUnitView view, bool ufo = false)
        {
            _monoBehaviour.StartCoroutine(RespawnTimer(time, view, ufo));
        }
        public void DisableAsteroid(BaseUnitView view)
        {
            var index =_asteroids.IndexOf(view);
            _asteroidsDirections.RemoveAt(index);
            DisableUnit(view, _asteroids, _disabledAsteroids);
        }

        public void DisableUfo(BaseUnitView view)
        {
            var index =_asteroids.IndexOf(view);
            DisableUnit(view, _ufos, _disabledUfos);
        }
        
        public List<BaseUnitView> CreateFragments(Vector3 position)
        {
            var createdObj = new List<BaseUnitView>();
            for (var i = 0; i < _fragmentsSettings.amount; ++i)
            {
                var obj = Object.Instantiate(_fragmentsSettings.unitView, _unitsParent);
                obj.transform.position = position;
                _fragmets.Add(obj);
                _fragmentsDirections.Add(Random.insideUnitCircle.normalized);
                createdObj.Add(obj);
            }
            return createdObj;
        }

        public void DestroyFragment(BaseUnitView view)
        {
            var index = _fragmets.IndexOf(view);
            _fragmentsDirections.RemoveAt(index);
            _fragmets.Remove(view);
            Object.Destroy(view.gameObject);
        }

        public BaseUnitView EnableAsteroid(BaseUnitView view)
        {
             EnableUnit(view, _asteroids, _disabledAsteroids);
            _asteroidsDirections.Add(Random.insideUnitCircle.normalized);
            return view;
        }
        
        public BaseUnitView EnableUfo(BaseUnitView view)
        {
            EnableUnit(view, _ufos, _disabledUfos);
            return view;
        }
        private BaseUnitView CreateUnit(BaseUnitView prefab, List<BaseUnitView> units)
        {
            var obj = Object.Instantiate(prefab, _unitsParent);
            units.Add(obj);
            return obj;
        }
        private void DisableUnit(BaseUnitView view, List<BaseUnitView> enableViews, List<BaseUnitView> disableViews)
        {
            view.gameObject.SetActive(false);
            disableViews.Add(view);
            enableViews.Remove(view);
        }

        private void EnableUnit(BaseUnitView view, List<BaseUnitView> enableViews, List<BaseUnitView> disableViews)
        {
            view.gameObject.SetActive(true);
            enableViews.Add(view);
            disableViews.Remove(view);
        }

        private IEnumerator RespawnTimer(float time, BaseUnitView view, bool ufo)
        {
            yield return new WaitForSeconds(time);
            BaseUnitView obj = null;
            
            obj = ufo ? EnableUfo(view) : EnableAsteroid(view);
            obj.transform.position = obj.StartPos;
        }
    }
}