namespace SpousesIsland.Framework
{
    public class DialogueTranslation
    {
        public string Key { get; set; }
        public string Arrival { get; set; }
        public string Location1 { get; set; }
        public string Location2 { get; set; }
        public string Location3 { get; set; }
        
        public DialogueTranslation()
        {
        }

        public DialogueTranslation(DialogueTranslation d)
        {
            Key = d.Key;
            Arrival = d.Arrival;
            Location1 = d.Location1;
            Location2 = d.Location2;
            Location3 = d.Location3;
        }
    }
}
