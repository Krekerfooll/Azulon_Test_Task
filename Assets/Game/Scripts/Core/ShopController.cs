using UnityEngine;
using AzulonTest.Data;
using AzulonTest.UI;
using System;

namespace AzulonTest.Managers
{
    public class ShopController : MonoBehaviour
    {
        public event Action<bool> OnBuy;

        [SerializeField] private ItemsDatabase _itemsDatabase;
        [SerializeField] private ShopDatabase _shopDatabase;
        [SerializeField] private Transform _slotsRoot;
        [SerializeField] private ShopSlotView _slotPrefab;

        public void Init()
        {
            foreach (var item in _shopDatabase.ShopItems)
            {
                var data = _itemsDatabase.GetById(item.ItemId);
                if (data == null) continue;

                var slot = Instantiate(_slotPrefab, _slotsRoot);
                var outlineColor = ClobalData.GetColorByRarity(data.Rarity);

                slot.Init(data.Icon, outlineColor, data.IconColor, item.Price);
            }
        }
    }
}
