﻿using Unreal.Core.Models;

namespace FortniteReplayReader.Models;

public class Vehicle
{
	public string VehicleName { get; set; }
	public FVector SpawnLocation { get; set; }
	public FRotator SpawnRotation { get; set; }

	public PlayerLocationRepMovement CurrentLocation { get; set; }
}
