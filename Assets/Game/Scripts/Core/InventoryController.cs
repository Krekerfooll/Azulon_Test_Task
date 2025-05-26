using System.Collections.Generic;
using AzulonTest.Data;
using System;
using AzulonTest.UI;

namespace AzulonTest.Managers
{
    public class InventoryController
    {
        private InventoryView _inventoryView;
        private ItemsDatabase _itemsDatabase;

        public void Init(InventoryView inventoryView, ItemsDatabase itemsDatabase, List<ItemStack> loadedInventory)
        {
            _inventoryView = inventoryView;
            _itemsDatabase = itemsDatabase;

            var viewData = new List<InventoryViewSlotData>();

            foreach (var stack in loadedInventory)
            {
                var data = _itemsDatabase.GetById(stack.ItemId);
                if (data == null) continue;

                viewData.Add(new InventoryViewSlotData(data, stack.Count));
            }

            _inventoryView.Init(viewData);
        }
    }

    [Serializable]
    public struct ItemStack
    {
        public string ItemId;
        public int Count;
    }
}
