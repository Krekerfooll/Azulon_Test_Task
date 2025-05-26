using AzulonTest.Data;
using AzulonTest.UI;
using UnityEngine;
using static AzulonTest.UI.UIController;

namespace AzulonTest.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ItemsDatabase _itemsDatabase;
        [SerializeField] private ShopDatabase _shopDatabase;
        [Space]
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private ShopView _shopView;
        [SerializeField] private BuyItemView _buyItemView;

        private UIController _uiController;
        private InventoryController _inventoryController;
        private ShopController _shopController;

        private void Start()
        {
            _uiController = new UIController();
            _inventoryController = new InventoryController();
            _shopController = new ShopController();

            _inventoryController.Init(_inventoryView, _itemsDatabase);
            _shopController.Init(_shopView, _buyItemView, _inventoryController, _itemsDatabase, _shopDatabase);

            _uiController.SetView(UIWindowType.MainMenu, _mainMenuView);
            _uiController.SetView(UIWindowType.Inventory, _inventoryView);
            _uiController.SetView(UIWindowType.Shop, _shopView);
            _uiController.SetView(UIWindowType.BuyConfirmation, _buyItemView);

            _uiController.Open(UIWindowType.MainMenu);
        }

        private void OnDestroy()
        {
            _mainMenuView.Dispose();
            _inventoryView.Dispose();
            _shopView.Dispose();
        }
    }
}
