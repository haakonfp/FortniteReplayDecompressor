namespace FortniteReplayReader.Models
{
    public class TeamStats : BaseEvent
    {
        public uint Position { get; internal set; }
        public uint TotalPlayers { get; internal set; }
    }
}
