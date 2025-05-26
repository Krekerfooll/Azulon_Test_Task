using System;
using UnityEngine;
using UnityEngine.UI;
using static AzulonTest.UI.UIController;

namespace AzulonTest.UI
{
    public abstract class ViewBase : MonoBehaviour, IView
    {
        public event Action OnCloseRequestedFromView;
        public event Action<UIWindowType> OnOpenRequestedFromView;

        [SerializeField] private GameObject _root;
        [SerializeField] private Button _closeButton;

        public bool IsVisible => _root != null && _root.activeSelf;

        public virtual void Open()
        {
            if (_root != null)
                _root.SetActive(true);

            _closeButton?.onClick.AddListener(OnCloseButtonClick);
        }
        public virtual void Close()
        {
            if (_root != null)
                _root.SetActive(false);

            _closeButton?.onClick.RemoveListener(OnCloseButtonClick);
        }

        protected virtual void OnCloseButtonClick()
        {
            OnCloseRequestedFromView?.Invoke();
        }
        protected virtual void OnOpenButtonClick(UIWindowType view)
        {
            OnOpenRequestedFromView?.Invoke(view);
        }
    }
}
