﻿using System;
using System.IO;
using System.Text;
using Unreal.Core.Models;
using Unreal.Core.Models.Enums;

namespace Unreal.Core;

/// <summary>
/// see https://github.com/EpicGames/UnrealEngine/blob/70bc980c6361d9a7d23f6d23ffe322a2d6ef16fb/Engine/Source/Runtime/Core/Public/Serialization/BitArchive.h
/// see https://github.com/EpicGames/UnrealEngine/blob/70bc980c6361d9a7d23f6d23ffe322a2d6ef16fb/Engine/Source/Runtime/Core/Private/Serialization/BitArchive.cpp
/// </summary>
public class BitReader : FBitArchive
{
	private FBitArray Bits { get; set; }

	/// <summary>
	/// Position in current BitArray. Set with <see cref="Seek(int, SeekOrigin)"/>
	/// </summary>
	public override int Position { get => _position; protected set => _position = value; }

	private int _position;

	/// <summary>
	/// Last used bit Position in current BitArray. Used to avoid reading trailing zeros to fill last byte.
	/// </summary>
	public int LastBit { get; private set; }

	/// <summary>
	/// For pushing and popping FBitReaderMark positions.
	/// </summary>
	public int MarkPosition { get; private set; }

	/// <summary>
	/// Initializes a new instance of the BitReader class based on the specified bytes.
	/// </summary>
	/// <param name="input">The input bytes.</param>
	/// <exception cref="System.ArgumentException">The stream does not support reading, is null, or is already closed.</exception>
	public BitReader(byte[] input)
	{
		Bits = new FBitArray(input);

		LastBit = Bits.Length;
	}

	public BitReader(byte[] input, int bitCount)
	{
		Bits = new FBitArray(input);
		LastBit = bitCount;
	}

	/// <summary>
	/// Initializes a new instance of the BitReader class based on the specified bool[].
	/// </summary>
	/// <param name="input">The input bool[].</param>

	public BitReader(bool[] input)
	{
		Bits = new FBitArray(input);
		LastBit = Bits.Length;
	}

	public BitReader(bool[] input, int bitCount)
	{
		Bits = new FBitArray(input);
		LastBit = bitCount;
	}

	/// <summary>
	/// Returns whether <see cref="Position"/> in current <see cref="Bits"/> is greater than the lenght of the current <see cref="Bits"/>.
	/// </summary>
	/// <returns>true, if <see cref="Position"/> is greater than lenght, false otherwise</returns>
	public override bool AtEnd()
	{
		return _position >= LastBit;
	}

	public override bool CanRead(int count)
	{
		return _position + count <= LastBit;
	}

	/// <summary>
	/// Returns the bit at <see cref="Position"/> and does not advance the <see cref="Position"/> by one bit.
	/// </summary>
	/// <returns>The value of the bit at position index.</returns>
	/// <seealso cref="ReadBit"/>
	public override bool PeekBit()
	{
		return Bits[_position];
	}

	/// <summary>
	/// Returns the bit at <see cref="Position"/> and advances the <see cref="Position"/> by one bit.
	/// </summary>
	/// <returns>The value of the bit at position index.</returns>
	/// <seealso cref="PeekBit"/>
	public override bool ReadBit()
	{

		if (_position >= LastBit || IsError)
		{
			IsError = true;
			return false;
		}

		return Bits[_position++];
	}

	/// <summary>
	/// Retuns int and advances the <see cref="Position"/> by <paramref name="bits"/> bits.
	/// </summary>
	/// <param name="bits">The number of bits to read.</param>
	/// <returns>int</returns>
	public int ReadBitsToInt(int bitCount)
	{
		var result = new byte();

		bool[] bits = ReadBits(bitCount);

		for (var i = 0; i < bits.Length; i++)
		{
			if (bits[i])
			{
				result |= (byte)(1 << i);
			}
		}

		return result;
	}

