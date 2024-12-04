using UnityEngine;
using BubbleSO;

[CreateAssetMenu(fileName = "SubAssetTest Bubble", menuName = "BubbleSO/Custom/SubAssetTest")]
public class SubAssetTest : BubbleScriptable
{
    [ChildBubble]
    public float variableBubble;
    
    [ChildBubble]
    public bool variableBubble2;
}
