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

        public Item(Material material, int quantity = 1, string? name = null)
        {
            Name = name ?? material.Name;
            Material = material;
            Amount = quantity;
        }

        public Item? Split(int amount) {
            if (amount > Amount)
                return null;

            Remove(amount);
            return new Item(Material, amount, Name);
        }

        public Item Add(int amount) {
            Amount += amount;
            return this;
        }

        public Item Remove(int amount) {
            Amount -= amount;
            return this;
        }
    }
}
