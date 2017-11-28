#if UNITY_2017_1_OR_NEWER
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

namespace PlayableShaderGlobal
{
   [CustomPropertyDrawer(typeof(PlayableShaderGlobalBehaviour))]
   public class PlayableShaderGlobalBehaviourDrawer : PropertyDrawer
   {
      public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
      {
         int fieldCount = 2;
         return fieldCount*EditorGUIUtility.singleLineHeight;
      }

      public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
      {
         SerializedProperty config = property.FindPropertyRelative("config");
         Rect rect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
         EditorGUI.PropertyField(rect, config);

         PlayableShaderGlobalConfig cfg = (PlayableShaderGlobalConfig)config.objectReferenceValue;
         if (cfg != null)
         {
            SerializedProperty value = property.FindPropertyRelative("value");
            rect.y += EditorGUIUtility.singleLineHeight;
            switch (cfg.valueType)
            {
               case PlayableShaderGlobalConfig.ValueType.Float:
                  {
                     Vector4 v = value.vector4Value;
                     if (cfg.range.x != cfg.range.y)
                     {
                        v.x = EditorGUI.Slider(rect, cfg.displayName, v.x, cfg.range.x, cfg.range.y);
                     }
                     else
                     {
                        v.x = EditorGUI.FloatField(rect, cfg.displayName, v.x);
                     }
                     value.vector4Value = v;
                     break;
                  }
               case PlayableShaderGlobalConfig.ValueType.Vector2:
                  {
                     Vector2 v = value.vector4Value;
                     v = EditorGUI.Vector2Field(rect, cfg.displayName, v);
                     value.vector4Value = v;
                     break;
                  }
               case PlayableShaderGlobalConfig.ValueType.Vector3:
                  {
                     Vector3 v = value.vector4Value;
                     v = EditorGUI.Vector3Field(rect, cfg.displayName, v);
                     value.vector4Value = v;
                     break;
                  }
               case PlayableShaderGlobalConfig.ValueType.Vector4:
                  {
                     Vector4 v = value.vector4Value;
                     v = EditorGUI.Vector4Field(rect, cfg.displayName, v);
                     value.vector4Value = v;
                     break;
                  }
               case PlayableShaderGlobalConfig.ValueType.Color:
                  {
                     Vector4 v = value.vector4Value;
                     v = EditorGUI.ColorField(rect, cfg.displayName, v);
                     value.vector4Value = v;
                     break;
                  }
               case PlayableShaderGlobalConfig.ValueType.MinMaxSlider:
                  {
                     Vector2 v = value.vector2Value;
                     if (cfg.range.x != cfg.range.y)
                     {
                        EditorGUI.MinMaxSlider(rect, cfg.displayName, ref v.x, ref v.y, cfg.range.x, cfg.range.y);
                     }
                     else
                     {
                        v = EditorGUI.Vector2Field(rect, cfg.displayName, v);
                     }
                     value.vector2Value = v;
                     break;
                  }
               case PlayableShaderGlobalConfig.ValueType.Int:
                  {
                     Vector4 v = value.vector4Value;
                     if (cfg.range.x != cfg.range.y)
                     {
                        EditorGUI.IntSlider(rect, cfg.displayName, (int)v.x, (int)cfg.range.x, (int)cfg.range.y);
                     }
                     else
                     {
                        EditorGUI.IntField(rect, cfg.displayName, (int)v.x);
                     }
                     value.vector4Value = v;
                     break;
                  }
            }
         }

      }
   }
}
#endif