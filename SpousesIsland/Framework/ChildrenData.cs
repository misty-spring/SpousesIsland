﻿using StardewModdingAPI;
using StardewModdingAPI.Events;
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
                        ModEntry.Mon.Log($"InfoExists (for {child.Name}) is {InfoExists}.");
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

                    /*if(IsFurniture)
                    {
                        e.Edit(asset => ChildSDV(InfoChildren[indexedchild], asset, indexedchild));
                    }
                    else
                    {
                        e.Edit(asset => ChildMOD(InfoChildren[indexedchild], asset, indexedchild));
                    }*/
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

        /* if bed is furniture - deprecated
        internal static void ChildSDV(ChildSchedule ChildInfo, IAssetData asset, int index)
        {
            IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
            data["Mon"] = $"620 IslandFarmHouse 20 {10 + index} 3/1100 {ChildInfo.L1} {ChildInfo.X1} {ChildInfo.Y1}/1400 {ChildInfo.L2} {ChildInfo.X2} {ChildInfo.Y2}/1700 {ChildInfo.L3} {ChildInfo.X3} {ChildInfo.Y3}/1900 IslandFarmHouse 15 {12 + index} 0";
            data["Tue"] = "GOTO Mon";
            data["Wed"] = "GOTO Mon";
            data["Thu"] = "GOTO Mon";
            data["Fri"] = "GOTO Mon";
            data["Sat"] = "GOTO Mon";
            data["Sun"] = "GOTO Mon";
        }

        //if bed is mod's - deprecated
        internal static void ChildMOD(ChildSchedule ChildInfo, IAssetData asset, int index)
        {
            IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
            data["Mon"] = $"620 IslandFarmHouse 20 {10 + index} 3/1100 {ChildInfo.L1} {ChildInfo.X1} {ChildInfo.Y1}/1400 {ChildInfo.L2} {ChildInfo.X2} {ChildInfo.Y2}/1700 {ChildInfo.L3} {ChildInfo.X3} {ChildInfo.Y3}/1900 IslandFarmHouse 15 {12 + index} 0/2100 IslandFarmHouse {36 - index} 14 3";
            data["Tue"] = "GOTO Mon";
            data["Wed"] = "GOTO Mon";
            data["Thu"] = "GOTO Mon";
            data["Fri"] = "GOTO Mon";
            data["Sat"] = "GOTO Mon";
            data["Sun"] = "GOTO Mon";
        }*/

    }

    internal class ChildSchedule
    {
        public string Time { get; set; }
        public string Location { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get;set; }
        public string Dialogue { get; set; }

        public ChildSchedule(ChildSchedule c)
        {
            Time = c.Time;
            Location = c.Location;
            X = c.X;
            Y = c.Y;
            Z = c.Z;
            Dialogue = c.Dialogue;
        }
        /*
        public ChildSchedule(int index)
        {
            L1 = "IslandWest";
            X1 = 75 - index;
            Y1 = 46 - index;

            L2 = "IslandSouth";
            X2 = 15 + index;
            Y2 = 32;

            L3 = "IslandWest";
            X3 = 93 + index;
            Y3 = 36 - index;
        }*/
    }

}