using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using System;
using System.Collections.Generic;

namespace SpousesIsland.Framework
{
    internal class Information
    {
        /// <summary>
        /// Checks a translation's key. If it's one of the game's language codes, returns its filename/extension. If not, returns the language code as-is.
        /// </summary>
        internal static string ParseLangCode(string key)
        {
            switch (key.ToLower())
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

        /// <summary>
        /// Checks the values of the contentpack provided. If any conflict, returns a detailed error and the value "false" (to indicate the pack isn't valid).
        /// </summary>
        /// <param name="cpd">The content pack being parsed.</param>
        /// <param name="monitor">Monitor, used to inform of any errors</param>
        /// <returns></returns>
        internal static bool HasAnyErrors(IslandVisitData cpd)
        {
            var monitor = ModEntry.Mon;
            bool tempbool = false;

            if (cpd.ArrivalPosition is null)
            {
                monitor.Log($"There's no arrival position for {cpd.Spousename}!", LogLevel.Error);
                tempbool = true;
            }
            if (cpd.ArrivalDialogue is null)
            {
                monitor.Log($"There's no arrival dialogue for {cpd.Spousename}!", LogLevel.Error);
                tempbool = true;
            }
            if (cpd.ArrivalTime > 2500 || cpd.ArrivalTime < 600)
            {
                monitor.Log($"There's no arrival time for {cpd.Spousename}!", LogLevel.Error);
                tempbool = true; ;
            }
            if (cpd.ReturnTime > 2500 || cpd.ReturnTime < 600)
            {
                monitor.Log($"There's no return time for {cpd.Spousename}!", LogLevel.Error);
                tempbool = true; ;
            }

            foreach (var content in cpd.Locations)
            {
                var index = cpd.Locations.IndexOf(content);

                if (content.Name is null)
                {
                    monitor.Log($"There's no name for the map in Location #{index}.", LogLevel.Error);
                    tempbool = true;
                }
                if (content.Time <= cpd.ArrivalTime || content.Time > cpd.ReturnTime)
                {
                    monitor.Log($"The arrival time of Location #{index} must be between {cpd.ArrivalTime} and {cpd.ReturnTime}.", LogLevel.Error);
                    tempbool = true;
                }
                if (content.Position is null)
                {
                    monitor.Log($"There's no position for {cpd.Spousename} in Location #{index}.", LogLevel.Error);
                    tempbool = true;
                }
                if (content.Dialogue is null)
                {
                    monitor.Log($"There's no dialogue for {cpd.Spousename} in Location #{index}.", LogLevel.Warn);
                    //tempbool = true; just a warning
                }
            }

            int indexof = 0;
            foreach (var translation in cpd.Translations)
            {
                indexof++;

                if (translation.Key is null)
                {
                    monitor.LogOnce($"Missing language key in {cpd.Spousename}'s pack. (Position {indexof} in list)", LogLevel.Warn);
                    tempbool = true;
                }

                var locInd = 0;

                foreach(var dialogue in translation.Value)
                {
                    locInd++;

                    if (dialogue is null)
                    {
                        monitor.LogOnce($"{cpd.Spousename} pack: Translation #{locInd} missing for {translation.Key ?? "unnamed list"}.", LogLevel.Warn);
                        tempbool = true;
                    }
                }
            }

            return tempbool;
        }

        /// <summary>
        /// Compares the integers provided. If conditions apply, returns true (to reload assets).
        /// </summary>
        /// <param name="Previous"> The Random number chosen the previous in-game day.</param>
        /// <param name="CustomChance"> The custom number set by the user.</param>
        /// <param name="Current"> Today(in-game)'s randomly chosen number. Values range from 0-100.</param>
        /// <returns></returns>
        internal static bool ParseReloadCondition(int Previous, int CustomChance, int Current)
        {
            if ((CustomChance < Current && CustomChance >= Previous) || CustomChance >= Current)
            {
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Checks the validity of a specified DialogueTranslation. If the values aren't null or whitespace, returns true.
        /// </summary>
        internal static bool IsListValid(DialogueTranslation kpv)
        {
            if (!string.IsNullOrWhiteSpace(kpv.Key) && !string.IsNullOrWhiteSpace(kpv.Location1) && !string.IsNullOrWhiteSpace(kpv.Location2))
            {
                return true;
            }

            return false;
        }
        internal static string ParseOrReturnNull(LocationData ld, string Spouse, int index)
        {
            string result = "";

            if(ld?.Time > 2500 || ld?.Time < 600 || string.IsNullOrWhiteSpace(ld?.Name) || string.IsNullOrWhiteSpace(ld?.Position))
            {
                return null;
            }

            result += $"{ld?.Time} {ld?.Name} {ld?.Position}";

            if (!string.IsNullOrWhiteSpace(ld?.Dialogue))
            {
                result += $" \"Characters\\Dialogue\\{Spouse}:marriage_islandvisit_{index}\"";
            }

            result += "/";

            return result;
        }
        internal static bool ShouldReloadDevan(bool Leah, bool Elliott, bool CCC)
        {
            if (Leah is true || Elliott is true || CCC is true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        internal static bool HasMod(string ModID)
        {
            if (ModEntry.Help.ModRegistry.Get(ModID) is not null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
