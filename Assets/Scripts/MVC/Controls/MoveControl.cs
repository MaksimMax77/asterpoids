using System;
using MVC;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Controls
{
    public class MoveControl
    {
        public event Action Disappear;
        private Camera _camera;

        public MoveControl(Camera camera)
        {
            _camera = camera;
        }
        public void Move(Vector3 direction, BaseView view, float speed)
        {
            view.transform.position += direction * speed * Time.deltaTime;
        }
        public void TeleportOrDisappearEffect(BaseView view, bool disappear = false, Action Disappear = null)
        {
            var pos = _camera.WorldToScreenPoint(view.transform.position);
            if (pos.x > Screen.width || pos.x < 0)
            {
                if (disappear)
                {
                    Disappear?.Invoke();
                }
                else
                {
                    view.transform.position = new Vector3((_camera.ScreenToWorldPoint(pos).x * -1) + GetOffset(pos.x),
                        view.transform.position.y);
                }
            }

            if (pos.y > Screen.height || pos.y < 0)
            {
                if (disappear)
                {
                    Disappear?.Invoke();
                }
                else
                {
                    view.transform.position = new Vector3(view.transform.position.x,
                        (_camera.ScreenToWorldPoint(pos).y * -1) + GetOffset(pos.y));
                }
            }
        }

        private float GetOffset(float pos)
        {
            float value = 0;
            switch (pos)
            {
                case > 0:
                    value = 0.9f;
                    break;
                case < 0:
                    value = -0.9f;
                    break;
            }

            return value;
        }
    }
}
