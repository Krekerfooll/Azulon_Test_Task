using System;
using System.Collections.Generic;
using UnityEngine;

namespace AzulonTest.Data
{
    [CreateAssetMenu(fileName = "ShopDatabase", menuName = "Scriptable Objects/Shop/Shop Database")]
    public class ShopDatabase : ScriptableObject
    {
        [field: SerializeField] public List<ShopItemData> ShopItems { get; private set; }
    }

    [Serializable]
    public struct ShopItemData
    {
        public string ItemId;
        public int Price;
    }
}
