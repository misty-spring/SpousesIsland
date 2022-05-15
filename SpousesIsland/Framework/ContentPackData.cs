//using StardewModdingAPI;

namespace SpousesIsland.Framework
{
    public class ContentPackData //: ITranslationHelper
    {
        //add support for translations in the future

        public string Spousename { get; set; }
        public string ArrivalPosition { get; set; }
        public string ArrivalDialogue { get; set; }
        public Location1Data Location1 { get; set; }
        public Location2Data Location2 { get; set; }
        public Location3Data Location3 { get; set; }

        public ContentPackData()
        {
        }
        public ContentPackData(ContentPackData cpd)
        {
            Spousename = cpd.Spousename;
            ArrivalPosition = cpd.ArrivalPosition;
            ArrivalDialogue = cpd.ArrivalDialogue;

            //Location1 = cpd.Location1;
            Location1.Name = cpd.Location1.Name;
            Location1.Time = cpd.Location1.Time;
            Location1.Position = cpd.Location1.Position;
            Location1.Dialogue = cpd.Location1.Dialogue;

            //Location2 = cpd.Location2;
            Location2.Name = cpd.Location2.Name;
            Location2.Time = cpd.Location2.Time;
            Location2.Position = cpd.Location2.Position;
            Location2.Dialogue = cpd.Location2.Dialogue;

            //Location3 = cpd.Location3;
            Location3.Name = cpd.Location3.Name;
            Location3.Time = cpd.Location3.Time;
            Location3.Position = cpd.Location3.Position;
            Location3.Dialogue = cpd.Location3.Dialogue;
        }
    }
}