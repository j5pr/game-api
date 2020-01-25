using GameAPI.Editor.Attributes;
using GameAPI.Editor.Util;
using UnityEditor;
using UnityEngine;

namespace GameAPI.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            EditorGUI.LabelField(position, property.GetValue()?.ToString() ?? "null");

            EditorGUI.EndProperty();
        }
    }
}