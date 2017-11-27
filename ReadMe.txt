Playable Shader Globals

 This system makes is incredibly easy to add global shader properties to the Timeline in Unity via Scriptable Objects. Lets imagine you have wind strength, and many shaders and C# systems want to read the wind strength. In unity, you can easily set and get this value via c# code like so:

   Shader.SetGlobalFloat("gWindSpeed", 1.0f);

   float curWindSpeed = Shader.GetGlobalFloat("gWindSpeed");

And in a shader, you can use this value by NOT declaring a property for it, and just declaring the value itself:

   float gWindSpeed;

This allows you to easily write shaders and systems which interact with this value without having to manage materials and singletons. 

What Playable Shader Globals does is allow you to easily add these global shader values to timeline animations. To do so, install this repository and:

To create a new Global Property:

- Right click in the project view and select Create->PlayableShaderGlobal
- Name the Scriptable Object "Wind Speed"
- Set the value type (color, float, etc)
- Set the name of the shader parameters (gWindSpeed)
- Set the display name for use in Timeline ("Wind Speed")
- A range value is available to limit the range of float and min/max sliders

To use the Global Property in Timeline

- Add a "PlayableShaderGlobal->PlayableShaderGlobalTrack" track to timeline
- Select the "Wind Speed" config
- Set the wind speed value

  
