#if UNITY_EDITOR
using BubbleSO;
using UnityEditor;

namespace BubbleSOEditor
{
    [CustomEditor(typeof(BubbleVariable))]
    public class BubbleVariableEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            BubbleVariable varScript = (BubbleVariable)target;

            varScript.VariableType = (BubbleVariableType)EditorGUILayout.EnumFlagsField("Variable Type", varScript.VariableType);
            if (varScript.VariableType == BubbleVariableType.None) return;

            DrawEventSettings("Float Settings", varScript.FloatVariable, BubbleVariableType.Float);
            DrawEventSettings("Int Settings", varScript.IntVariable, BubbleVariableType.Int);
            DrawEventSettings("Bool Settings", varScript.BoolVariable, BubbleVariableType.Bool);
            DrawEventSettings("String Settings", varScript.StringVariable, BubbleVariableType.String);
            DrawEventSettings("Vector2 Settings", varScript.Vector2Variable, BubbleVariableType.Vector2);
            DrawEventSettings("Vector3 Settings", varScript.Vector3Variable, BubbleVariableType.Vector3);
            DrawEventSettings("GameObject Settings", varScript.GameObjectVariable, BubbleVariableType.GameObject);
        }

        private void DrawEventSettings<T>(string label, BubbleVariable.BubbleVariableSetings<T> settings, BubbleVariableType type)
        {
            BubbleVariable varScript = (BubbleVariable)target;
            if (!varScript.IsVariableTypeActive(type)) return;

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);

            settings.VariableName = EditorGUILayout.TextField("Variable Name", settings.VariableName);
            settings.BubbleVariable = BubbleEditorUtils.DrawFieldForType(settings.BubbleVariable, "Value");
        }
    }
}
#endif