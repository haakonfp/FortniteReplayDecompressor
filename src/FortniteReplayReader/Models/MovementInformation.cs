﻿using FortniteReplayReader.Models.NetFieldExports.Enums;

namespace FortniteReplayReader.Models;

public class PlayerMovementInformation
{
	public bool InBus { get; internal set; }
	public bool Crouched { get; internal set; }
	public bool IsSlopeSliding { get; internal set; }
	public bool GliderOpen { get; internal set; }
	public bool Skydiving { get; internal set; }
	public bool IsInteracting { get; internal set; }
	public bool IsEmoting { get; internal set; }
	public bool IsTargeting { get; internal set; }
	public bool JumpedForceApplied { get; internal set; }
	public bool Sprinting { get; internal set; }
	public bool IsInWater { get; internal set; }
	public EFortMovementStyle MovementStyle { get; internal set; } = EFortMovementStyle.EFortMovementStyle_MAX;

	public PlayerMovementInformation Copy()
	{
		return new PlayerMovementInformation
		{
			InBus = InBus,
			Crouched = Crouched,
			GliderOpen = GliderOpen,
			IsEmoting = IsEmoting,
			IsInteracting = IsInteracting,
			IsSlopeSliding = IsSlopeSliding,
			IsTargeting = IsTargeting,
			Sprinting = Sprinting,
			Skydiving = Skydiving,
			JumpedForceApplied = JumpedForceApplied,
			IsInWater = IsInWater,
			MovementStyle = MovementStyle
		};
	}
}
