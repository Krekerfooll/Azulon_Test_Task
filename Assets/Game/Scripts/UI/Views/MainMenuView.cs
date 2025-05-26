using AzulonTest.UI.Utils;
using UnityEngine;

namespace AzulonTest.UI
{
    public class MainMenuView : ViewBase
    {
        [SerializeField] private OpenViewButton[] _openViewButtons;

        public override void Open()
        {
            base.Open();

            foreach (var button in _openViewButtons)
            {
                button.OnViewOpenRequested += OnOpenButtonClick;
            }
        }

        public override void Close()
        {
            base.Close();
            Dispose();
        }

        public override void Dispose()
        {
            foreach (var button in _openViewButtons)
            {
                button.OnViewOpenRequested -= OnOpenButtonClick;
            }
        }
    }
}
