//using StardewModdingAPI;

using System.Collections.Generic;

namespace SpousesIsland.Framework
{
    public class IslandVisitData
    {
        public string Spousename { get; set; }
        public int ArrivalTime { get; set; }
        public string ArrivalPosition { get; set; }
        public string ArrivalDialogue { get; set; }
        public List<LocationData> Locations { get; set; } = new();
        public Dictionary<string, List<string>> Translations { get; set; } = new();
        public int ReturnTime { get; set; }

        public IslandVisitData()
        {
        }
        public IslandVisitData(IslandVisitData ivd)
        {
            Spousename = ivd.Spousename;

            ArrivalTime = ivd.ArrivalTime;
            ArrivalPosition = ivd.ArrivalPosition;
            ArrivalDialogue = ivd.ArrivalDialogue;

            Locations = ivd.Locations;
            Translations = ivd.Translations;

            ReturnTime = ivd.ReturnTime;
        }
    }
}