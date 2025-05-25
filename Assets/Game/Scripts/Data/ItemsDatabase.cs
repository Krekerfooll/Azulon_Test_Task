using System.Collections.Generic;
using UnityEngine;

namespace AzulonTest.Data
{
    [CreateAssetMenu(fileName = "ItemsDatabase", menuName = "Scriptable Objects/Items/Items Database")]
    public class ItemsDatabase : ScriptableObject
    {
        [field: SerializeField] public List<ItemData> AllItems { get; private set; }

        private Dictionary<string, ItemData> _cachedItems;

        public void Init()
        {
            _cachedItems = new Dictionary<string, ItemData>();
            foreach (var item in AllItems)
            {
                _cachedItems[item.Id] = item;
            }
        }

        public ItemData GetById(string id)
        {
            _cachedItems ??= new Dictionary<string, ItemData>();
            if (_cachedItems.Count == 0) Init();
            return _cachedItems.TryGetValue(id, out var data) ? data : null;
        }
    }
}
