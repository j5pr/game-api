using GameAPI.Namespacing;
using UnityEditor;
using UnityEngine;

namespace GameAPI.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(Key))]
    public class KeyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            float margin = 8f;
            float size = (position.width - margin) / 2;

            Rect space = new Rect(position.x, position.y, size, position.height);
            Rect value = new Rect(position.x + size + margin, position.y, size, position.height);

            EditorGUI.PropertyField(space, property.FindPropertyRelative("Namespace"), GUIContent.none);
            EditorGUI.PropertyField(value, property.FindPropertyRelative("Value"), GUIContent.none);

            EditorGUI.EndProperty();
        }
    }
}