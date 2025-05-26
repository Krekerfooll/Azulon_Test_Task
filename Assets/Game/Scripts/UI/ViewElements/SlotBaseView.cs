using UnityEngine;
using UnityEngine.UI;

namespace AzulonTest.UI
{
    public class SlotBaseView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _outline;

        public void Init(Sprite icon, Color outlineColor, Color iconColor)
        {
            SetIcon(icon, iconColor);
            SetOutlineColor(outlineColor);
        }

        public virtual void SetIcon(Sprite icon, Color color)
        {
            _icon.sprite = icon;
            _icon.color = color;
        }
        public virtual void SetOutlineColor(Color color)
        {
            _outline.color = color;
        }
    }
}
