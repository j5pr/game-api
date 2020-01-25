using GameAPI.Namespacing;
using System;

namespace GameAPI.Items
{
    [Serializable]
    public class Item
    {
        public string Name;
        public Material Material;
        public int Amount;

        public Key Key => Material.Key;
        public string Id => Key.Id;

        public Item(Material material, string? name = null, int quantity = 1)
        {
            Name = name ?? material.Name;
            Material = material;
            Amount = quantity;
        }
    }
}
