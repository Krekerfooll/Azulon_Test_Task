using UnityEngine;
using TMPro;
using AzulonTest.Data;

namespace AzulonTest.UI
{
    public class InventorySlotView : SlotBaseView
    {
        [SerializeField] private TextMeshProUGUI _countText;

        public void Init(InventorySlotViewData viewData)
        {
            base.Init(viewData.Icon, viewData.OutlineColor, viewData.IconColor);
            SetCount(viewData.ItemsCount);
        }

        public virtual void SetCount(int count)
        {
            _countText.text = count.ToString();
            _countText.gameObject.SetActive(count > 0);
        }

        public readonly struct InventorySlotViewData
        {
            public readonly Sprite Icon;
            public readonly Color OutlineColor;
            public readonly Color IconColor;
            public readonly int ItemsCount;

            public InventorySlotViewData(Sprite icon, Color outlineColor, Color iconColor, int itemsCount)
            {
                Icon = icon;
                OutlineColor = outlineColor;
                IconColor = iconColor;
                ItemsCount = itemsCount;
            }

            public static InventorySlotViewData From(ItemData data, int count)
            {
                var colorByRarity = GlobalData.GetColorByRarity(data.Rarity);

                return new InventorySlotViewData(
                    icon: data.Icon,
                    outlineColor: colorByRarity,
                    iconColor: data.IconColor,
                    itemsCount: count
                );
            }
        };
    }
}
