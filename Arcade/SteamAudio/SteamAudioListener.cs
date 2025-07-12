using Arcade.SteamAudio.Processors;
using SteamAudio;
using Stride.Core;
using Stride.Engine;
using Stride.Engine.Design;

namespace Arcade.SteamAudio;
[DataContract]
[Display("Steam Audio Listener")]
[ComponentCategory("Audio")]
[DefaultEntityComponentProcessor(typeof(SteamAudioListenerProcessor), ExecutionMode = ExecutionMode.Runtime)]
public class SteamAudioListener : EntityComponent
{
	
}
