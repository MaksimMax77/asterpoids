using System;
using Controls;
using UnityEngine;
namespace MVC
{
    public class BaseUnitView : BaseView
    {
        public event Action<BaseUnitView>OnEnemyTriggerEnter;
        public event Action Disabled;
        [SerializeField] protected EffectsContainer _effectsContainer;
        [SerializeField] protected CollisionType _collisionType;
        private Vector2 _startPos;
        public Vector2 StartPos => _startPos;
        public void Start()
        {
            _startPos = transform.position;
        }

        public override void Dispose()
        {
            base.Dispose();
            OnEnemyTriggerEnter = null;
        }

        private void OnDestroy()
        {
            Dispose();
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            var unit = other.GetComponent<CollisionType>();

            if (unit == null)
            {
                return;
            }

            for (int i = 0, len = _collisionType.EnemyColliders.Count; i < len; ++i)
            {
                if (unit.CurrentUnit == _collisionType.EnemyColliders[i])
                {
                    OnEnemyTriggerEnter?.Invoke(this);
                    break;
                }
            }
        }

        protected virtual void OnEnable()
        {
            if (_effectsContainer == null)
            {
                return;
            }
            _effectsContainer.ShowRespawnEffects(transform);
        }

        protected virtual void OnDisable()
        {
            Disabled?.Invoke();
            if (_effectsContainer == null)
            {
                return;
            }
            _effectsContainer.ShowDestroyEffects(transform);
        }
    }
}

 
