﻿using Unreal.Core.Attributes;
using Unreal.Core.Models.Enums;

namespace FortniteReplayReader.Models.NetFieldExports.Vehicles;

[NetFieldExportGroup("/Valet/BasicTruck/Valet_BasicTruck_Vehicle.Valet_BasicTruck_Vehicle_C", ParseType.Full)]
public class BasicTruck : ValetVehicle
{
}

[NetFieldExportGroup("/Valet/BasicTruck/Valet_BasicTruck_Vehicle_Upgrade.Valet_BasicTruck_Vehicle_Upgrade_C", ParseType.Full)]
public class BasicTruckUpgrade : ValetVehicle
{
}