	/// <summary>
	/// Retuns int and advances the <see cref="Position"/> by <paramref name="bits"/> bits.
	/// </summary>
	/// <param name="bits">The number of bits to read.</param>
	/// <returns>int</returns>
	public ulong ReadBitsToLong(int bitCount)
	{
		ulong result = new();

		bool[] bits = ReadBits(bitCount);

		for (var i = 0; i < bits.Length; i++)
		{
			if (bits[i])
			{
				result |= (1UL << i);
			}
		}

		return result;
	}

	/// <summary>
	/// Retuns bool[] and advances the <see cref="Position"/> by <paramref name="bits"/> bits.
	/// </summary>
	/// <param name="bits">The number of bits to read.</param>
	/// <returns>bool[]</returns>
	public override bool[] ReadBits(int bitCount)
	{
		if (!CanRead(bitCount) || bitCount < 0)
		{
			IsError = true;
			return Array.Empty<bool>();
		}

		var result = new bool[bitCount];

		//Buffer.BlockCopy(Bits.Items, _position, result, 0, bitCount);


#if NETSTANDARD2_0
            Array.Copy(Bits.Items, _position, result, 0, bitCount);
#else
		Bits.AsSpan(_position, bitCount).CopyTo(result);
#endif

		_position += bitCount;

		return result;
	}

	public override void Read(bool[] buffer, int count)
	{
		if (!CanRead(count) || count < 0)
		{
			IsError = true;

			return;
		}

		Buffer.BlockCopy(Bits.Items, _position, buffer, 0, count);

		_position += count;
	}

	/// <summary>
	/// Retuns bool[] and advances the <see cref="Position"/> by <paramref name="bits"/> bits.
	/// </summary>
	/// <param name="bits">The number of bits to read.</param>
	/// <returns>bool[]</returns>
	public override bool[] ReadBits(uint bitCount)
	{
		return ReadBits((int)bitCount);
	}

	/// <summary>
	/// Returns the bit at <see cref="Position"/> and advances the <see cref="Position"/> by one bit.
	/// </summary>
	/// <returns>The value of the bit at position index.</returns>
	/// <seealso cref="ReadBit"/>
	/// <exception cref="IndexOutOfRangeException"></exception>
	public override bool ReadBoolean()
	{
		return ReadBit();
	}

	/// <summary>
	/// Returns the byte at <see cref="Position"/>
	/// </summary>
	/// <returns>The value of the byte at <see cref="Position"/> index.</returns>
	public override byte PeekByte()
	{
		var result = ReadByte();
		_position -= 8;

		return result;
	}

	/// <summary>
	/// Returns the byte at <see cref="Position"/> and advances the <see cref="Position"/> by 8 bits.
	/// </summary>
	/// <returns>The value of the byte at <see cref="Position"/> index.</returns>
	public override byte ReadByte()
	{
		if (!CanRead(8))
		{
			IsError = true;

			return 0;
		}

		var result = new byte();

		var pos = _position;

		if (Bits.ByteArrayUsed != null && _position % 8 == 0)
		{
			result = Bits.ByteArrayUsed[_position / 8];
		}
		else
		{
			for (var i = 0; i < 8; i++)
			{
				if (Bits[pos + i])
				{
					result |= (byte)(1 << i);
				}
			}
		}

		_position += 8;

		return result;
	}

	public override T ReadByteAsEnum<T>()
	{
		return (T)Enum.ToObject(typeof(T), ReadByte());
	}

	public override byte[] ReadBytes(int byteCount)
	{
		if (byteCount < 0)
		{
			IsError = true;
			return new byte[0];
		}

		if (!CanRead(byteCount))
		{
			IsError = true;
			return new byte[0];
		}

		var result = new byte[byteCount];

		if (Bits.ByteArrayUsed != null && _position % 8 == 0)
		{
			//Pull straight from byte array
			Buffer.BlockCopy(Bits.ByteArrayUsed, _position / 8, result, 0, byteCount);

			_position += byteCount * 8;
		}
		else
		{
			bool[] bits = ReadBits(byteCount * 8);

			for (int i = 0; i < bits.Length; i += 8)
			{
				byte b = new();

				for (int x = 0; x < 8; x++)
				{
					if (bits[i + x])
					{
						b |= (byte)(1 << x);
					}
				}

				result[i / 8] = b;
			}
		}

		return result;
	}

