﻿using System;

namespace Unreal.Core.Models.Enums;

/// <summary>
/// see https://github.com/EpicGames/UnrealEngine/blob/811c1ce579564fa92ecc22d9b70cbe9c8a8e4b9a/Engine/Source/Runtime/Engine/Classes/Engine/DemoNetDriver.h#L142
/// </summary>
[Flags]
public enum ReplayHeaderFlags
{
	None = 0,
	ClientRecorded = (1 << 0),
	HasStreamingFixes = (1 << 1),
	DeltaCheckpoints = (1 << 2),
	GameSpecificFrameData = (1 << 3),
	ReplayConnection = (1 << 4), //TODO
}
