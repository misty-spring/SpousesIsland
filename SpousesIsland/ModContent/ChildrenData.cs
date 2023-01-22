using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Locations;
using System;
using System.Collections.Generic;

namespace SpousesIsland
{
    
    internal class ChildrenData
    {
        public static Dictionary<string,string> GetInformation(Dictionary<string, ChildSchedule[]> dictionary)
        {
            Dictionary<string, string>result = new(); //will hold names and parsed schedule

            //template is: ARRIVALLOCATION as placeholder
            //for each 
            foreach(var childdata in dictionary)
            {
                var childname = childdata.Key;
                string Schedule = "620 IslandFarmHouse 20 ARRIVALLOCATION 3";
                var index = 0;

                //check validity of each one
                foreach(var place in childdata.Value)
                {

                    //first check its time isnt less than 620
                    //if last patch and it's Not for the farmhouse, add a "placeholder" for this.

                    var time = int.Parse(place.Time.Replace("a", ""));

                    if (time > 620)
                    {
                        index++;

                        string Dialogue = "";
                        if(!string.IsNullOrWhiteSpace(place.Dialogue))
                        {
                            Dialogue = $" \"Strings\\schedules\\{childname}:island_place{index}\"";
                        }

                        if(Array.IndexOf(childdata.Value,place) == childdata.Value.Length && !(place.Location == "IslandFarmHouse") && ModEntry.notfurniture && childname is not "default1" || childname is not "default2")
                        {
                            Schedule += $"/{place.Time} {place.Location} {place.X} {place.Y} {place.Z}{Dialogue}/{place.Time + 10} IslandFarmHouse 15 RETURNLOCATION 0";
                        }
                        else
                        {
                            Schedule+= $"/{place.Time} {place.Location} {place.X} {place.Y} {place.Z}{Dialogue}";
                        }
                    }
                    else
                    {
                        //log to modentry that it won't work
                    }
                }
            
                if(!(Schedule == "620 IslandFarmHouse 20 ARRIVALLOCATION 3"))
                {
                    result.Add(childname, Schedule);
                }
            }
        
            return result;
        }
        public static void EditAllKidSchedules(bool IsFurniture, AssetRequestedEventArgs e)
        {
            if(IsFurniture && BedCode.HasAnyKidBeds() == false)
            {
                ModEntry.Mon.Log("There's no children beds in farmhouse! The kids won't visit.", LogLevel.Warn);
                return;
            }

            var Children = ModEntry.Children;
            var InfoChildren = ModEntry.InfoChildren;

            foreach (var child in Children)
            {
                if (e.NameWithoutLocale.IsEquivalentTo($"Characters/schedules/{child.Name}"))
                {
                    var indexedchild = Children.IndexOf(child);
                    bool InfoExists;

                    //try to get value and catch just in case
                    try
                    {
                        InfoExists = InfoChildren?.Count >= indexedchild;
                        ModEntry.Mon.Log($"InfoExists (for {child.Name}) is {InfoExists}.", LogLevel.Debug);
                    }
                    catch(Exception ex)
                    {
                        ModEntry.Mon.Log($"Error while checking if child exists in mod. defaulting to false. (Exception: {ex})");
                        InfoExists = false;
                    }

                    if(!InfoExists)
                    {
                        ModEntry.Mon.Log("Child doesn't have moddata, file won't be patched.");
                        continue;
                    }

                    e.Edit(asset => PatchSchedule(InfoChildren[indexedchild.ToString()], asset, indexedchild));

                }
            }
        }

        internal static void PatchSchedule(string ChildInfo, IAssetData asset, int index)
        {
            var schedule = ChildInfo;
            schedule.Replace("ARRIVALLOCATION",$"{10 + index}");
            schedule.Replace("RETURNLOCATION", $"{12 + index}");

            IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
            data["Mon"] = schedule;
            data["Tue"] = "GOTO Mon";
            data["Wed"] = "GOTO Mon";
            data["Thu"] = "GOTO Mon";
            data["Fri"] = "GOTO Mon";
            data["Sat"] = "GOTO Mon";
            data["Sun"] = "GOTO Mon";
        }
    }

    internal class ChildSchedule
    {
        public string Time { get; set; } = "600";
        public string Location { get; set; } = "null";
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public int Z { get;set; } = 0;
        public string Dialogue { get; set; } = null;

        public ChildSchedule()
        {
        }
        
        public ChildSchedule(ChildSchedule c)
        {
            Time = c.Time;
            Location = c.Location;
            X = c.X;
            Y = c.Y;
            Z = c.Z;
            Dialogue = c.Dialogue;
        }

        public ChildSchedule(string time, string location, int x, int y, int z)
        {
            Time = time;
            Location = location;
            X = x;
            Y = y;
            Z = z;
        }
    }

    //specifically made for a single patch.
    internal class C2NPC
    {
        internal static void WarpPatch(object sender, UpdateTickedEventArgs e)
        {
            if (!(Context.IsWorldReady))
                return;

            //only run twice per second to avoid lag
            if (!(e.IsMultipleOf(60)) || !(ModEntry.IslandToday))
                return;

            //if time is between [time the kid arrives at]
            if (ModEntry.ArrivalTime && ModEntry.MustPatchC2N)
            {
                //if any npc is standing in front of the door (FishShop) AND is a child npc, halt and warp to island south + continue schedule.
                var colliding = ModEntry.FishShop_map?.doesPositionCollideWithCharacter(4, 4);

                //check if it matches any child name
                foreach(var child in ModEntry.Children)
                {
                    if(child.Name == colliding?.Name)
                    {
                        ModEntry.Mon.Log($"NPC {child?.Name} found matching in child list.");

                        colliding?.Halt();

                        var islandsouth = Game1.getLocationFromName("IslandSouth");
                        var position = new Vector2(21, 43); //right next to boat 

                        Game1.warpCharacter(colliding, islandsouth, position);
                        colliding?.moveCharacterOnSchedulePath();
                    }
                }

            }
        }
    }
}