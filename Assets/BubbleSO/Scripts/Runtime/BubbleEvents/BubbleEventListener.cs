using UnityEngine;
using System;
using System.Collections.Generic;

namespace BubbleSO
{
    public class BubbleEventListener : MonoBehaviour
    {
        public BubbleEvent ScriptableEvent;
        [SerializeField] [HideInInspector] public bool EnableDebugLogs = true;

        private readonly Dictionary<Type, Action<object>> eventHandlers = new();

        private void Awake()
        {
            eventHandlers.Add(typeof(float), value => OnFloatEventReceived((float)value));
            eventHandlers.Add(typeof(int), value => OnIntEventReceived((int)value));
            eventHandlers.Add(typeof(string), value => OnStringEventReceived((string)value));
            eventHandlers.Add(typeof(bool), value => OnBoolEventReceived((bool)value));
            eventHandlers.Add(typeof(Vector2), value => OnVector2EventReceived((Vector2)value));
            eventHandlers.Add(typeof(Vector3), value => OnVector3EventReceived((Vector3)value));
            eventHandlers.Add(typeof(GameObject), value => OnGameObjectEventReceived((GameObject)value));
        }

        private void OnEnable()
        {
            if (ScriptableEvent == null) return;
            
            ScriptableEvent.RegisterListener<float>(value => HandleEvent(value));
            ScriptableEvent.RegisterListener<int>(value => HandleEvent(value));
            ScriptableEvent.RegisterListener<string>(value => HandleEvent(value));
            ScriptableEvent.RegisterListener<bool>(value => HandleEvent(value));
            ScriptableEvent.RegisterListener<Vector2>(value => HandleEvent(value));
            ScriptableEvent.RegisterListener<Vector3>(value => HandleEvent(value));
            ScriptableEvent.RegisterListener<GameObject>(value => HandleEvent(value));
        }

        private void OnDisable()
        {
            if (ScriptableEvent == null) return;
            
            ScriptableEvent.UnregisterListener<float>(value => HandleEvent(value));
            ScriptableEvent.UnregisterListener<int>(value => HandleEvent(value));
            ScriptableEvent.UnregisterListener<string>(value => HandleEvent(value));
            ScriptableEvent.UnregisterListener<bool>(value => HandleEvent(value));
            ScriptableEvent.UnregisterListener<Vector2>(value => HandleEvent(value));
            ScriptableEvent.UnregisterListener<Vector3>(value => HandleEvent(value));
            ScriptableEvent.UnregisterListener<GameObject>(value => HandleEvent(value));
        }

        private void HandleEvent<T>(T value)
        {
            if (eventHandlers.TryGetValue(typeof(T), out var handler))
            {
                handler(value);
            }
            else
            {
                Debug.LogWarning($"Unhandled event type: {typeof(T)}");
            }
        }
        
        private void EventLog(string message)
        {
            if (EnableDebugLogs)
            {
                Debug.Log(message);
            }
        }
        
        public virtual void OnFloatEventReceived(float value) => EventLog("Float Event Received: " + value);
        public virtual void OnIntEventReceived(int value) => EventLog("Int Event Received: " + value);
        public virtual void OnStringEventReceived(string value) => EventLog("String Event Received: " + value);
        public virtual void OnBoolEventReceived(bool value) => EventLog("Bool Event Received: " + value);
        public virtual void OnVector2EventReceived(Vector2 value) => EventLog("Vector2 Event Received: " + value);
        public virtual void OnVector3EventReceived(Vector3 value) => EventLog("Vector3 Event Received: " + value);
        public virtual void OnGameObjectEventReceived(GameObject value) => EventLog("GameObject Event Received: " + value);
    }
}
