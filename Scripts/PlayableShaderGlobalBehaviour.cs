#if UNITY_2017_1_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Collections.Generic;

namespace PlayableShaderGlobal
{
   [Serializable]
   public class PlayableShaderGlobalBehaviour : PlayableBehaviour
   {
      public PlayableShaderGlobalConfig config;
      public Vector4 value;

      Dictionary<string, Vector4> entries = new Dictionary<string, Vector4>(12);
      Dictionary<string, PlayableShaderGlobalConfig.ValueType> typemap = new Dictionary<string, PlayableShaderGlobalConfig.ValueType>(12);

      Vector4 FindOrCreate(string name, PlayableShaderGlobalConfig.ValueType valueType)
      {
         Vector4 d;
         if (entries.TryGetValue(name, out d))
         {
            return d;
         }
         entries.Add(name, d);
         typemap.Add(name, valueType);
         return d;
      }

      public override void PrepareFrame(Playable playable, FrameData info)
      {
         entries.Clear();
         typemap.Clear();
         base.PrepareFrame(playable, info);
      }

      public override void ProcessFrame(Playable playable, FrameData info, object playerData)
      {
         int inputCount = playable.GetInputCount();

         for (int i = 0; i < inputCount; i++)
         {
            float weight = playable.GetInputWeight(i);
            ScriptPlayable<PlayableShaderGlobalBehaviour> inputPlayable = (ScriptPlayable<PlayableShaderGlobalBehaviour>)playable.GetInput(i);
            PlayableShaderGlobalBehaviour input = inputPlayable.GetBehaviour();
            if (input.config != null)
            {
               Vector4 d = FindOrCreate(input.config.shaderParamName, input.config.valueType);
               d += input.value * weight;
               entries[input.config.shaderParamName] = d;
               typemap[input.config.shaderParamName] = input.config.valueType;
            }
         }

         var enumerator = entries.GetEnumerator();
         var typeEnumerator = typemap.GetEnumerator();
         while( enumerator.MoveNext() )
         {
            typeEnumerator.MoveNext();
            var key = enumerator.Current.Key;
            var d = enumerator.Current.Value;
            var tp = typeEnumerator.Current.Value;

            switch (tp)
            {
               case PlayableShaderGlobalConfig.ValueType.Float:
                  {
                     Shader.SetGlobalFloat(key, d.x); 
                     break;
                  }
               case PlayableShaderGlobalConfig.ValueType.Int:
                  {
                     Shader.SetGlobalInt(key, (int)d.x); 
                     break;
                  }
               default:
                  {
                     Shader.SetGlobalVector(key, d); 
                     break;
                  }
            }
            Shader.SetGlobalVector(key, d);
         }
      }
   }
}

#endif