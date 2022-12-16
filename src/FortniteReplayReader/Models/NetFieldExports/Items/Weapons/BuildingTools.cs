using Unreal.Core.Attributes;
using Unreal.Core.Models;
using Unreal.Core.Models.Enums;

namespace FortniteReplayReader.Models.NetFieldExports.Items.Weapons;

[NetFieldExportGroup("/Game/Weapons/FORT_BuildingTools/Blueprints/DefaultBuildingTool.DefaultBuildingTool_C", ParseType.Normal)]
public class BuildingTools : BaseWeapon
{
}

[NetFieldExportGroup("/Game/Weapons/FORT_BuildingTools/Blueprints/DefaultEditingTool.DefaultEditingTool_C", ParseType.Normal)]
public class EditingTools : BaseWeapon
{
}

[NetFieldExportGroup("/Game/Weapons/FORT_BuildingTools/TrapTool.TrapTool_C", ParseType.Normal)]
public class TrapTool : BaseWeapon
{
	[NetFieldExport("ItemDefinition", RepLayoutCmdType.Property)]
	public ItemDefinitionGUID ItemDefinition { get; set; }

	public override bool ManualRead(string property, object value)
	{
		switch (property)
		{
			case "ItemDefinition":
				ItemDefinition = (ItemDefinitionGUID)value;
				break;
			default:
				return base.ManualRead(property, value);
		}

		return true;
	}

}
