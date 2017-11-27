#if UNITY_2017_1_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace PlayableShaderGlobal
{
   [Serializable]
   public class PlayableShaderGlobalClip : PlayableAsset, ITimelineClipAsset
   {
      public PlayableShaderGlobalBehaviour data = new PlayableShaderGlobalBehaviour ();

      public ClipCaps clipCaps
      {
         get { return ClipCaps.Blending; }
      }

      public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
      {
         return ScriptPlayable<PlayableShaderGlobalBehaviour>.Create(graph, data);
      }
   }
}
#endif