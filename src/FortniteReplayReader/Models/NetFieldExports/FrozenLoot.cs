using Unreal.Core.Attributes;
using Unreal.Core.Models;
using Unreal.Core.Models.Enums;

namespace FortniteReplayReader.Models.NetFieldExports;

[NetFieldExportGroup("/Game/Athena/Items/Gameplay/FrozenLoot/BP_Athena_FrozenLoot.BP_Athena_FrozenLoot_C", ParseType.Normal)]
public class FrozenLoot : BaseProp
{
	[NetFieldExport("LoottoSpawn", RepLayoutCmdType.Property)]
	public ItemDefinitionGUID LootToSpawn { get; set; }

	public override bool ManualRead(string property, object value)
	{
		switch (property)
		{
			case "LoottoSpawn":
				LootToSpawn = (ItemDefinitionGUID)value;
				break;
			default:
				return base.ManualRead(property, value);
		}

		return true;
	}

}
