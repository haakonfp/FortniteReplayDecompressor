
namespace FortniteReplayReader.Models
{
    public class BaseContainer
    {
        public bool? bDestroyed { get; set; }
        public FQuantizedBuildingAttribute BuildTime { get; set; }
        public FQuantizedBuildingAttribute RepairTime { get; set; }
        public short? Health { get; set; }
        public short? MaxHealth { get; set; }
        public int? ReplicatedLootTier { get; set; }
        public uint? SearchAnimationCount { get; set; }
        public bool? bAlreadySearched { get; set; }
        public int? ResourceType { get; set; }
    }
}
