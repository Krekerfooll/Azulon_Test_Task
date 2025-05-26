using System.Collections.Generic;
using AzulonTest.Data;
using System;
using AzulonTest.UI;
using RomanKapustynskyi.SaveSystem;

namespace AzulonTest.Managers
{
    public interface IInventoryController
    {
        void AddItems(string id, int count);
        bool TryRemoveItems(string id, int count);
    }

    public class InventoryController : IInventoryController
    {
        private InventoryView _inventoryView;
        private ItemsDatabase _itemsDatabase;

        private StorageProxy<List<ItemStack>> _inventoryData;

        public void Init(InventoryView inventoryView, ItemsDatabase itemsDatabase)
        {
            _inventoryView = inventoryView;
            _itemsDatabase = itemsDatabase;

            _inventoryData = new StorageProxy<List<ItemStack>>(GlobalData.INVENTORY_DATA_SAVE, new List<ItemStack>(), StorageMode.Persistent);
            _inventoryData.OnValueChanged += UpdateInventoryView;

            UpdateInventoryView(_inventoryData.Value);
        }

        public void AddItems(string id, int count)
        {
            var data = _inventoryData.Value;

            for (int i = 0; i < data.Count; i++)
            {
                var itemStack = data[i];

                if (itemStack.ItemId == id)
                {
                    data[i] = new ItemStack { ItemId = id, Count = itemStack.Count + count };
                    _inventoryData.ForceSave(true);
                    return;
                }
            }

            data.Add(new ItemStack { ItemId = id, Count = count });
            _inventoryData.ForceSave(true);
        }
        public bool TryRemoveItems(string id, int count)
        {
            var data = _inventoryData.Value;

            for (int i = 0; i < data.Count; i++)
            {
                var itemStack = data[i];

                if (itemStack.ItemId == id)
                {
                    if (itemStack.Count >= count)
                    {
                        data[i] = new ItemStack { ItemId = id, Count = itemStack.Count - count };
                        _inventoryData.ForceSave(true);
                        return true;
                    }
                }
            }

            return false;
        }

        private void UpdateInventoryView(List<ItemStack> inventoryData)
        {
            var viewData = new List<InventoryViewSlotData>();

            foreach (var stack in inventoryData)
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
