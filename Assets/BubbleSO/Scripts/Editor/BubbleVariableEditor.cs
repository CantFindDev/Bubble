#if UNITY_EDITOR
using BubbleSO;
using UnityEditor;
using UnityEngine;

    [CustomEditor(typeof(VariableBubble))]
    public class BubbleVariableEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            VariableBubble BubbleVarScript = (VariableBubble)target;

            BubbleVarScript.VariableType = (BubbleVariableType)EditorGUILayout.EnumFlagsField("Variable Type", BubbleVarScript.VariableType);
            if (BubbleVarScript.VariableType == BubbleVariableType.None) return;
            
            DrawEventSettings("Float Settings", BubbleVarScript.FloatVariable, BubbleVariableType.Float);
            DrawEventSettings("Int Settings", BubbleVarScript.IntVariable, BubbleVariableType.Int);
            DrawEventSettings("Bool Settings", BubbleVarScript.BoolVariable, BubbleVariableType.Bool);
            DrawEventSettings("String Settings", BubbleVarScript.StringVariable, BubbleVariableType.String);
            DrawEventSettings("Vector2 Settings", BubbleVarScript.Vector2Variable, BubbleVariableType.Vector2);
            DrawEventSettings("Vector3 Settings", BubbleVarScript.Vector3Variable, BubbleVariableType.Vector3);
            DrawEventSettings("GameObject Settings", BubbleVarScript.GameObjectVariable, BubbleVariableType.GameObject);
        }

        private void DrawEventSettings<T>(string label, VariableBubble.BubbleVariableSetings<T> settings, BubbleVariableType type)
        {
            if (!((VariableBubble)target).IsVariableTypeActive(type)) return;
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
            
            settings.VariableName = EditorGUILayout.TextField("Variable Name", settings.VariableName);
            settings.BubbleVariable = DrawFieldForType(settings.BubbleVariable, "Value");
        }
        
        private T DrawFieldForType<T>(T value, string label)
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
    }
    #endif