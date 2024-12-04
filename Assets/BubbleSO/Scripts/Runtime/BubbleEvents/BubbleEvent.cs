using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace BubbleSO
{
#if UNITY_EDITOR
    [CreateAssetMenu(fileName = "Bubble Event", menuName = "BubbleSO/Bubble Event")]
    [Icon("Assets/BubbleSO/Icons/BubbleScriptableEvent.png")]
    #endif
    public class BubbleEvent : ScriptableObject
    {
        private Dictionary<Type, Delegate> eventActions = new();

        public void RegisterListener<T>(UnityAction<T> listener)
        {
            if (eventActions.TryGetValue(typeof(T), out var existingAction))
            {
                eventActions[typeof(T)] = Delegate.Combine(existingAction, listener);
            }
            else
            {
                eventActions[typeof(T)] = listener;
            }
        }

        public void UnregisterListener<T>(UnityAction<T> listener)
        {
            if (eventActions.TryGetValue(typeof(T), out var existingAction))
            {
                var newAction = Delegate.Remove(existingAction, listener);
                if (newAction == null)
                    eventActions.Remove(typeof(T));
                else
                    eventActions[typeof(T)] = newAction;
            }
        }

        public void RaiseEvent<T>(T value)
        {
            if (eventActions.TryGetValue(typeof(T), out var action))
            {
                ((UnityAction<T>)action)?.Invoke(value);
            }
        }
    }
}