#if UNITY_EDITOR
using BubbleSO;
using UnityEditor;

namespace BubbleSOEditor
{
    [CustomEditor(typeof(BubbleScriptable))]
    public class BubbleScriptableEditor : Editor
    {
        public override void OnInspectorGUI()
        {

        }
    }
}
#endif