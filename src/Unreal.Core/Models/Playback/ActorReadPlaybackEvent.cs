namespace Unreal.Core.Models.Playback;

public class ActorReadPlaybackEvent : ReplayPlaybackEvent
{
	public Actor Actor { get; set; }
	public uint Channel { get; set; }
}
