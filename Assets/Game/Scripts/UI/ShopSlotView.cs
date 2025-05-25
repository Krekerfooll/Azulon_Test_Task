using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace AzulonTest.UI
{
    public class ShopSlotView : SlotBaseView
    {
        public event Action OnShopSlotClick;

        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private Button _buyButton;

        private int _price;
        public int Price
        {
            get => _price;
            private set
            {
                _price = value;
                _priceText.text = value.ToString();
            }
        }

        public void Init(Sprite icon, Color outlineColor, Color iconColor, int price)
        {
            base.Init(icon, outlineColor, iconColor);
            SetPrice(price);
        }

        private void OnEnable() => _buyButton.onClick.AddListener(OnBuyButtonClick);
        private void OnDisable() => _buyButton.onClick.RemoveListener(OnBuyButtonClick);

        public virtual void SetPrice(int price)
        {
            Price = price;
        }

        protected virtual void OnBuyButtonClick()
        {
            OnShopSlotClick?.Invoke();
        }
    }
}
