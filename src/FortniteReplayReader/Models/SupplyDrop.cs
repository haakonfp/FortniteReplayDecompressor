using System;
using System.Collections.Generic;
using System.Text;

namespace FortniteReplayReader.Models
{
    public class SupplyDrop : SearchableItem
    {
		public float SpawnTime { get; internal set; }
		public bool BalloonPopped { get; internal set; }
    }
}
