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

      Vector4 FindOrCreate(string name)
      {
         Vector4 d;
         if (entries.TryGetValue(name, out d))
         {
            return d;
         }
         entries.Add(name, d);
         return d;
      }

      public override void PrepareFrame(Playable playable, FrameData info)
      {
         entries.Clear();
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
               Vector4 d = FindOrCreate(input.config.shaderParamName);
               d += input.value * weight;
               entries[input.config.shaderParamName] = d;
            }
         }

         var enumerator = entries.GetEnumerator();
         while( enumerator.MoveNext() )
         {
            var key = enumerator.Current.Key;
            var d = enumerator.Current.Value;
            Shader.SetGlobalVector(key, d);
         }
      }
   }
}

#endif