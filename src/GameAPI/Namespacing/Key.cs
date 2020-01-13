#pragma warning disable CS0660, CS0661

namespace GameAPI.Namespacing
{
    public partial struct Key
    {
        public string Namespace;
        public string Value;
        public string Id => $"{Namespace}:{Value}";

        public override string ToString() =>
            $"Key {{\n  Namespace = {Namespace},\n  Value = {Value},\n  Id = {Id}\n}}";
    }

    public partial struct Key
    {
        public static explicit operator string(Key key) =>
            key.Id;

        public static implicit operator Key(string value) =>
            new Key {
                Namespace = value.Split(':')[0],
                Value = value.Split(':')[1]
            };

        public static implicit operator Key((string, string) value) =>
            new Key {
                Namespace = value.Item1,
                Value = value.Item2
            };

        public static bool operator ==(Key a, Key b) =>
            a.Namespace == b.Namespace && a.Value == b.Value;

        public static bool operator !=(Key a, Key b) =>
            !(a == b);
    }
}
