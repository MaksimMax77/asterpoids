using System.Collections.Generic;
using Settings;
using UnityEngine;

namespace MVC
{
    public class AsteroidsModel: UfoModel
    {
        private List<Vector3> _asteroidsDirections = new List<Vector3>();
        public List<Vector3> AsteroidsDirection => _asteroidsDirections;
        public AsteroidsModel(GameSettings gameSettings, Camera camera, Transform parent) : base(gameSettings, camera, parent)
        {
        }
        public void AddDirection(Vector3 dir)
        {
            _asteroidsDirections.Add(dir);
        }
        
        public void RemoveDirections(Vector3 dir)
        {
            _asteroidsDirections.Remove(dir);
        }
    }
}