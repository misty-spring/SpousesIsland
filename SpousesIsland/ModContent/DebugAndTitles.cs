using SpousesIsland.Framework;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpousesIsland
{
    public class Debugging
    {
        
        internal static void Reset(string arg1, string[] arg2)
        {
            var args = arg2;

            bool IsDebug = args?.Contains("debug") ?? false;
            var firstArg = args[0] ?? null;

            if (!Context.IsWorldReady && !IsDebug)
            {
                ModEntry.Mon.Log(ModEntry.Help.Translation.Get("CLI.nosaveloaded"), LogLevel.Error);
                return;
            }
            else if (args?.Length > 1 && !IsDebug)
            {
                ModEntry.Mon.Log(ModEntry.Help.Translation.Get($"CLI.TooManyArguments"), LogLevel.Info);
            }
            else if (!args.Any() || firstArg is "help" || firstArg is "h")
            {
                ModEntry.Mon.Log(ModEntry.Help.Translation.Get("CLI.reset.description"), LogLevel.Info);
            }
            else
            {
                if (firstArg is "schedules" || firstArg is "s")
                {
                    int ResetCounter = 0;
                    foreach (string str in ModEntry.CustomSchedule.Keys)
                    {
                        ModEntry.Help.GameContent.InvalidateCache($"Characters/schedules/{str}");
                        ResetCounter++;
                    }
                    ModEntry.Mon.Log($"Reloaded {ResetCounter} schedules.", LogLevel.Info);
                }
                else if (firstArg is "dialogues" || firstArg is "d")
                {
                    int ResetCounter = 0;
                    int TlCounter = 0;
                    foreach (IslandVisitData cpd in ModEntry.CustomSchedule.Values)
                    {
                        ModEntry.Help.GameContent.InvalidateCache($"Characters/Dialogue/{cpd.Spousename}");
                        foreach (DialogueTranslation tl in cpd.Translations)
                        {
                            ModEntry.Help.GameContent.InvalidateCache($"Characters/schedules/{cpd?.Spousename}{Information.ParseLangCode(tl?.Key)}");
                            TlCounter++;
                        }
                        ResetCounter++;
                    }
                    ModEntry.Mon.Log($"Reloaded {ResetCounter} default dialogues and {TlCounter} translations.", LogLevel.Info);
                }
                else if (firstArg is "packs" || firstArg is "p")
                {
                    ModEntry.Mon.Log(ModEntry.Help.Translation.Get($"CLI.MustReset"), LogLevel.Warn);
                }
                else
                {
                    ModEntry.Mon.Log(ModEntry.Help.Translation.Get($"CLI.InvalidValue"), LogLevel.Error);
                }
            }
        }
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
        internal static void List(string arg1, string[] arg2)
        {
            var args = arg2;

            if (!Context.IsWorldReady && !args.Contains("debug"))
            {
                ModEntry.Mon.Log(ModEntry.Help.Translation.Get("CLI.nosaveloaded"), LogLevel.Error);
                return;
            }
            else if (args.Count<string>() > 1 && args[1] is not "debug")
            {
                ModEntry.Mon.Log(ModEntry.Help.Translation.Get($"CLI.TooManyArguments"), LogLevel.Info);
            }
            else if (!args.Any() || args[0] is "help" || args[0] is null)
            {
                ModEntry.Mon.Log(ModEntry.Help.Translation.Get("CLI.list.description"), LogLevel.Info);
            }
            else
            {
                if (args[0] is "schedules" || args[0] is "s")
                {
                    string tempsched = "";
                    foreach (string s in ModEntry.SchedulesEdited)
                    {
                        tempsched += $"\n   {s}";
                    }
                    ModEntry.Mon.Log($"\n{ModEntry.Help.Translation.Get("CLI.get.schedules")}\n{tempsched}", LogLevel.Info);
                }
                else if (args[0] is "dialogues" || args[0] is "d")
                {
                    string tempdial = "";
                    foreach (string s in ModEntry.DialoguesEdited)
                    {
                        tempdial += $"\n   {s}";
                    }
                    ModEntry.Mon.Log($"\n{ModEntry.Help.Translation.Get("CLI.get.dialogues")}\n{tempdial}", LogLevel.Info);
                }
                else if (args[0] is "packs" || args[0] is "p")
                {
                    string tempkeys = "";
                    foreach (string s in ModEntry.CustomSchedule.Keys)
                    {
                        tempkeys += $"\n   {s}";
                    }
                    ModEntry.Mon.Log($"{ModEntry.Help.Translation.Get("CLI.get.packs")}\n{tempkeys}", LogLevel.Info);
                }
                else if (args[0] is "translations" || args[0] is "translation" || args[0] is "tl")
                {
                    string temptl = "";
                    foreach (string s in ModEntry.TranslationsAdded)
                    {
                        temptl += $"\n   {s}";
                    }
                    ModEntry.Mon.Log($"{ModEntry.Help.Translation.Get("CLI.get.packs")}\n{temptl}", LogLevel.Info);
                }
                else if (args[0] is "internal" || args[0] is "i")
                {
                    ModEntry.Mon.Log($"Internal info: \n\n PreviousDayRandom = {ModEntry.PreviousDayRandom}; \n\n CCC = {ModEntry.CCC}; \n\n SawDevan4H = {ModEntry.SawDevan4H}; \n\n HasSVE = {ModEntry.HasSVE}; \n\n HasC2N = {ModEntry.HasC2N}; \n\n HasExGIM = {ModEntry.HasExGIM}; \n\n IsDebug = {ModEntry.IsDebug}", LogLevel.Info);
                }
                else if (args[0] is "married" || args[0] is "m" || args[0] is "im")
                {
                    string log = null;
                    var all = Values.GetAllSpouses(Game1.player);
                    foreach(var name in all)
                    {
                        if(all[^1].Equals(name))
                        {
                            log += $"{name}.";
                        }
                        else
                        {
                            log += $"{name}, ";
                        }
                    }
                    ModEntry.Mon.Log(log, LogLevel.Info);
                }
                else
                {
                    ModEntry.Mon.Log(ModEntry.Help.Translation.Get($"CLI.InvalidValue"), LogLevel.Error);
                }
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