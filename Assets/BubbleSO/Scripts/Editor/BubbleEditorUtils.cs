#if UNITY_EDITOR
using System;
using BubbleSO;
using UnityEditor;
using UnityEngine;

namespace BubbleSOEditor
{
    public class BubbleEditorUtils : Editor
    {
        public static T DrawFieldForType<T>(T value, string label)
        {
            if (typeof(T) == typeof(float))
                return (T)(object)EditorGUILayout.FloatField(label, (float)(object)value);
            if (typeof(T) == typeof(int))
                return (T)(object)EditorGUILayout.IntField(label, (int)(object)value);
            if (typeof(T) == typeof(bool))
                return (T)(object)EditorGUILayout.Toggle(label, (bool)(object)value);
            if (typeof(T) == typeof(string))
                return (T)(object)EditorGUILayout.TextField(label, (string)(object)value);
            if (typeof(T) == typeof(Vector2))
                return (T)(object)EditorGUILayout.Vector2Field(label, (Vector2)(object)value);
            if (typeof(T) == typeof(Vector3))
                return (T)(object)EditorGUILayout.Vector3Field(label, (Vector3)(object)value);
            if (typeof(T) == typeof(GameObject))
                return (T)(object)EditorGUILayout.ObjectField(label, (GameObject)(object)value, typeof(GameObject), true);

            EditorGUILayout.LabelField(label, "Unsupported type");
            return value;
        }
        
        public static object DrawFieldForType(Type fieldType, object value, string label)
        {
            if (fieldType == typeof(float))
                return EditorGUILayout.FloatField(label, value != null ? (float)value : 0f);
            if (fieldType == typeof(int))
                return EditorGUILayout.IntField(label, value != null ? (int)value : 0);
            if (fieldType == typeof(bool))
                return EditorGUILayout.Toggle(label, value != null && (bool)value);
            if (fieldType == typeof(string))
                return EditorGUILayout.TextField(label, value != null ? (string)value : "");
            if (fieldType == typeof(Vector2))
                return EditorGUILayout.Vector2Field(label, value != null ? (Vector2)value : Vector2.zero);
            if (fieldType == typeof(Vector3))
                return EditorGUILayout.Vector3Field(label, value != null ? (Vector3)value : Vector3.zero);
            if (fieldType == typeof(GameObject))
                return EditorGUILayout.ObjectField(label, value as GameObject, typeof(GameObject), true);

            EditorGUILayout.LabelField(label, "Unsupported type");
            return value;
        }

        public static BubbleVariableType GetBubbleVariableTypeFromFieldType(Type fieldType)
        {
            if (fieldType == typeof(float)) return BubbleVariableType.Float;
            if (fieldType == typeof(int)) return BubbleVariableType.Int;
            if (fieldType == typeof(string)) return BubbleVariableType.String;
            if (fieldType == typeof(bool)) return BubbleVariableType.Bool;
            if (fieldType == typeof(Vector2)) return BubbleVariableType.Vector2;
            if (fieldType == typeof(Vector3)) return BubbleVariableType.Vector3;
            if (fieldType == typeof(GameObject)) return BubbleVariableType.GameObject;

            return BubbleVariableType.None;
        }
    }
}
#endif