using Unreal.Core.Attributes;
using Unreal.Core.Models;
using Unreal.Core.Models.Enums;

namespace FortniteReplayReader.Models.NetFieldExports.Items.Containers;

[NetFieldExportGroup("/Game/Building/ActorBlueprints/Containers/Tiered_Chest_Athena.Tiered_Chest_Athena_C", ParseType.Full)]
public class Chest : SearchableContainer
{
}

[NetFieldExportGroup("/Game/Building/ActorBlueprints/Containers/Creative_Tiered_Chest.Creative_Tiered_Chest_C", ParseType.Full)]
public class CreativeChest : Chest
{
	[NetFieldExport("SpawnItems", RepLayoutCmdType.Property)]
	public DebuggingObject SpawnItems { get; set; }

	[NetFieldExport("PrimaryAssetName", RepLayoutCmdType.Property)]
	public DebuggingObject PrimaryAssetName { get; set; }

	[NetFieldExport("Quantity", RepLayoutCmdType.Property)]
	public DebuggingObject Quantity { get; set; }

	public override bool ManualRead(string property, object value)
	{
		switch (property)
		{
			case "SpawnItems":
				SpawnItems = (DebuggingObject)value;
				break;
			case "PrimaryAssetName":
				PrimaryAssetName = (DebuggingObject)value;
				break;
			case "Quantity":
				Quantity = (DebuggingObject)value;
				break;
			default:
				return base.ManualRead(property, value);
		}

		return true;
	}

}
