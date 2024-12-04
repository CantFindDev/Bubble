#if UNITY_EDITOR
using BubbleSO;
using UnityEditor;
using UnityEngine;

    [CustomEditor(typeof(BubbleEventSender))]
    public class BubbleSenderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            BubbleEventSender BubbleScript = (BubbleEventSender)target;

            BubbleScript.EventType = (BubbleEventType)EditorGUILayout.EnumFlagsField("Event Type", BubbleScript.EventType);
            if (BubbleScript.EventType == BubbleEventType.NoEvent) return;
           
            DrawSendEventSettings(BubbleScript);
            
            DrawEventSettings("Float Event Settings", BubbleScript.floatBubbleEventSettings, BubbleEventType.FloatEvent);
            DrawEventSettings("Int Event Settings", BubbleScript.intBubbleEventSettings, BubbleEventType.IntEvent);
            DrawEventSettings("Bool Event Settings", BubbleScript.boolBubbleEventSettings, BubbleEventType.BoolEvent);
            DrawEventSettings("String Event Settings", BubbleScript.stringBubbleEventSettings, BubbleEventType.StringEvent);
            DrawEventSettings("Vector2 Event Settings", BubbleScript.vector2BubbleEventSettings, BubbleEventType.Vector2Event);
            DrawEventSettings("Vector3 Event Settings", BubbleScript.vector3BubbleEventSettings, BubbleEventType.Vector3Event);
            DrawEventSettings("GameObject Event Settings", BubbleScript.gameObjectBubbleEventSettings, BubbleEventType.GameObjectEvent);
        }

        private void DrawSendEventSettings(BubbleEventSender BubbleScript)
        {
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Send Event", EditorStyles.boldLabel);
            BubbleScript.AutoEventType = (BubbleAutoEventType)EditorGUILayout.EnumPopup("Auto Send Event Type", BubbleScript.AutoEventType);
            if (BubbleScript.AutoEventType == BubbleAutoEventType.SendTimer && !BubbleScript.isAutoEventSent)
            {
                BubbleScript.sendAfterSeconds = EditorGUILayout.FloatField("Send After Seconds", BubbleScript.sendAfterSeconds);
            }
            if (GUILayout.Button("Trigger Event", EditorStyles.miniButton))
            {
                BubbleScript.SendEvent();
            }
        }

        private void DrawEventSettings<T>(string label, BubbleEventSettings<T> settings, BubbleEventType type)
        {
            if (!((BubbleEventSender)target).IsEventTypeActive(type)) return;
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);

            settings.BubbleEvent = (BubbleEvent)EditorGUILayout.ObjectField($"{label} Event", settings.BubbleEvent, typeof(BubbleEvent), true);
            settings.BubbleEventValue = DrawFieldForType(settings.BubbleEventValue, "Value");
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