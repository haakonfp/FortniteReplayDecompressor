namespace FortniteReplayReader.Models;

// DamageTime/ClearTime may be 0 if replay ends early.
// DamageTime may be 0 if storm surge ends before
//     any damage is done.
public class StormSurge
{
	public float WarningTime { get; internal set; }
	public float DamageTime { get; internal set; }
	public float ClearTime { get; internal set; }
}