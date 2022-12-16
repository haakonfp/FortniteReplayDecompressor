using FortniteReplayReader.Models.NetFieldExports;
using System.Collections.Generic;
using Unreal.Core.Models;

namespace FortniteReplayReader.Models;

public class QueuedPlayerPawn
{
	public uint ChannelId { get; internal set; }
	public PlayerPawnC PlayerPawn { get; internal set; }
	public List<NetDeltaUpdate> InventoryUpdates { get; private set; } = new List<NetDeltaUpdate>();
}
