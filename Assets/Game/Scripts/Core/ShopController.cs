using AzulonTest.Data;
using AzulonTest.UI;
using System.Collections.Generic;

namespace AzulonTest.Managers
{
    public class ShopController
    {
        private ShopView _shopView;
        private ItemsDatabase _itemsDatabase;
        private ShopDatabase _shopDatabase;
        private IInventoryController _inventoryController;

        public void Init(
            ShopView shopView, BuyItemView buyItemView, IInventoryController inventoryController, 
            ItemsDatabase itemsDatabase, ShopDatabase shopDatabase)
        {
            _shopView = shopView;
            _itemsDatabase = itemsDatabase;
            _shopDatabase = shopDatabase;
            _inventoryController = inventoryController;

            var viewData = new List<ShopViewSlotData>();

            foreach (var item in _shopDatabase.ShopItems)
            {
                var data = _itemsDatabase.GetById(item.ItemId);
                if (data == null) continue;

                viewData.Add(new ShopViewSlotData(data, item.Price));
            }

            _shopView.Init(buyItemView, viewData);
            _shopView.OnBuy += OnItemBuy;
        }

        protected virtual void OnItemBuy(string id, int count)
        {
            _inventoryController.AddItems(id, count);
        }
    }
}
