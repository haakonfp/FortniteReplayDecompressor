﻿using Unreal.Core.Attributes;
using Unreal.Core.Models.Enums;

namespace FortniteReplayReader.Models.NetFieldExports.Vehicles;

[NetFieldExportGroup("/Valet/BasicCar/Valet_BasicCar_Vehicle.Valet_BasicCar_Vehicle_C", ParseType.Full)]
public class BasicCar : ValetVehicle
{
}

[NetFieldExportGroup("/Valet/BasicCar/Valet_BasicCar_Vehicle_Upgrade.Valet_BasicCar_Vehicle_Upgrade_C", ParseType.Full)]
public class BasicCarUpgrade : ValetVehicle
{
}
