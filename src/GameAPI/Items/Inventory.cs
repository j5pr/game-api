using System.Collections.Generic;

namespace GameAPI.Items
{
    public class Inventory
    {
        private readonly Dictionary<Item, int> items = new Dictionary<Item, int>();

        public int MaxSize;

        public int this[Item i] => items[i];

        public Inventory(int maxSize = int.MaxValue)
        {
            MaxSize = maxSize;
        }

        public void Deposit(Item item, int amount)
        {
            if (items.ContainsKey(item))
                items[item] += amount;
            else
                items.Add(item, amount);
        }

        public void Withdraw(Item item, int amount)
        {

        }
    }
}
