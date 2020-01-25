using GameAPI.Editor.Attributes;
using UnityEditor;
using UnityEngine;

namespace GameAPI.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(NameAttribute))]
    public class NameDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) =>
            EditorGUI.PropertyField(position, property, new GUIContent((attribute as NameAttribute)!.DisplayName));
    }
}