	public override byte[] ReadBytes(uint byteCount)
	{
		return ReadBytes((int)byteCount);
	}

	public override T[] ReadArray<T>(Func<T> func1)
	{
		throw new NotImplementedException();
	}

	public override string ReadBytesToString(int count)
	{
		// https://github.com/dotnet/corefx/issues/10013
		return BitConverter.ToString(ReadBytes(count)).Replace("-", "");
	}

	/// <summary>
	/// see https://github.com/EpicGames/UnrealEngine/blob/70bc980c6361d9a7d23f6d23ffe322a2d6ef16fb/Engine/Source/Runtime/Core/Private/Containers/String.cpp#L1390
	/// </summary>
	/// <returns>string</returns>
	public override string ReadFString()
	{
		var length = ReadInt32();

		if (length == 0 || IsError)
		{
			return "";
		}

		string value;
		if (length < 0)
		{
			length = -2 * length;

			int totalBits = length * 8;

			if (totalBits <= 0 || !CanRead(totalBits))
			{
				IsError = true;
				return "";
			}

			value = Encoding.Unicode.GetString(ReadBytes(length));
		}
		else
		{
			int totalBits = length * 8;

			if (totalBits <= 0 || !CanRead(totalBits))
			{
				IsError = true;
				return "";
			}

			value = Encoding.Default.GetString(ReadBytes(length));
		}

		return value.Trim(new[] { ' ', '\0' });
	}

	public override string ReadGUID()
	{
		return ReadBytesToString(16);
	}

	public override string ReadGUID(int size)
	{
		return ReadBytesToString(size);
	}

	/// <summary>
	/// see https://github.com/EpicGames/UnrealEngine/blob/70bc980c6361d9a7d23f6d23ffe322a2d6ef16fb/Engine/Source/Runtime/Core/Public/Serialization/BitReader.h#L69
	/// </summary>
	/// <param name="maxValue"></param>
	/// <returns>uint</returns>
	/// <exception cref="OverflowException"></exception>
	public override uint ReadSerializedInt(int maxValue)
	{
		// https://github.com/EpicGames/UnrealEngine/blob/70bc980c6361d9a7d23f6d23ffe322a2d6ef16fb/Engine/Source/Runtime/Core/Private/Serialization/BitWriter.cpp#L123
		//  const int32 LengthBits = FMath::CeilLogTwo(ValueMax); ???

		uint value = 0;
		for (uint mask = 1; (value + mask) < maxValue; mask *= 2)
		{
			if (ReadBit())
			{
				value |= mask;
			}
		}

		return value;
	}

	public UInt32 ReadUInt32Max(Int32 maxValue)
	{
		var maxBits = Math.Floor(Math.Log10(maxValue) / Math.Log10(2)) + 1;

		UInt32 value = 0;
		for (int i = 0; i < maxBits && (value + (1 << i)) < maxValue; ++i)
		{
			value += (ReadBit() ? 1U : 0U) << i;
		}

		if (value > maxValue)
		{
			throw new Exception("ReadUInt32Max overflowed!");
		}

		return value;

	}

	public override short ReadInt16()
	{
		var value = ReadBytes(2);
#if NETSTANDARD2_0
            return IsError ? (short)0 : BitConverter.ToInt16(value, 0);
#else
		return IsError ? (short)0 : BitConverter.ToInt16(value);
#endif
	}

	public override int ReadInt32()
	{
		var value = ReadBytes(4);

#if NETSTANDARD2_0
            return IsError ? 0 : BitConverter.ToInt32(value, 0);
#else
		return IsError ? 0 : BitConverter.ToInt32(value);
#endif
	}

