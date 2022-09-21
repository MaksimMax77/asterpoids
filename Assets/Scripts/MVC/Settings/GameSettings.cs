using System;
using System.Collections;
using System.Collections.Generic;
using MVC;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "New GameSettings", menuName = "Game Settings", order = 51)]
    public class GameSettings : ScriptableObject
    {
        public SmallAsteroidSettings smallAsteroidSettings;
        public UfoSettings ufoSettings;
        public AsteroidSettings asteroidSettings;
        public ShipSettings shipSettings;
        public ProjectileSettings projectileSettings;

        [Serializable]
        public class AsteroidSettings
        {
            public float speed;
            public int amount;
            public BaseUnitView unitView;
        }

        [Serializable]
        public class SmallAsteroidSettings : AsteroidSettings
        {
        }

        [Serializable]
        public class UfoSettings : AsteroidSettings
        {
        }

        [Serializable]
        public class ShipSettings : AsteroidSettings
        {
            public BaseUnitView projectilePrefab;
            public float projectileSpeed;
            public float laserAmount;
        }

        [Serializable]
        public class ProjectileSettings : AsteroidSettings
        {

        }
    }
}
