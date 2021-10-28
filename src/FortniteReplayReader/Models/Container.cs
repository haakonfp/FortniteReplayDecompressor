using Unreal.Core.Models;

namespace FortniteReplayReader.Models
{
    public class Container : BaseContainer
    {
        public int? ForceMetadataRelevant { get; set; }
        public uint? StaticMesh { get; set; }
        public ItemDefinitionGUID WeaponData { get; set; }
        public bool? bDestroyOnPlayerBuildingPlacement { get; set; }
        public bool? bInstantDeath { get; set; }
        public uint? SearchedMesh { get; set; }
        public int? AltMeshIdx { get; set; }
        public float? ProxyGameplayCueDamagePhysicalMagnitude { get; set; }
        public FGameplayEffectContextHandle EffectContext { get; set; }
        public int? ChosenRandomUpgrade { get; set; }
        public bool? bMirrored { get; set; }
        public FVector? ReplicatedDrawScale3D { get; set; }
        public bool? bIsInitiallyBuilding { get; set; }
        public bool? bForceReplayRollback { get; set; }
    }
}
