using Settings;
using UnityEngine;

namespace MVC
{
    public class AsteroidsController : UnitsController
    {
        private GameSettings.AsteroidSettings _asteroidSettings;
        public AsteroidsController(BattlefieldModel battlefieldModel) : base(battlefieldModel)
        {
            _asteroidSettings = battlefieldModel.GameSettings.asteroidSettings;
            
            for (int i = 0, len = _enemiesModel.Asteroids.Count; i < len; ++i)
            {
                _enemiesModel.Asteroids[i].OnEnemyTriggerEnter += OnEnemyTriggerEnter;
            }
        }

        public override void Dispose()
        {
            for (int i = 0, len = _enemiesModel.Asteroids.Count; i < len; ++i)
            {
                _enemiesModel.Asteroids[i].OnEnemyTriggerEnter -= OnEnemyTriggerEnter;
            }
        }

        public override void Update()
        {
            base.Update();

            for (int i = 0, len = _enemiesModel.Fragmets.Count; i < len; ++i)
            {
                Move(_enemiesModel.Fragmets[i],_enemiesModel.FragmentsDirections[i]);
            }
            for (int i = 0, len = _enemiesModel.Asteroids.Count; i < len; ++i)
            {
                Move(_enemiesModel.Asteroids[i], _enemiesModel.AsteroidsDirections[i]);
            }
        }
        protected void OnEnemyTriggerEnter(BaseUnitView view)
        {
            var fragments = _enemiesModel.CreateFragments(view.transform.position);
           
           for (int i = 0, len = fragments.Count; i < fragments.Count; ++i)
           {
               fragments[i].OnEnemyTriggerEnter += OnEnemyTriggerFragmentEnter;
           }
            _enemiesModel.DisableAsteroid(view);
            _enemiesModel.StartRespawnTimer(2f, view);
       
        }
        private void OnEnemyTriggerFragmentEnter(BaseUnitView view)
        {
            view.OnEnemyTriggerEnter -= OnEnemyTriggerFragmentEnter;
            _enemiesModel.DestroyFragment(view);
        }
        protected override void Move(BaseUnitView baseUnitView, Vector3 direction)
        {
            base.Move(baseUnitView, direction);
            _moveControl.Move(direction, baseUnitView, _asteroidSettings.speed);
        }

    }
}
