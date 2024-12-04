using BubbleSO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScriptableBubble), true)]
public class SubAssetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        var scriptableBubble = (ScriptableBubble)target;
        var SB = new SerializedObject(scriptableBubble);

        // Iterate over all fields with [SubAsset]
        var property = SB.GetIterator();
        while (property.NextVisible(true))
        {
            var field = scriptableBubble.GetType().GetField(property.name,
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Instance);

            if (field != null && System.Attribute.IsDefined(field, typeof(ChildBubbleAttribute)))
            {
                var subAsset = property.objectReferenceValue;

                if (subAsset == null)
                {
                    if (GUILayout.Button($"Create {field.FieldType.Name}"))
                    {
                 
                        // Create a new sub-asset
                        var newSubAsset = CreateInstance(field.FieldType);

                        // Add it as a sub-asset of the current ScriptableObject
                        newSubAsset.name = field.Name;
                        AssetDatabase.AddObjectToAsset(newSubAsset, scriptableBubble);
                        AssetDatabase.SaveAssets();

                        // Assign the created sub-asset
                        property.objectReferenceValue = newSubAsset;
                        SB.ApplyModifiedProperties();
                    }
                }
            }
        }
    }
}