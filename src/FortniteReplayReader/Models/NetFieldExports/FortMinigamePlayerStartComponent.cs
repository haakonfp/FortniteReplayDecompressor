using Unreal.Core.Attributes;
using Unreal.Core.Contracts;
using Unreal.Core.Models;
using Unreal.Core.Models.Enums;

namespace FortniteReplayReader.Models.NetFieldExports
{
	[NetFieldExportGroup("/Script/FortniteGame.FortMinigamePlayerStartComponent", ParseType.Debug)]
	[RedirectPath("FortMinigamePlayerStart")]
	public class FortMinigamePlayerStartComponent : INetFieldExportGroup
	{
		[NetFieldExport("CurrentMinigame", RepLayoutCmdType.Property)]
		public ActorGUID CurrentMinigame { get; set; }
	}
}