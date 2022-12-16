﻿using Unreal.Core.Attributes;
using Unreal.Core.Models.Enums;

namespace FortniteReplayReader.Models.NetFieldExports.Vehicles;

[NetFieldExportGroup("/Valet/BigRig/Valet_BigRig_Vehicle.Valet_BigRig_Vehicle_C", ParseType.Full)]
public class BigRig : ValetVehicle
{

}

[NetFieldExportGroup("/Valet/BigRig/Valet_BigRig_Vehicle_Upgrade.Valet_BigRig_Vehicle_Upgrade_C", ParseType.Full)]
public class BigRigUpgrade : ValetVehicle
{

}
