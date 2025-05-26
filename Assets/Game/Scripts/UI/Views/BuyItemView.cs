using AzulonTest.Data;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AzulonTest.UI
{
    public class BuyItemView : ViewBase
    {
        public event Action<string, int> OnBuy;

        [Space]
        [SerializeField] private Image _titleBackground;
        [SerializeField] private Image _outline;
        [SerializeField] private Image _icon;
        [Space]
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [Space]
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private Button _buyButton;

        private string _id;
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

        public void Init(BuyItemViewData viewData)
        {
            Dispose();

            _id = viewData.Id;
            Price = viewData.Price;

            _titleBackground.color = viewData.TitleColor;
            _outline.color = viewData.OutlineColor;

            _icon.sprite = viewData.Icon;
            _icon.color = viewData.IconColor;

            _titleText.text = viewData.Title;
            _descriptionText.text = viewData.Description;

            _buyButton.onClick.AddListener(OnBuyButtonClick);
        }

        protected virtual void OnBuyButtonClick()
        {
            OnBuy?.Invoke(_id, 1);
        }

        public override void Dispose()
        {
            _buyButton.onClick.RemoveListener(OnBuyButtonClick);
        }
    }

    public readonly struct BuyItemViewData
    {
        public readonly string Id;
        public readonly string Title;
        public readonly string Description;

        public readonly Sprite Icon;
        public readonly int Price;

        public readonly Color IconColor;
        public readonly Color TitleColor;
        public readonly Color OutlineColor;

        public BuyItemViewData(
            string id, Sprite icon, string title, string description, int price, 
            Color iconColor, Color titleColor, Color outlineColor)
        {
            Id = id;
            Icon = icon;
            Title = title;
            Description = description;
            Price = price;
            IconColor = iconColor;
            TitleColor = titleColor;
            OutlineColor = outlineColor;
        }

        public static BuyItemViewData From(ItemData data, int price)
        {
            var colorByRarity = GlobalData.GetColorByRarity(data.Rarity);

            return new BuyItemViewData(
                id: data.Id,
                icon: data.Icon,
                title: data.Name,
                description: data.Description,
                price: price,
                iconColor: data.IconColor,
                titleColor: colorByRarity,
                outlineColor: colorByRarity
            );
        }
    };
}
