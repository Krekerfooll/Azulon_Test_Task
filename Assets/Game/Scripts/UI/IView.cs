using System;

namespace AzulonTest.UI
{
    public interface IView
    {
        event Action OnCloseRequestedFromView;
        event Action<UIController.UIWindowType> OnOpenRequestedFromView;

        bool IsVisible { get; }

        void Open();
        void Close();
    }
}
