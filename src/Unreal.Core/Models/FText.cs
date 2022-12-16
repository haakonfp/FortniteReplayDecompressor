﻿using System;
using Unreal.Core.Contracts;

namespace Unreal.Core.Models;

public class FText : IProperty
{
	public string Namespace { get; set; }
	public string Key { get; set; }
	public string Text { get; set; }

	public void Serialize(NetBitReader reader)
	{
		int flags = reader.ReadInt32();
		ETextHistoryType historyType = reader.ReadByteAsEnum<ETextHistoryType>();

		switch (historyType)
		{
			case ETextHistoryType.Base:
				Namespace = reader.ReadFString();
				Key = reader.ReadFString();
				Text = reader.ReadFString();
				break;
			default:
				//Need to add other formats as they're encountered
				break;
		}
	}

	public override string ToString()
	{
		return Text;
	}
}

[Flags]
public enum FTextType
{
	Transient = (1 << 0),
	CultureInvariant = (1 << 1),
	ConvertedProperty = (1 << 2),
	Immutable = (1 << 3),
	InitializedFromString = (1 << 4),  // this ftext was initialized using FromString
};

public enum ETextHistoryType
{
	None = -1,
	Base = 0,
	NamedFormat,
	OrderedFormat,
	ArgumentFormat,
	AsNumber,
	AsPercent,
	AsCurrency,
	AsDate,
	AsTime,
	AsDateTime,
	Transform,
	StringTableEntry,
	TextGenerator
};
