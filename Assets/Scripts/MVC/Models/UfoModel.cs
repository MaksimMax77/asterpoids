using System.Collections.Generic;
using Settings;
using UnityEngine;

namespace MVC
{
    public class UfoModel: BaseModel
    {
        private GameSettings _gameSettings;
        private List<BaseUnitView> _units = new List<BaseUnitView>();
        private List<BaseUnitView> _disabledUnits = new List<BaseUnitView>();
        private Camera _camera;
        private Transform _parent;
        
        public List<BaseUnitView> Units => _units;
        public GameSettings GameSettings => _gameSettings;
        public Camera Camera => _camera;
        public List<BaseUnitView> DisabledUnits => _disabledUnits;
        public Transform Parent => _parent;
        
        public UfoModel(GameSettings gameSettings, Camera camera, Transform parent)
        {
            _camera = camera;
            _gameSettings = gameSettings;
            _parent = parent;
        }
        
        public void AddUnit(BaseUnitView unit)
        {
            _units.Add(unit);
            _disabledUnits.Remove(unit);
        }
        
        public void RemoveUnit(BaseUnitView unit)
        {
            _units.Remove(unit);
            _disabledUnits.Add(unit);
        }
    }
}