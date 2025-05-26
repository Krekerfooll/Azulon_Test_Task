using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using AzulonTest.Data;

namespace AzulonTest.UI
{
    public class ShopSlotView : SlotBaseView
    {
        public event Action OnShopSlotClick;

        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private Image _titleBackground;
        [Space]
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

        public void Init(ShopSlotViewData viewData)
        {
            base.Init(viewData.Icon, viewData.OutlineColor, viewData.IconColor);
            SetTitle(viewData.Title, viewData.TitleColor);
            SetPrice(viewData.Price);
        }

        private void OnEnable() => _buyButton.onClick.AddListener(OnBuyButtonClick);
        private void OnDisable() => _buyButton.onClick.RemoveListener(OnBuyButtonClick);

        public virtual void SetTitle(string text, Color color)
        {
            _titleText.text = text;
            _titleBackground.color = color;
        }
        public virtual void SetPrice(int price)
        {
            Price = price;
        }

        protected virtual void OnBuyButtonClick()
        {
            OnShopSlotClick?.Invoke();
        }

        public readonly struct ShopSlotViewData
        {
            public readonly Sprite Icon;
            public readonly string Title;
            public readonly int Price;

            public readonly Color IconColor;
            public readonly Color TitleColor;
            public readonly Color OutlineColor;

            public ShopSlotViewData(Sprite icon, string title, int price, Color iconColor, Color titleColor, Color outlineColor)
            {
                Icon = icon;
                Title = title;
                Price = price;
                IconColor = iconColor;
                TitleColor = titleColor;
                OutlineColor = outlineColor;
            }

            public static ShopSlotViewData From(ItemData data, int price)
            {
                var colorByRarity = GlobalData.GetColorByRarity(data.Rarity);

                return new ShopSlotViewData(
                    icon: data.Icon,
                    title: data.Name,
                    price: price,
                    iconColor: data.IconColor,
                    titleColor: colorByRarity,
                    outlineColor: colorByRarity
                );
            }
        };
    }
}
