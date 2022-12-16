using Unreal.Core.Attributes;
using Unreal.Core.Contracts;
using Unreal.Core.Models;
using Unreal.Core.Models.Enums;

namespace FortniteReplayReader.Models.NetFieldExports.ClassNetCaches.Structures;

[NetFieldExportGroup("/Script/FortniteGame.FortClientObservedStat", ParseType.Normal)]
public class FortClientObservedStat : INetFieldExportGroup
{
	[NetFieldExport("StatName", RepLayoutCmdType.PropertyName)]
	public string StatName { get; set; }

	[NetFieldExport("StatValue", RepLayoutCmdType.PropertyUInt32)]
	public uint? StatValue { get; set; }

	public override bool ManualRead(string property, object value)
	{
		switch (property)
		{
			case "StatName":
				StatName = (string)value;
				break;
			case "StatValue":
				StatValue = (uint)value;
				break;
			default:
				return base.ManualRead(property, value);
		}

		return true;
	}

}
