using BubbleSO;
using BubbleSOEditor;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BubbleScriptable), true)]
public class SubAssetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var scriptableBubble = (BubbleScriptable)target;
        var serializedBubble = new SerializedObject(scriptableBubble);

        serializedBubble.Update();

        var fields = scriptableBubble.GetType()
            .GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

        foreach (var field in fields)
        {
            if (System.Attribute.IsDefined(field, typeof(ChildBubbleAttribute)))
            {
                var property = serializedBubble.FindProperty(field.Name);
                if (property == null)
                {
                    Debug.LogWarning($"Property for field '{field.Name}' not found.");
                    continue;
                }

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(property, new GUIContent(ObjectNames.NicifyVariableName(field.Name)));

                if (scriptableBubble.BubbleVariables.ContainsKey(field.Name))
                {
                    if (GUILayout.Button("X", GUILayout.Width(30)))
                    {
                        Undo.RecordObject(scriptableBubble, "Remove Sub-Asset");
                        AssetDatabase.RemoveObjectFromAsset(scriptableBubble.BubbleVariables[field.Name]);
                        scriptableBubble.BubbleVariables.Remove(field.Name);
                        AssetDatabase.SaveAssets();
                    }
                }
                else
                {
                    if (GUILayout.Button($"Create {field.FieldType.Name}", GUILayout.Width(100)))
                    {
                        var newSubAsset = CreateInstance<BubbleVariable>();
                        newSubAsset.VariableType = BubbleEditorUtils.GetBubbleVariableTypeFromFieldType(field.FieldType);
                        newSubAsset.name = field.Name;

                        scriptableBubble.BubbleVariables.Add(field.Name, newSubAsset);
                        AssetDatabase.AddObjectToAsset(newSubAsset, scriptableBubble);
                        AssetDatabase.SaveAssets();

                        property.objectReferenceValue = newSubAsset;
                    }
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        serializedBubble.ApplyModifiedProperties();
    }
}