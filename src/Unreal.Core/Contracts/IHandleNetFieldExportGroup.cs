using System.Collections.Generic;
using Unreal.Core.Models;

namespace Unreal.Core.Contracts;

//Throws unknown handles here instead of throwing warnings 
public abstract class IHandleNetFieldExportGroup : INetFieldExportGroup
{
	public Dictionary<uint, DebuggingObject> UnknownHandles = new();
}
