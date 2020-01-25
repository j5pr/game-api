using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

namespace GameAPI.Editor.Util
{
    public static class SerializedPropertyExtension
    {
        public static object? GetValue(this SerializedProperty property)
        {
            object obj = property.serializedObject.targetObject;
            FieldInfo? info;

            foreach (string path in property.propertyPath.Split2("."))
            {
                info = obj.GetType().GetField(path);
                obj = info.GetValue(obj);
            }

            return obj;
        }

        // Unity doesn't like string.Split() from prebuilt assemblies?
        private static IEnumerable<string> Split2(this string source, string delim)
        {
            int oldIndex = 0, newIndex;
            
            while ((newIndex = source.IndexOf(delim, oldIndex)) != -1)
            {
                yield return source.Substring(oldIndex, newIndex - oldIndex);

                oldIndex = newIndex + delim.Length;
            }

            yield return source.Substring(oldIndex);
        }
    }
}