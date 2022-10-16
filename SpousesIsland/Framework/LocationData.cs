namespace SpousesIsland.Framework
{
    public class LocationData
    {
        public string Name { get; set; }
        public int Time { get; set; }
        public string Position { get; set; }
        public string Dialogue { get; set; }

        public LocationData()
        {
        }

        public LocationData(LocationData loc1)
        {
            Name = loc1.Name;
            Time = loc1.Time;
            Position = loc1.Position;
            Dialogue = loc1.Dialogue;
        }
    }
}
