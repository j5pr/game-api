using System.Linq;
using System.Collections.Generic;
using GameAPI.Namespacing;

namespace GameAPI.Items
{
    public class Inventory
    {
        private readonly List<Item> items = new List<Item>();

        public int MaxSize;

        public Item this[int i] =>
        
            items[i];
        public Item? this[Key k] =>
            items.FirstOrDefault(e => e.Key == k);

        public Item? this[Material m] =>
            this[m.Key];

        public Inventory(int maxSize = int.MaxValue) =>
            MaxSize = maxSize;

        public void Deposit(Item item, bool merge = true)
        {
            if (merge)
            {
                Item orig = items.FirstOrDefault(e => e.Key == item.Key);

                if (orig != null)
                {
                    orig.Amount += item.Amount;
                    return;
                }
            }

            if (items.Count + 1 >= MaxSize)
                throw new System.Exception("Cannot deposit item to full Inventory!");

            items.Add(item);
        }

        public Item? Withdraw(Material material) =>
            Withdraw(material.Key);

        public Item? Withdraw(Key k)
        {
            Item? i = this[k];

            if (i == null)
                return null;

            items.Remove(i);
            return i;
        }

        public bool Contians(Material material) =>
            Contains(material.Key);

        public bool Contains(Key k) =>
            items.Exists(e => e.Key == k);

        public List<Item> GetList() =>
            items;
    }
}
