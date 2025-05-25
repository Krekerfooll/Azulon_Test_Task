using UnityEngine;
using TMPro;

namespace AzulonTest.UI
{
    public class InventorySlotView : SlotBaseView
    {
        [SerializeField] private TextMeshProUGUI _countText;

        public void Init(Sprite icon, Color outlineColor, Color iconColor, int itemsCount)
        {
            base.Init(icon, outlineColor, iconColor);
            SetCount(itemsCount);
        }

        public virtual void SetCount(int count)
        {
            _countText.text = count.ToString();
            _countText.gameObject.SetActive(count > 0);
        }
    }
}
