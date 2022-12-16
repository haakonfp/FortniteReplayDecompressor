namespace Unreal.Core.Models.Playback;

public class NetDeltaPlaybackEvent : ReplayPlaybackEvent
{
	public NetDeltaUpdate DeltaUpdate { get; set; }
}
