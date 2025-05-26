using System;
using UnityEngine;
using UnityEngine.UI;
using static AzulonTest.UI.UIController;

namespace AzulonTest.UI.Utils
{
    [RequireComponent(typeof(Button))]
    public class OpenViewButton : MonoBehaviour
    {
        public event Action<UIWindowType> OnViewOpenRequested;

        [SerializeField] private UIWindowType _viewType;

        private Button _button;

        private void Awake() => _button = GetComponent<Button>();
        protected void OnEnable() => _button.onClick.AddListener(OnButtonClick);
        protected void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);

        private void OnButtonClick() => OnViewOpenRequested?.Invoke(_viewType);
    }
}
