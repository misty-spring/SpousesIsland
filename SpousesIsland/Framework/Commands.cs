using StardewModdingAPI;
using System;
using System.Collections.Generic;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using System.Linq;

namespace SpousesIsland.Framework
{
    internal class Commands
    {
        public static void EditDialogue(ContentPackData cpd, IAssetData asset, IMonitor monitor)
        {
            monitor.Log("EditDialogue(ContentPackData cpd, IAssetData asset, IMonitor monitor)", LogLevel.Trace);
            IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
            data["marriage_islandhouse"] = cpd.ArrivalDialogue;
            data["marriage_loc1"] = cpd.Location1.Dialogue;
            data["marriage_loc2"] = cpd.Location2.Dialogue;
            if(cpd.Location3.Dialogue is not null)
            {
                data["marriage_loc3"] = cpd.Location3.Dialogue;
            };
            if (data.TryGetValue("marriage_loc1", out _) is false || data.TryGetValue("marriage_loc2", out _) is false)
            {
                monitor.Log($"Something went wrong when editing {cpd.Spousename}'s dialogue. Please check the log for more details.", LogLevel.Error);
            };
        }
        public static void EditSchedule(ContentPackData cpd, IAssetData asset)
        {
            IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;

            data["marriage_Mon"] = $"620 FishShop 4 7 0/900 IslandSouth 1 11/940 IslandWest 77 43 0/1020 IslandFarmHouse {cpd.ArrivalPosition} \"Characters\\Dialogue\\{cpd.Spousename}:marriage_islandhouse\"/{cpd.Location1.Time} {cpd.Location1.Name} {cpd.Location1.Position} \"Characters\\Dialogue\\{cpd.Spousename}:marriage_loc1\"/{cpd.Location2.Time} {cpd.Location2.Name} {cpd.Location2.Position} \"Characters\\Dialogue\\{cpd.Spousename}:marriage_loc2\" {cpd.Location3.Time} {cpd.Location3.Name} {cpd.Location3.Position} \"Characters\\Dialogue\\{cpd.Spousename}:marriage_loc3\"/a2150 IslandFarmHouse {cpd.ArrivalPosition}";
            data["marriage_Tue"] = "GOTO marriage_Mon";
            data["marriage_Wed"] = "GOTO marriage_Mon";
            data["marriage_Thu"] = "GOTO marriage_Mon";
            data["marriage_Fri"] = "GOTO marriage_Mon";
            data["marriage_Sat"] = "GOTO marriage_Mon";
            data["marriage_Sun"] = "GOTO marriage_Mon";
        }

        internal static string ParseLangCode(string key)
        {
            switch(key)
            {
                case "de":
                    return ".de-DE";
                case "en":
                    return "";
                case "es":
                    return ".es-ES";
                case "fr":
                    return ".fr-FR";
                case "hu":
                    return ".hu-HU";
                case "it":
                    return ".it-IT";
                case "ja":
                    return ".ja-JP";
                case "ko":
                    return ".ko-KR";
                case "pt":
                    return ".pt-BR";
                case "ru":
                    return ".ru-RU";
                case "tr":
                    return ".tr-TR";
                case "zh":
                    return ".zh-CN";
                case null:
                    return "";
                default:
                    return $".{key}";
            }
        }

        internal static bool ParseContentPack(ContentPackData cpd, IMonitor monitor)
        {
            
            bool tempbool = false;
            if (cpd.ArrivalPosition is null)
            {
                monitor.Log($"There's no arrival position in {cpd.Spousename}'s schedule!", LogLevel.Error);
                tempbool = true;
            }
            if (cpd.ArrivalDialogue is null)
            {
                monitor.Log($"There's no arrival dialogue in {cpd.Spousename}'s schedule!", LogLevel.Error);
                tempbool = true;
            }
            if (cpd.Location1.Name is null)
            {
                monitor.Log($"There's no name for the Location1 map. Add the field and try again.", LogLevel.Error);
                tempbool = true;
            }
            if (cpd.Location1.Time <= 1020 || cpd.Location1.Time > 2150)
            {
                monitor.Log("Make sure the arrival time of Location1 is between 1030 and 2150.", LogLevel.Error);
                tempbool = true;
            }
            if (cpd.Location1.Position is null)
            {
                monitor.Log($"There's no position for {cpd.Spousename} in Location1.", LogLevel.Error);
                tempbool = true;
            }
            if (cpd.Location1.Dialogue is null)
            {
                monitor.Log($"There's no dialogue for {cpd.Spousename} in Location1.", LogLevel.Error);
                tempbool = true;
            }
            if (cpd.Location2.Name is null)
            {
                monitor.Log($"There's no name for the Location2 map. Add the field and try again.", LogLevel.Error);
                tempbool = true;
            }
            if (cpd.Location2.Time <= 1020 || cpd.Location2.Time > 2150)
            {
                monitor.Log("Make sure the arrival time of Location2 is between 1030 and 2150.", LogLevel.Error);
                tempbool = true;
            }
            if (cpd.Location2.Position is null)
            {
                monitor.Log($"There's no position for {cpd.Spousename} in Location2.", LogLevel.Error);
                tempbool = true;
            }
            if (cpd.Location2.Dialogue is null)
            {
                monitor.Log($"There's no dialogue for {cpd.Spousename} in Location2.", LogLevel.Error);
                tempbool = true;
            }
            if (cpd.Location3 is not null)
            {
                if (cpd.Location3.Name is null)
                {
                    monitor.Log("There's no name for the Location3 map. Add the field and try again.", LogLevel.Error);
                    tempbool = true;
                }
                if (cpd.Location3.Time <= 1020 || cpd.Location3.Time > 2150)
                {
                    monitor.Log("Make sure the arrival time of Location3 is between 1030 and 2150.", LogLevel.Error);
                    tempbool = true;
                }
                if (cpd.Location3.Position is null)
                {
                    monitor.Log($"There's no position for {cpd.Spousename} in Location3.", LogLevel.Error);
                    tempbool = true;
                }
                if (cpd.Location3.Dialogue is null)
                {
                    monitor.Log($"There's no dialogue for {cpd.Spousename} in Location3.", LogLevel.Error);
                    tempbool = true;
                }
            }
            if (tempbool is true)
            { 
                return true; 
            }
            else
            { 
                return false; 
            }    
        }
        internal static bool ParseReloadCondition(int Previous, int CustomChance, int Current)
        {
            if ((CustomChance < Current && CustomChance >= Previous) || CustomChance >= Current)
            {
                return true;
            }
            else
                return false;
        }
    }
}