	public override bool ReadInt32AsBoolean()
	{
		var i = ReadInt32();

		return i == 1;
	}

	public override long ReadInt64()
	{
		var value = ReadBytes(8);

#if NETSTANDARD2_0
            return IsError ? 0 : BitConverter.ToInt64(value, 0);
#else
		return IsError ? 0 : BitConverter.ToInt64(value);
#endif
	}

	/// <summary>
	/// Retuns uint
	/// see https://github.com/EpicGames/UnrealEngine/blob/70bc980c6361d9a7d23f6d23ffe322a2d6ef16fb/Engine/Source/Runtime/Core/Private/Serialization/BitReader.cpp#L254
	/// </summary>
	/// <returns>uint</returns>
	public override uint ReadIntPacked()
	{
		int BitsUsed = (int)_position % 8;
		int BitsLeft = 8 - BitsUsed;
		int SourceMask0 = (1 << BitsLeft) - 1;
		int SourceMask1 = (1 << BitsUsed) - 1;

		uint value = 0;

		int OldPos = _position;

		int shift = 0;
		for (var it = 0; it < 5; it++)
		{
			if (IsError)
			{
				return 0;
			}

			int currentBytePos = (int)_position / 8;
			int byteAlignedPositon = currentBytePos * 8;

			_position = byteAlignedPositon;

			byte currentByte = ReadByte();
			byte nextByte = currentByte;

			if (BitsUsed != 0)
			{
				nextByte = (_position + 8 <= LastBit) ? PeekByte() : new byte();
			}

			OldPos += 8;

			int readByte = ((currentByte >> BitsUsed) & SourceMask0) | ((nextByte & SourceMask1) << (BitsLeft & 7));

			value = (uint)((readByte >> 1) << shift) | value;

			if ((readByte & 1) == 0)
			{
				break;
			}
			shift += 7;
		}

		_position = OldPos;

		return value;
	}

	/// <summary>
	/// see https://github.com/EpicGames/UnrealEngine/blob/70bc980c6361d9a7d23f6d23ffe322a2d6ef16fb/Engine/Source/Runtime/Engine/Classes/Engine/NetSerialization.h#L1210
	/// </summary>
	/// <returns>Vector</returns>
	public override FVector ReadPackedVector(int scaleFactor, int maxBits)
	{
		if (EngineNetworkVersion >= EngineNetworkVersionHistory.HISTORY_PACKED_VECTOR_LWC_SUPPORT)
		{
			return ReadQuantizedVector(scaleFactor);
		}
		else
		{
			return ReadPackedVector_Legacy(scaleFactor, maxBits);
		}
	}

	private FVector ReadQuantizedVector(int scaleFactor)
	{
		var componentBitCountAndExtraInfo = ReadUInt32Max(1 << 7);
		var componentBitCount = componentBitCountAndExtraInfo & 63U;
		var extraInfo = componentBitCountAndExtraInfo >> 6;

		if (componentBitCount > 0)
		{
			ulong X = ReadBitsToLong((int)componentBitCount);
			ulong Y = ReadBitsToLong((int)componentBitCount);
			ulong Z = ReadBitsToLong((int)componentBitCount);

			ulong signBit = 1UL << (int)(componentBitCount - 1);

			double fX = (long)(X ^ signBit) - (long)signBit;
			double fY = (long)(Y ^ signBit) - (long)signBit;
			double fZ = (long)(Z ^ signBit) - (long)signBit;

			if (extraInfo > 0)
			{
				fX /= scaleFactor;
				fY /= scaleFactor;
				fZ /= scaleFactor;
			}

			var f = new FVector(fX, fY, fZ);
			f.Bits = (int)componentBitCount;
			f.ScaleFactor = scaleFactor;
			return f;
		}
		else if (extraInfo == 0)
		{
			var x = ReadSingle();
			var y = ReadSingle();
			var z = ReadSingle();
			var f = new FVector(x, y, z);
			f.Bits = 32;
			return f;
		}
		else
		{
			var x = ReadDouble();
			var y = ReadDouble();
			var z = ReadDouble();
			var f = new FVector(x, y, z);
			f.Bits = 64;
			return f;
		}
	}

