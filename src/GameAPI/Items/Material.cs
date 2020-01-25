using System;
using System.Collections.Generic;
using GameAPI.Namespacing;

namespace GameAPI.Items
{
    [Serializable]
    public partial class Material
    {
        public static Dictionary<Key, Material> Table = new Dictionary<Key, Material>();

        public readonly Key Key;
        public readonly string Name;

        public string Id => Key.Id;

        public Material(Key key, string name = "None")
        {
            Key = key;
            Name = name;

            Table.Add(key, this);
        }
    }

    public partial class Material
    {
        public static Material None = new Material("game:none", "None");
        public static Material Invalid = new Material("game:invalid", "Invalid Item");
        public static Material Unique => new Material(("unique", Guid.NewGuid().ToString()), "Unique Item");
    }
}
