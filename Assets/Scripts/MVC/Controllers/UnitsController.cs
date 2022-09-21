using Controls;
using UnityEngine;

namespace MVC
{ 
    public abstract class UnitsController: BaseController
    {
        protected EnemiesModel _enemiesModel;
        protected MoveControl _moveControl;
        protected BattlefieldModel _battlefieldModel;
        public UnitsController(BattlefieldModel battlefieldModel)
        {
            _enemiesModel = Main.Instance.EnemiesModel;
            _battlefieldModel = battlefieldModel;
            _moveControl = new MoveControl(_battlefieldModel.Camera);
        }

        protected virtual void Move(BaseUnitView baseUnitView, Vector3 direction)
        {
            Teleport(baseUnitView);
        }
        
        private void Teleport(BaseUnitView baseUnitView)
        {
            _moveControl.TeleportOrDisappearEffect(baseUnitView); 
        }
    }
}