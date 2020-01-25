using UnityEngine;

namespace GameAPI.Editor.Attributes
{
    public class NameAttribute : PropertyAttribute
    {
        public readonly string DisplayName;

        public NameAttribute(string name) =>
            DisplayName = name;
    }
}