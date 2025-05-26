using AzulonTest.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using static AzulonTest.UI.ShopSlotView;

namespace AzulonTest.UI
{
    public class ShopView : ViewBase
    {
        public event Action<bool> OnBuy;

        [SerializeField] private Transform _slotsRoot;
        [SerializeField] private ShopSlotView _slotPrefab;

        public void Init(List<ShopViewSlotData> loadedInventory)
        {
            foreach (Transform child in _slotsRoot)
                Destroy(child.gameObject);

            foreach (var data in loadedInventory)
            {
                var slot = Instantiate(_slotPrefab, _slotsRoot);
                var slotViewData = ShopSlotViewData.From(data.Item, data.Price);

                slot.Init(slotViewData);
            }
        }
    }

    public class ShopViewSlotData
    {
        public readonly ItemData Item;
        public readonly int Price;

        public ShopViewSlotData(ItemData item, int price)
        {
            Item = item;
            Price = price;
        }
    }
}
