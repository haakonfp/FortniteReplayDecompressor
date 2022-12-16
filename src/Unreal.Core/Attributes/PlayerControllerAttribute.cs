using System;

namespace Unreal.Core.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class PlayerControllerAttribute : Attribute
{
	public string Name { get; private set; }

	public PlayerControllerAttribute(string name)
	{
		Name = name;
	}
}
