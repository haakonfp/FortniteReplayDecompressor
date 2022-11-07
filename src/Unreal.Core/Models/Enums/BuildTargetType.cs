using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unreal.Core.Models.Enums
{
    /// <summary>
    /// https://docs.unrealengine.com/4.27/en-US/API/Runtime/Core/GenericPlatform/EBuildTargetType/
    /// </summary>
    [Flags]
    public enum BuildTargetType
    {
        Unknown,
        Game,
        Server,
        Client,
        Editor,
        Program,
    }
}