	private FVector ReadPackedVector_Legacy(int scaleFactor, int maxBits)
	{
		var bits = ReadSerializedInt(maxBits);

		if (IsError)
		{
			return new FVector(0, 0, 0);
		}

		var bias = 1 << ((int)bits + 1);
		var max = 1 << ((int)bits + 2);

		var dx = ReadSerializedInt(max);
		var dy = ReadSerializedInt(max);
		var dz = ReadSerializedInt(max);

		if (IsError)
		{
			return new FVector(0, 0, 0);
		}

		var x = (float)(dx - bias) / scaleFactor;
		var y = (float)(dy - bias) / scaleFactor;
		var z = (float)(dz - bias) / scaleFactor;

		FVector vector = new(x, y, z);
		vector.ScaleFactor = scaleFactor;
		vector.Bits = (int)bits;

		return vector;
	}

	/// <summary>
	/// see https://github.com/EpicGames/UnrealEngine/blob/70bc980c6361d9a7d23f6d23ffe322a2d6ef16fb/Engine/Source/Runtime/Core/Private/Math/UnrealMath.cpp#L79
	/// see https://github.com/EpicGames/UnrealEngine/blob/70bc980c6361d9a7d23f6d23ffe322a2d6ef16fb/Engine/Source/Runtime/Core/Public/Math/Rotator.h#L654
	/// </summary>
	/// <returns></returns>
	public override FRotator ReadRotation()
	{
		float pitch = 0;
		float yaw = 0;
		float roll = 0;

		if (ReadBit()) // Pitch
		{
			pitch = ReadByte() * 360f / 256f;
		}

		if (ReadBit())
		{
			yaw = ReadByte() * 360f / 256f;
		}

		if (ReadBit())
		{
			roll = ReadByte() * 360f / 256f;
		}

		if (IsError)
		{
			return new FRotator(0, 0, 0);
		}

		return new FRotator(pitch, yaw, roll);
	}

	/// <summary>
	/// see https://github.com/EpicGames/UnrealEngine/blob/70bc980c6361d9a7d23f6d23ffe322a2d6ef16fb/Engine/Source/Runtime/Core/Private/Math/UnrealMath.cpp#L79
	/// see https://github.com/EpicGames/UnrealEngine/blob/70bc980c6361d9a7d23f6d23ffe322a2d6ef16fb/Engine/Source/Runtime/Core/Public/Math/Rotator.h#L654
	/// </summary>
	/// <returns></returns>
	public override FRotator ReadRotationShort()
	{
		float pitch = 0;
		float yaw = 0;
		float roll = 0;

		if (ReadBit()) // Pitch
		{
			pitch = ReadUInt16() * 360 / 65536f;
		}

		if (ReadBit())
		{
			yaw = ReadUInt16() * 360 / 65536f;
		}

		if (ReadBit())
		{
			roll = ReadUInt16() * 360 / 65536f;
		}

		if (IsError)
		{
			return new FRotator(0, 0, 0);
		}

		return new FRotator(pitch, yaw, roll);
	}

	public override sbyte ReadSByte()
	{
		throw new NotImplementedException();
	}

	public override float ReadSingle()
	{
		byte[] arr = ReadBytes(4);

		if (IsError)
		{
			return 0;
		}

#if NETSTANDARD2_0
            return BitConverter.ToSingle(arr, 0);
#else
		return BitConverter.ToSingle(arr);
#endif
	}

	public override double ReadDouble()
	{
		byte[] arr = ReadBytes(8);

		if (IsError)
		{
			return 0;
		}

#if NETSTANDARD2_0
            return BitConverter.ToDouble(arr, 0);
#else
		return BitConverter.ToDouble(arr);
#endif
	}

