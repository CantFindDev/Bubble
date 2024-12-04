using System;
using System.Collections;
using UnityEngine;

namespace BubbleSO
{
    [Flags]
    public enum BubbleEventType
    {
        NoEvent = 0x0, //00000
        FloatEvent = 0x1, //00001
        IntEvent = 0x2,//00010
        StringEvent = 0x4,//00100
        BoolEvent = 0x8,//01000
        Vector2Event = 0x10,//10000
        Vector3Event = 0x20,//100000
        GameObjectEvent = 0x40,//1000000
    }

    public enum BubbleAutoEventType
    {
        DontSendAutoEvent,
        SendOnAwake,
        SendOnStart,
        SendTimer,
    }
    
    public class BubbleEventSender : MonoBehaviour
    {
        public BubbleEventType EventType;
        public bool IsEventTypeActive(BubbleEventType type) => (EventType & type) != 0;
        public BubbleAutoEventType AutoEventType;

        public float sendAfterSeconds = 0f;
        public bool isAutoEventSent;

        public BubbleEventSettings<float> floatBubbleEventSettings;
        public BubbleEventSettings<int> intBubbleEventSettings;
        public BubbleEventSettings<bool> boolBubbleEventSettings;
        public BubbleEventSettings<string> stringBubbleEventSettings;
        public BubbleEventSettings<Vector2> vector2BubbleEventSettings;
        public BubbleEventSettings<Vector3> vector3BubbleEventSettings;
        public BubbleEventSettings<GameObject> gameObjectBubbleEventSettings;

        private void Awake()
        {
            HandleAutoEvent(BubbleAutoEventType.SendOnAwake);
        }

        private void Start()
        {
            HandleAutoEvent(BubbleAutoEventType.SendOnStart);

            if (AutoEventType == BubbleAutoEventType.SendTimer && !isAutoEventSent && sendAfterSeconds > 0)
            {
                StartCoroutine(SendEventAfterSeconds(sendAfterSeconds));
            }
        }

        private void HandleAutoEvent(BubbleAutoEventType triggerType)
        {
            if (AutoEventType == triggerType && !isAutoEventSent)
            {
                SendEvent();
                isAutoEventSent = true;
            }
        }

        private IEnumerator SendEventAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            SendEvent();
            isAutoEventSent = true;
        }

        public void SendEvent()
        {
            TryRaiseEvent(floatBubbleEventSettings, BubbleEventType.FloatEvent);
            TryRaiseEvent(intBubbleEventSettings, BubbleEventType.IntEvent);
            TryRaiseEvent(boolBubbleEventSettings, BubbleEventType.BoolEvent);
            TryRaiseEvent(stringBubbleEventSettings, BubbleEventType.StringEvent);
            TryRaiseEvent(vector2BubbleEventSettings, BubbleEventType.Vector2Event);
            TryRaiseEvent(vector3BubbleEventSettings, BubbleEventType.Vector3Event);
            TryRaiseEvent(gameObjectBubbleEventSettings, BubbleEventType.GameObjectEvent);
        }

        private void TryRaiseEvent<T>(BubbleEventSettings<T> settings, BubbleEventType type)
        {
            if (settings?.BubbleEvent == null) return;
            if ((EventType & type) != 0)
            {
                settings.BubbleEvent.RaiseEvent(settings.BubbleEventValue);
            }
        }
    }

    [Serializable]
    public class BubbleEventSettings<T>
    {
        public BubbleEvent BubbleEvent;
        public T BubbleEventValue;
    }
}
