using UnityEngine;

namespace AzulonTest.Data
{
    public enum ItemRarity { Common, Rare, Epic, Legendary }

    public class GlobalData
    {
        public const string INVENTORY_DATA_SAVE = "INVENTORY_DATA_SAVE";

        public static readonly Color DEFAULT_BACKGROUND_COLOR = new Color32(55, 55, 55, 255);
        public static readonly Color DEFAULT_FOREGROUND_COLOR = new Color32(210, 210, 210, 255);

        public static readonly Color RARITY_COMMON = new Color32(166, 255, 127, 255);
        public static readonly Color RARITY_RARE = new Color32(127, 218, 255, 255);
        public static readonly Color RARITY_EPIC = new Color32(161, 127, 255, 255);
        public static readonly Color RARITY_LEGENDARY = new Color32(255, 219, 71, 255);

        public static Color GetColorByRarity(ItemRarity rarity)
        {
            return rarity switch
            {
                ItemRarity.Common => RARITY_COMMON,
                ItemRarity.Rare => RARITY_RARE,
                ItemRarity.Epic => RARITY_EPIC,
                ItemRarity.Legendary => RARITY_LEGENDARY,
                _ => DEFAULT_FOREGROUND_COLOR,
            };
        }
    }
}
