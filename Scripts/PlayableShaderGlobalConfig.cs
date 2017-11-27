using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayableShaderGlobal", menuName = "PlayableShaderGlobal")]
public class PlayableShaderGlobalConfig : ScriptableObject 
{
   public enum ValueType
   {
      Float = 0,
      Vector2,
      Vector3,
      Vector4,
      MinMaxSlider,
      Color,
      Int,
   }

   public ValueType valueType = ValueType.Float;

   public string shaderParamName;
   public string displayName;

   public Vector2 range;

}
