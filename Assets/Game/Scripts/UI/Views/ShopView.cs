using AzulonTest.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using static AzulonTest.UI.ShopSlotView;
using static AzulonTest.UI.UIController;

namespace AzulonTest.UI
{
    public class ShopView : ViewBase
    {
        public event Action<string, int> OnBuy;
        public override UIWindowType PreferedClosePath => UIWindowType.MainMenu;

        [SerializeField] private Transform _slotsRoot;
        [SerializeField] private ShopSlotView _slotPrefab;

        private BuyItemView _buyItemConfirmationView;
        private IReadOnlyList<ShopViewSlotData> _currentSlots;

        public void Init(BuyItemView buyItemView, List<ShopViewSlotData> viewData)
        {
            Dispose();

            _buyItemConfirmationView = buyItemView;
            _currentSlots = viewData;

            foreach (var data in _currentSlots)
            {
                var slot = Instantiate(_slotPrefab, _slotsRoot);
                var slotViewData = ShopSlotViewData.From(data.Item, data.Price);

                slot.Init(slotViewData);
                slot.OnBuy += OnSlotBuyButtonClick;
            }

            _buyItemConfirmationView.OnBuy += OnConfirmationViewBuyButtonClick;
        }

        protected virtual void OnSlotBuyButtonClick(string id, int count)
        {
            foreach (var slot in _currentSlots)
            {
                if (slot.Item.Id == id)
                {
                    var confirmationViewData = BuyItemViewData.From(slot.Item, slot.Price);
                    _buyItemConfirmationView.Init(confirmationViewData);
                    OnOpenButtonClick(UIController.UIWindowType.BuyConfirmation);
                    return;
                }
            }
        }
        protected virtual void OnConfirmationViewBuyButtonClick(string id, int count)
        {
            OnBuy?.Invoke(id, count);
        }

        public override void Dispose()
        {
            if (_buyItemConfirmationView)
            {
                _buyItemConfirmationView.OnBuy -= OnConfirmationViewBuyButtonClick;
            }

            if (_slotsRoot == null)
                return;

            foreach (Transform child in _slotsRoot)
            {
                if (child == null)
                    continue;

                if (child.TryGetComponent<ShopSlotView>(out var slot))
                {
                    slot.OnBuy -= OnSlotBuyButtonClick;
                }

                Destroy(child.gameObject);
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
