using AzulonTest.Data;
using System.Collections.Generic;
using UnityEngine;
using static AzulonTest.UI.InventorySlotView;

namespace AzulonTest.UI
{
    public class InventoryView : ViewBase
    {
        [SerializeField] private Transform _slotsRoot;
        [SerializeField] private InventorySlotView _slotPrefab;

        public void Init(List<InventoryViewSlotData> loadedInventory)
        {
            foreach (Transform child in _slotsRoot)
                Destroy(child.gameObject);

            foreach (var stack in loadedInventory)
            {
                var slot = Instantiate(_slotPrefab, _slotsRoot);
                var slotViewData = InventorySlotViewData.From(stack.Item, stack.Count);

                slot.Init(slotViewData);
            }
        }
    }

    public class InventoryViewSlotData
    {
        public readonly ItemData Item;
        public readonly int Count;

        public InventoryViewSlotData(ItemData item, int count)
        {
            Item = item;
            Count = count;
        }
    }
}
