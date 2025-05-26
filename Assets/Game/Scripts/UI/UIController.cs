using System.Collections.Generic;

namespace AzulonTest.UI
{
    public class UIController
    {
        public enum UIWindowType
        {
            None,
            MainMenu,
            Shop,
            Inventory,
            BuyConfirmation,
        }

        private Dictionary<UIWindowType, IView> _windows;
        private UIWindowType _currentWindow = UIWindowType.None;
        private UIWindowType _previousWindow = UIWindowType.None;

        public void SetView(UIWindowType type, IView view)
        {
            _windows ??= new Dictionary<UIWindowType, IView>();
            _windows[type] = view;
        }

        public bool IsOpen(UIWindowType type)
        {
            return _currentWindow == type && _windows.TryGetValue(type, out var view) && view.IsVisible;
        }

        public void Open(UIWindowType type)
        {
            if (_currentWindow == type)
                return;

            CloseCurrent();

            if (_windows.TryGetValue(type, out var window))
            {
                window.OnCloseRequestedFromView += OnCloseRequestedInternal;
                window.OnOpenRequestedFromView += OnOpenRequestedInternal;
                window.Open();
                _currentWindow = type;
            }
        }
        public void CloseCurrent()
        {
            if (_currentWindow != UIWindowType.None &&
                _windows.TryGetValue(_currentWindow, out var window))
            {
                window.OnCloseRequestedFromView -= OnCloseRequestedInternal;
                window.OnOpenRequestedFromView -= OnOpenRequestedInternal;
                window.Close();
            }

            _previousWindow = _currentWindow;
            _currentWindow = UIWindowType.None;
        }

        private void OnCloseRequestedInternal()
        {
            if (_currentWindow == UIWindowType.None)
                return;

            if (_windows.TryGetValue(_currentWindow, out var window) && window.PreferedClosePath != UIWindowType.None)
            {
                _previousWindow = window.PreferedClosePath;
            }

            if (_previousWindow == UIWindowType.None)
            {
                CloseCurrent();
                return;
            }

            Open(_previousWindow);
        }
        private void OnOpenRequestedInternal(UIWindowType type)
        {
            Open(type);
        }
    }
}
