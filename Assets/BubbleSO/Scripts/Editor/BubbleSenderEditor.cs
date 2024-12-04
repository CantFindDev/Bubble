#if UNITY_EDITOR
using BubbleSO;
using UnityEditor;
using UnityEngine;

namespace BubbleSOEditor
{
    [CustomEditor(typeof(BubbleEventSender))]
    public class BubbleSenderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var bubbleScript = (BubbleEventSender)target;

            bubbleScript.EventType = (BubbleEventType)EditorGUILayout.EnumFlagsField("Event Type", bubbleScript.EventType);
            if (bubbleScript.EventType == BubbleEventType.NoEvent) return;

            DrawSendEventSettings(bubbleScript);

            DrawEventSettings("Float Event Settings", bubbleScript.floatBubbleEventSettings, BubbleEventType.FloatEvent);
            DrawEventSettings("Int Event Settings", bubbleScript.intBubbleEventSettings, BubbleEventType.IntEvent);
            DrawEventSettings("Bool Event Settings", bubbleScript.boolBubbleEventSettings, BubbleEventType.BoolEvent);
            DrawEventSettings("String Event Settings", bubbleScript.stringBubbleEventSettings, BubbleEventType.StringEvent);
            DrawEventSettings("Vector2 Event Settings", bubbleScript.vector2BubbleEventSettings, BubbleEventType.Vector2Event);
            DrawEventSettings("Vector3 Event Settings", bubbleScript.vector3BubbleEventSettings, BubbleEventType.Vector3Event);
            DrawEventSettings("GameObject Event Settings", bubbleScript.gameObjectBubbleEventSettings, BubbleEventType.GameObjectEvent);
        }

        private void DrawSendEventSettings(BubbleEventSender bubbleScript)
        {
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Send Event", EditorStyles.boldLabel);
            bubbleScript.AutoEventType = (BubbleAutoEventType)EditorGUILayout.EnumPopup("Auto Send Event Type", bubbleScript.AutoEventType);
            if (bubbleScript.AutoEventType == BubbleAutoEventType.SendTimer && !bubbleScript.isAutoEventSent)
            {
                bubbleScript.sendAfterSeconds = EditorGUILayout.FloatField("Send After Seconds", bubbleScript.sendAfterSeconds);
            }

            if (GUILayout.Button("Trigger Event", EditorStyles.miniButton))
            {
                bubbleScript.SendEvent();
            }
        }

        private void DrawEventSettings<T>(string label, BubbleEventSettings<T> settings, BubbleEventType type)
        {
            if (!((BubbleEventSender)target).IsEventTypeActive(type)) return;
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);

            settings.BubbleEvent = (BubbleEvent)EditorGUILayout.ObjectField($"{label} Event", settings.BubbleEvent, typeof(BubbleEvent), true);
            settings.BubbleEventValue = BubbleEditorUtils.DrawFieldForType(settings.BubbleEventValue, "Value");
        }
    }
}
#endif