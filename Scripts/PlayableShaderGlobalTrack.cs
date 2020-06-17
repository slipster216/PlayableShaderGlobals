#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Collections.Generic;

namespace PlayableShaderGlobal
{
   [TrackColor(0.5f, 0.5f, 0.8f)]
   [TrackMediaType(TimelineAsset.MediaType.Script)]
   [TrackClipType(typeof(PlayableShaderGlobalClip))]
   public class PlayableShaderGlobalTrack : TrackAsset
   {

      public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
      {
         foreach (var c in GetClips())
         {
            PlayableShaderGlobalClip tr = (PlayableShaderGlobalClip)c.asset;
            if (tr != null && tr.data != null && tr.data.config != null)
            {
               c.displayName = tr.data.config.displayName;
            }
            else
            {
               c.displayName = "Shader Global";
            }
         }
         return ScriptPlayable<PlayableShaderGlobalBehaviour>.Create(graph, inputCount);
      }
   }
}
#endif