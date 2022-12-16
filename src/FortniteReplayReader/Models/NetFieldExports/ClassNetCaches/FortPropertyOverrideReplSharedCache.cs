using Unreal.Core.Attributes;

namespace FortniteReplayReader.Models.NetFieldExports.ClassNetCaches;

[NetFieldExportRPC("FortPropertyOverrideReplShared_ClassNetCache")]
public class FortPropertyOverrideReplSharedCache
{
	[NetFieldExportRPCProperty("ReplOverrides", "/Script/FortniteGame.FortPropertyOverrideReplShared")]
	public object ReplOverrides { get; set; }
}
