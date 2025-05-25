using System.Collections.Generic;
using UnityEngine;
using AzulonTest.Data;
using System;
using AzulonTest.UI;

namespace AzulonTest.Managers
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private ItemsDatabase _itemsDatabase;
        [SerializeField] private Transform _slotsRoot;
        [SerializeField] private InventorySlotView _slotPrefab;

        private List<ItemStack> _items;

        public void Init(List<ItemStack> loadedInventory)
        {
            _items = loadedInventory;

            foreach (var stack in _items)
            {
                var data = _itemsDatabase.GetById(stack.ItemId);
                if (data == null) continue;

                var slot = Instantiate(_slotPrefab, _slotsRoot);
                var outlineColor = ClobalData.GetColorByRarity(data.Rarity);

                slot.Init(data.Icon, outlineColor, data.IconColor, stack.Count);
            }
        }
    }

    [Serializable]
    public struct ItemStack
    {
        public string ItemId;
        public int Count;
    }
}
