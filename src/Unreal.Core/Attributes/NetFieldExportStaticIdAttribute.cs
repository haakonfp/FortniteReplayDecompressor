using System;

namespace Unreal.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
    public sealed class NetFieldExportStaticGroupAttribute : Attribute
    {
        public string StaticActorId { get; private set; }

        public NetFieldExportStaticGroupAttribute(string staticActorId)
        {
            StaticActorId = staticActorId;
        }
    }
}
