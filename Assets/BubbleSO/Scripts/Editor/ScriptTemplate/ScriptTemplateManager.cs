using UnityEditor;

public class ScriptTemplateManager
{
	[MenuItem("Assets/Create/BubbleSO/Bubble Listener Script", false, 80)]
	private static void CreateBubbleListenerScript()
	{
		string templatePath = "Assets/BubbleSO/Scripts/Editor/ScriptTemplate/BubbleListenerTemplate.txt";
		string defaultName = "NewBubbleEventListener.cs";

		ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, defaultName);
	}
    
    [MenuItem("Assets/Create/BubbleSO/Custom Bubble Scriptable", false, 80)]
    private static void CreateBubbleScriptableScript()
    {
        string templatePath = "Assets/BubbleSO/Scripts/Editor/ScriptTemplate/BubbleScriptableTemplate.txt";
        string defaultName = "NewCustomBubble.cs";

        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, defaultName);
    }
}
