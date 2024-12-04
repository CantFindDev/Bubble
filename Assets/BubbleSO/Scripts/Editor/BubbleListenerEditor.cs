#if UNITY_EDITOR
using BubbleSO;
using UnityEditor;

[CustomEditor(typeof(BubbleEventListener))]
class BubbleListenerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        BubbleEventListener script = (BubbleEventListener)target;
        script.ScriptableEvent = (BubbleEvent)EditorGUILayout.ObjectField("Scriptable Event", script.ScriptableEvent, typeof(BubbleEvent), true);
        script.EnableDebugLogs = EditorGUILayout.Toggle("Enable Debug Logs", script.EnableDebugLogs);
    }
}
#endif
