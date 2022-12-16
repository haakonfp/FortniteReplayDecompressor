using Unreal.Core.Models;

namespace Unreal.Core.Contracts;

public abstract class INetFieldExportGroup
{
	public Actor ChannelActor { get; internal set; }
	public ExternalData ExternalData { get; internal protected set; }

	public virtual bool ManualRead(string property, object value)
	{
		return false;
	}
}
