using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BubbleSO
{
#if UNITY_EDITOR
    [CreateAssetMenu(fileName = "Bubble Scriptable", menuName = "BubbleSO/Bubble Scriptable")]
    [Icon("Assets/BubbleSO/Icons/BubbleScriptableObj.png")]
#endif
    public class BubbleScriptable : ScriptableObject
    {
        public Dictionary<string, Object> BubbleVariables = new Dictionary<string, Object>();
    }
}