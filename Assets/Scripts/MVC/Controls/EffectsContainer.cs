using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controls
{
    public class EffectsContainer : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _destroyEffects;

        [SerializeField] private List<GameObject> _spawnEffects;
        private Transform _parent;

        private void Awake()
        {
            _parent = Main.Instance.ProjectilesParent;
        }

        public void ShowDestroyEffects(Transform pos)
        {
            Show(_destroyEffects, pos);
        }

        public void ShowRespawnEffects(Transform pos)
        {
            Show(_spawnEffects, pos);
        }

        private void Show(ICollection gameObjects, Transform pos)
        {
            if (gameObjects == null)
            {
                return;
            }

            for (int i = 0, len = gameObjects.Count; i < len; ++i)
            {
                Instantiate(_destroyEffects[i], pos.position, Quaternion.identity, _parent);
            }
        }
    }
}
