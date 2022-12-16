﻿using FortniteReplayReader.Models.NetFieldExports.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Unreal.Core.Models;

namespace FortniteReplayReader.Models
{
    public class GameState
    {
        public string SessionId { get; internal set; }
        public float GameWorldStartTime { get; internal set; }
        public float MatchEndTime { get; internal set; }
        public float InitialSafeZoneStartTime { get; internal set; }
        public DateTime MatchTime { get; internal set; }
        public int TotalBots { get; internal set; }
        public FortPoiManager PoiManager { get; internal set; }
        public string PlaylistId { get; internal set; }
		public Player ReplayRecorder { get; internal set; }

		public int TotalTeams { get; internal set; }
        public int TeamSize
        {
            get
            {
                //Older replays
                if(TotalTeams == 0)
                {
                    return OldTeamSize;
                }

                return (int)Math.Round(MaxPlayers / (double)TotalTeams);
            }
        }

        public int MaxPlayers { get; internal set; }
        public uint WinningTeam { get; internal set; }

        public float AirCraftStartTime { get; internal set; }

        public List<Aircraft> BusPaths { get; internal set; } = new List<Aircraft>();

        public bool LargeTeamGame { get; internal set; }
        public int EventTournamentRound => (int)EEventTournamentRound;

        public EEventTournamentRound EEventTournamentRound { get; internal set; } = EEventTournamentRound.EEventTournamentRound_MAX;

        public float CurrentWorldTime { get; internal set; }

        //public List<Player> WinningPlayers { get; internal set; } = new List<Player>();

        //Internal information to keep track of game state
        internal int RemainingPlayers { get; set; }
        internal int CurrentTeams { get; set; }
        internal byte SafeZonePhase { get; set; }
        internal int RemainingBots { get; set; }
        internal int TotalPlayerStructures { get; set; }
        internal float ElapsedTime { get; set; } //Time since last update?
        internal int OldTeamSize { get; set; } //Used in older replays
        internal float DeltaGameTime => CurrentWorldTime - GameWorldStartTime;
		internal uint RecorderActor { get; set; }
	}
}
