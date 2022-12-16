﻿using Unreal.Core.Contracts;

namespace Unreal.Core.Models;

public class FVector2D : IProperty
{
	/// <summary>
	/// see https://github.com/EpicGames/UnrealEngine/blob/70bc980c6361d9a7d23f6d23ffe322a2d6ef16fb/Engine/Source/Runtime/Core/Public/Math/Vector2D.h#L17
	/// </summary>
	/// <param name="X"></param>
	/// <param name="Y"></param>
	public FVector2D(float X, float Y)
	{
		this.X = X;
		this.Y = Y;
	}

	public FVector2D()
	{

	}

	public float X { get; set; }
	public float Y { get; set; }

	public void Serialize(NetBitReader reader)
	{
		X = reader.SerializePropertyFloat();
		Y = reader.SerializePropertyFloat();
	}

	public override string ToString()
	{
		return $"X: {X}, Y: {Y}";
	}
}
