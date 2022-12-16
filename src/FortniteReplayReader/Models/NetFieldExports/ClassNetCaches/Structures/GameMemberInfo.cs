using Unreal.Core.Attributes;
using Unreal.Core.Contracts;
using Unreal.Core.Models;
using Unreal.Core.Models.Enums;

namespace FortniteReplayReader.Models.NetFieldExports.ClassNetCaches.Structures;

[NetFieldExportGroup("/Script/FortniteGame.GameMemberInfo", ParseType.Normal)]
public class GameMemberInfo : INetFieldExportGroup
{
	[NetFieldExport("SquadId", RepLayoutCmdType.PropertyByte)]
	public byte SquadId { get; set; }

	[NetFieldExport("TeamIndex", RepLayoutCmdType.Enum)]
	public int TeamIndex { get; set; }

	[NetFieldExport("MemberUniqueId", RepLayoutCmdType.PropertyNetId)]
	public string MemberUniqueId { get; set; }

	public override bool ManualRead(string property, object value)
	{
		switch (property)
		{
			case "SquadId":
				SquadId = (byte)value;
				break;
			case "TeamIndex":
				TeamIndex = (int)value;
				break;
			case "MemberUniqueId":
				MemberUniqueId = (string)value;
				break;
			default:
				return base.ManualRead(property, value);
		}

		return true;
	}

}
