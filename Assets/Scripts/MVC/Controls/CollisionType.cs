using System.Collections.Generic;
using UnityEngine;

namespace Controls
{
    public enum CollisionObjects
    {
        none,
        Asteroid,
        Ufo,
        Ship,
        bullet
    }

    public class CollisionType : MonoBehaviour
    {
        [SerializeField] protected CollisionObjects _currentUnit;
        [SerializeField] protected List<CollisionObjects> _enemyColliders;

        public CollisionObjects CurrentUnit => _currentUnit;
        public List<CollisionObjects> EnemyColliders => _enemyColliders;
    }
}
