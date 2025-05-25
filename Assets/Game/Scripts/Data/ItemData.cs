using UnityEngine;

namespace AzulonTest.Data
{

    [CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/Items/Item Data")]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public ItemRarity Rarity { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public Color IconColor { get; private set; }
        [field: SerializeField, TextArea(2, 5)] public string Description { get; private set; }
    }
}