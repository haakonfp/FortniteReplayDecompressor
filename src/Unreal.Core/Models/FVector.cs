using System;
using Unreal.Core.Contracts;

namespace Unreal.Core.Models;

public class FVector : IProperty
{
	/// <summary>
	/// see https://github.com/EpicGames/UnrealEngine/blob/70bc980c6361d9a7d23f6d23ffe322a2d6ef16fb/Engine/Source/Runtime/Core/Public/Math/Vector.h#L29
	/// </summary>
	/// <param name="X"></param>
	/// <param name="Y"></param>
	/// <param name="Z"></param>
	public FVector(double X, double Y, double Z)
	{
		this.X = X;
		this.Y = Y;
		this.Z = Z;
	}

	public FVector()
	{

	}

	public double X { get; set; }
	public double Y { get; set; }
	public double Z { get; set; }
	public int ScaleFactor { get; set; }
	public int Bits { get; set; }

	public override string ToString()
	{
		return $"X: {X}, Y: {Y}, Z: {Z}";
	}

	public double Size()
	{
		return (double)Math.Sqrt(X * X + Y * Y + Z * Z);
	}

	public void Serialize(NetBitReader reader)
	{
		X = reader.ReadSingle();
		Y = reader.ReadSingle();
		Z = reader.ReadSingle();
	}

	public static FVector operator -(FVector v1, FVector v2)
	{
		return new FVector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
	}

	public static bool operator ==(FVector v1, FVector v2)
	{
		return v1?.X == v2?.X && v1?.Y == v2?.Y && v1?.Z == v2?.Z;
	}

	public static bool operator !=(FVector v1, FVector v2)
	{
		return v1?.X != v2?.X || v1?.Y != v2?.Y || v1?.Z != v2?.Z;
	}

	public static FVector operator *(FVector v1, double val)
	{
		return new FVector((double)(v1.X * val), (double)(v1.Y * val), (double)(v1.Z * val));
	}

	public static FVector operator /(FVector v1, double val)
	{
		return new FVector((double)(v1.X / val), (double)(v1.Y / val), (double)(v1.Z / val));
	}

	public double DistanceTo(FVector vector)
	{
		return Math.Sqrt(DistanceSquared(vector));
	}

	private double DistanceSquared(FVector vector)
	{
		return Math.Pow(vector.X - this.X, 2) + Math.Pow(vector.Y - this.Y, 2) + Math.Pow(vector.Z - this.Z, 2);
	}
}
