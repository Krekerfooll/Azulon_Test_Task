using System;

namespace AzulonTest.UI
{
    public interface IView
    {
        event Action OnCloseRequestedFromView;
        event Action<UIController.UIWindowType> OnOpenRequestedFromView;

        bool IsVisible { get; }
        UIController.UIWindowType PreferedClosePath { get; }

        void Open();
        void Close();
    }
}
