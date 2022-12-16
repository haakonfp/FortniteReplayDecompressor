using System.Collections.Generic;

namespace FortniteReplayReader.Models;

public class Team
{
	public List<Player> Players { get; internal set; } = new List<Player>();
}
