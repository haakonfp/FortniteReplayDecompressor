using FortniteReplayReader.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Unreal.Core.Models;

namespace FortniteReplayReader.Models
{
    public class PlayerEliminationInfo
    { public FVector Location { get; internal set; }
        public PlayerTypes PlayerType { get; internal set; }

        public string Id { get; internal set; }
        public bool IsBot => PlayerType == PlayerTypes.Bot || PlayerType == PlayerTypes.NamedBot;
    }
}
