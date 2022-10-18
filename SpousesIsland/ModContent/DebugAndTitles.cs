using StardewModdingAPI;
using System;
using System.Linq;

namespace SpousesIsland
{
    public class Debugging
    {
        internal static void Chance(string arg1, string[] arg2)
        {
            var IsDebug = arg2?.Contains<string>("debug") ?? false;

            ModEntry.Mon.Log($"{ModEntry.RandomizedInt}", LogLevel.Info);

            if (IsDebug)
            {
                if (!Context.IsWorldReady)
                {
                    ModEntry.Mon.Log(ModEntry.Help.Translation.Get("CLI.nosaveloaded"), LogLevel.Error);
                    return;
                }

                ModEntry.Mon.Log(ModEntry.Help.Translation.Get("CLI.Day0") + $": {ModEntry.PreviousDayRandom}", LogLevel.Info);
            }
        }
    }

    public class Titles
    {
        internal static string SpouseT()
        {
            var SpousesGrlTitle = "SDV";
            return SpousesGrlTitle;
        }
        internal static string SVET()
        {
            var sve = "SVE";
            return sve;
        }
        internal static string Debug()
        {
            var db = "Debug";
            return db;
        }
    }
}