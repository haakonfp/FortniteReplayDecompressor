namespace Unreal.Core.Models
{
    /// <summary>
    /// see https://github.com/EpicGames/UnrealEngine/blob/70bc980c6361d9a7d23f6d23ffe322a2d6ef16fb/Engine/Source/Runtime/Engine/Classes/Engine/DemoNetDriver.h#L60
    /// </summary>
    public class ExternalData
    {
		public uint NetGUID { get; internal set; }
		public float TimeSeconds { get; internal set; }
		public byte[] Data { get; internal set; }
		public string HandleName { get; internal set; }
	}
}
