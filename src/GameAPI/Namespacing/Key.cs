using System;

namespace GameAPI.Namespacing
{
    [Serializable]
    public partial struct Key : IEquatable<Key>
    {
        public string Namespace;
        public string Value;
        public string Id => $"{Namespace}:{Value}";

        public Key(string space, string value)
        {
            Namespace = space;
            Value = value;
        }

        public override string ToString() =>
            Id;

        public bool Equals(Key key) =>
            key.Namespace == Namespace &&
            key.Value == Value;

        public override bool Equals(object obj) =>
            obj != null && obj is Key && Equals((Key) obj);

        public override int GetHashCode() =>
            new { Namespace, Value }.GetHashCode();
    }

    public partial struct Key
    {
        public static Key Unique(string space = "") =>
            new Key(space, Guid.NewGuid().ToString());
    }

    public partial struct Key
    {
        public static explicit operator string(Key key) =>
            key.Id;

        public static implicit operator Key(string value) {
            string[] parts = value.Split(':', 2);
            return new Key(parts[0], parts[1]);
        }

        public static implicit operator Key((string Namespace, string Value) value) =>
            new Key(value.Namespace, value.Value);

        public static bool operator ==(Key a, Key b) =>
            a.Equals(b);

        public static bool operator !=(Key a, Key b) =>
            !a.Equals(b);
    }
}
