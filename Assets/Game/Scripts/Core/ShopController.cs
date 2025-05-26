using UnityEngine;
using AzulonTest.Data;
using AzulonTest.UI;
using System;
using System.Collections.Generic;

namespace AzulonTest.Managers
{
    public class ShopController
    {
        public event Action<bool> OnBuy;

        private ShopView _shopView;
        private ItemsDatabase _itemsDatabase;
        private ShopDatabase _shopDatabase;

        public void Init(ShopView shopView, ItemsDatabase itemsDatabase, ShopDatabase shopDatabase)
        {
            _shopView = shopView;
            _itemsDatabase = itemsDatabase;
            _shopDatabase = shopDatabase;

            var viewData = new List<ShopViewSlotData>();

            foreach (var item in _shopDatabase.ShopItems)
            {
                var data = _itemsDatabase.GetById(item.ItemId);
                if (data == null) continue;

                viewData.Add(new ShopViewSlotData(data, item.Price));
            }

            _shopView.Init(viewData);
        }
    }
}
