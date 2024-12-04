using System;
using UnityEngine;

namespace BubbleSO
{
   [Flags]
   public enum BubbleVariableType
   {
      None = 0x0, //00000
      Float = 0x1, //00001
      Int = 0x2,//00010
      String = 0x4,//00100
      Bool = 0x8,//01000
      Vector2 = 0x10,//10000
      Vector3 = 0x20,//100000
      GameObject = 0x40,//1000000
   }
   
#if UNITY_EDITOR
   [CreateAssetMenu(fileName = "Bubble Variable", menuName = "BubbleSO/Bubble Variable")]
   [Icon("Assets/BubbleSO/Icons/BubbleScriptableObj.png")]
#endif
   public class BubbleVariable : ScriptableObject
   {
      public BubbleVariableType VariableType;
      public bool IsVariableTypeActive(BubbleVariableType type) => (VariableType & type) != 0;
      
      public BubbleVariableSetings<float> FloatVariable;
      public BubbleVariableSetings<int> IntVariable;
      public BubbleVariableSetings<bool> BoolVariable;
      public BubbleVariableSetings<string> StringVariable;
      public BubbleVariableSetings<Vector2> Vector2Variable;
      public BubbleVariableSetings<Vector3> Vector3Variable;
      public BubbleVariableSetings<GameObject> GameObjectVariable;
      
      [Serializable]
      public class BubbleVariableSetings<T>
      {
         public string VariableName;
         public T BubbleVariable;
      }
   }
}