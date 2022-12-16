﻿namespace FortniteReplayReader.Models;

public class WeaponSwitch
{
	public Weapon Weapon { get; internal set; }
	public WeaponSwitchState State { get; internal set; }
	public float DeltaGameTimeSeconds { get; internal set; }

	public override string ToString()
	{
		return Weapon?.ToString();
	}
}

public enum WeaponSwitchState { Equipped, Unequipped };