	public override (T, U)[] ReadTupleArray<T, U>(Func<T> func1, Func<U> func2)
	{
		throw new NotImplementedException();
	}

	public override ushort ReadUInt16()
	{
		byte[] arr = ReadBytes(2);

		if (IsError)
		{
			return 0;
		}

#if NETSTANDARD2_0
            return BitConverter.ToUInt16(arr, 0);
#else
		return BitConverter.ToUInt16(arr);
#endif
	}

	public override uint ReadUInt32()
	{
		byte[] arr = ReadBytes(4);

		if (IsError)
		{
			return 0;
		}

#if NETSTANDARD2_0
            return BitConverter.ToUInt32(arr, 0);
#else
		return BitConverter.ToUInt32(arr);
#endif
	}

	public override bool ReadUInt32AsBoolean()
	{
		throw new NotImplementedException();
	}

	public override T ReadUInt32AsEnum<T>()
	{
		throw new NotImplementedException();
	}

	public override ulong ReadUInt64()
	{
		byte[] arr = ReadBytes(8);

		if (IsError)
		{
			return 0;
		}


#if NETSTANDARD2_0
            return BitConverter.ToUInt32(arr, 0);
#else
		return BitConverter.ToUInt64(arr);
#endif
	}

	/// <summary>
	/// Sets <see cref="Position"/> within current BitArray.
	/// </summary>
	/// <param name="offset">The offset relative to the <paramref name="seekOrigin"/>.</param>
	/// <param name="seekOrigin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
	/// <returns></returns>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public override void Seek(int offset, SeekOrigin seekOrigin = SeekOrigin.Begin)
	{
		if (offset < 0 || offset > LastBit || (seekOrigin == SeekOrigin.Current && offset + _position > LastBit))
		{
			throw new ArgumentOutOfRangeException("Specified offset doesnt fit within the BitArray buffer");
		}

		_ = (seekOrigin switch
		{
			SeekOrigin.Begin => _position = offset,
			SeekOrigin.End => _position = LastBit - offset,
			SeekOrigin.Current => _position += offset,
			_ => _position = offset,
		});
	}

	public override void SkipBytes(uint byteCount)
	{
		SkipBytes((int)byteCount);
	}

	public override void SkipBytes(int byteCount)
	{
		Seek(byteCount * 8, SeekOrigin.Current);
	}

	public override void SkipBits(int numbits)
	{
		_position += numbits;

		if (numbits < 0 || _position > LastBit)
		{
			IsError = true;

			_position = LastBit;
		}
	}

	/// <summary>
	/// Save Position to <see cref="MarkPosition"/> so we can reset back to this point.
	/// see https://github.com/EpicGames/UnrealEngine/blob/70bc980c6361d9a7d23f6d23ffe322a2d6ef16fb/Engine/Source/Runtime/Core/Public/Serialization/BitReader.h#L228
	/// </summary>
	public override void Mark()
	{
		MarkPosition = _position;
	}

	/// <summary>
	/// Set Position back to <see cref="MarkPosition"/>
	/// see https://github.com/EpicGames/UnrealEngine/blob/70bc980c6361d9a7d23f6d23ffe322a2d6ef16fb/Engine/Source/Runtime/Core/Public/Serialization/BitReader.h#L228
	/// </summary>
	public override void Pop()
	{
		// TODO: pop makes it sound like a list...
		_position = MarkPosition;
	}

	/// <summary>
	/// Get number of bits left, including any bits after <see cref="LastBit"/>.
	/// </summary>
	/// <returns></returns>
	public override int GetBitsLeft()
	{
		return LastBit - _position;
	}

	/// <summary>
	/// Append bool array to this archive.
	/// </summary>
	/// <param name="data"></param>
	public override void AppendDataFromChecked(bool[] data)
	{
		LastBit += data.Length;
		Bits.Append(data);
	}
}
