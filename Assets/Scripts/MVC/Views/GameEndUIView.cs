using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVC
{
    public class GameEndUIView : BaseView
    {
        public event Action<GameEndUIView> ButtonClicked;
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _scoreText; 

        private void Awake()
        {
            _button.onClick.AddListener(() => ButtonClick(this));
        }

        public override void Dispose()
        {
            base.Dispose();
            ButtonClicked = null;
        }
        public void SetScoreText(int value)
        {
            _scoreText.text = value.ToString();
        }
        private void ButtonClick(GameEndUIView baseView)
        {
            ButtonClicked?.Invoke(baseView);
        }
        private void OnDestroy()
        {
            _button.onClick.RemoveListener(() => ButtonClick(this));
            Dispose();
        }
    }
}
