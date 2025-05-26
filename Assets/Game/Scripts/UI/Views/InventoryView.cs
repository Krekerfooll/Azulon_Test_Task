using AzulonTest.Data;
using System.Collections.Generic;
using UnityEngine;
using static AzulonTest.UI.InventorySlotView;
using static AzulonTest.UI.UIController;

namespace AzulonTest.UI
{
    public class InventoryView : ViewBase
    {
        public override UIWindowType PreferedClosePath => UIWindowType.MainMenu;

        [SerializeField] private Transform _slotsRoot;
        [SerializeField] private InventorySlotView _slotPrefab;

        public void Init(List<InventoryViewSlotData> loadedInventory)
        {
            Dispose();

            foreach (var stack in loadedInventory)
            {
                var slot = Instantiate(_slotPrefab, _slotsRoot);
                var slotViewData = InventorySlotViewData.From(stack.Item, stack.Count);

                slot.Init(slotViewData);
            }
        }

        public override void Dispose()
        {
            if (_slotsRoot == null)
                return;

            foreach (Transform child in _slotsRoot)
            {
                if (child == null)
                    continue;

                Destroy(child.gameObject);
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
