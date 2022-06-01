using SpousesIsland.Framework;
using StardewModdingAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using static StardewValley.LocalizedContentManager;

namespace SpousesIsland
{
    public class LanguageInfo
    {
        internal static string GetLanguageCode()
        {
            return CurrentLanguageCode.ToString();
        }
    }
    public class Debugging
    {
        internal static void Reset(ModEntry me, string[] args, IModHelper Helper)
        {
            if (!Context.IsWorldReady && !args.Contains("debug"))
            {
                me.Monitor.Log(Helper.Translation.Get("CLI.nosaveloaded"), LogLevel.Error);
                return;
            }
            else if (args.Count<string>() > 1 && args[1] is not "debug")
            {
                me.Monitor.Log(Helper.Translation.Get($"CLI.TooManyArguments"), LogLevel.Info);
            }
            else if (!args.Any() || args[0] is "help" || args[0] is "h")
            {
                me.Monitor.Log(Helper.Translation.Get("CLI.reset.description"), LogLevel.Info);
            }
            else
            {
                if (args[0] is "schedules" || args[0] is "s")
                {
                    int ResetCounter = 0;
                    foreach (string str in ModEntry.CustomSchedule.Keys)
                    {
                        Helper.GameContent.InvalidateCache($"Characters/schedules/{str}");
                        ResetCounter++;
                    }
                    me.Monitor.Log($"Reloaded {ResetCounter} schedules.", LogLevel.Info);
                }
                else if (args[0] is "dialogues" || args[0] is "d")
                {
                    int ResetCounter = 0;
                    int TlCounter = 0;
                    foreach (ContentPackData cpd in ModEntry.CustomSchedule.Values)
                    {
                        Helper.GameContent.InvalidateCache($"Characters/Dialogue/{cpd.Spousename}");
                        foreach (DialogueTranslation tl in cpd.Translations)
                        {
                            Helper.GameContent.InvalidateCache($"Characters/schedules/{cpd?.Spousename}{Commands.ParseLangCode(tl?.Key)}");
                            TlCounter++;
                        }
                        ResetCounter++;
                    }
                    me.Monitor.Log($"Reloaded {ResetCounter} default dialogues and {TlCounter} translations.", LogLevel.Info);
                }
                else if (args[0] is "packs" || args[0] is "p")
                {
                    me.Monitor.Log(Helper.Translation.Get($"CLI.MustReset"), LogLevel.Warn);
                }
                else
                {
                    me.Monitor.Log(Helper.Translation.Get($"CLI.InvalidValue"), LogLevel.Error);
                }
            }
        }
        internal static void List(ModEntry me, string[] args, IModHelper Helper, ModConfig Config)
        {
            if (!Context.IsWorldReady && !args.Contains("debug"))
            {
                me.Monitor.Log(Helper.Translation.Get("CLI.nosaveloaded"), LogLevel.Error);
                return;
            }
            else if (args.Count<string>() > 1 && args[1] is not "debug")
            {
                me.Monitor.Log(Helper.Translation.Get($"CLI.TooManyArguments"), LogLevel.Info);
            }
            else if (!args.Any() || args[0] is "help" || args[0] is null)
            {
                me.Monitor.Log(Helper.Translation.Get("CLI.list.description"), LogLevel.Info);
            }
            else
            {
                if (args[0] is "schedules" || args[0] is "s")
                {
                    string tempsched = "";
                    foreach (string s in me.SchedulesEdited)
                    {
                        tempsched = tempsched + $"\n   {s}";
                    }
                    me.Monitor.Log($"\n{Helper.Translation.Get("CLI.get.schedules")}\n{tempsched}", LogLevel.Info);
                }
                else if (args[0] is "dialogues" || args[0] is "d")
                {
                    string tempdial = "";
                    foreach (string s in me.DialoguesEdited)
                    {
                        tempdial = tempdial + $"\n   {s}";
                    }
                    me.Monitor.Log($"\n{Helper.Translation.Get("CLI.get.dialogues")}\n{tempdial}", LogLevel.Info);
                }
                else if (args[0] is "packs" || args[0] is "p")
                {
                    string tempkeys = "";
                    foreach (string s in ModEntry.CustomSchedule.Keys)
                    {
                        tempkeys = tempkeys + $"\n   {s}";
                    }
                    me.Monitor.Log($"{Helper.Translation.Get("CLI.get.packs")}\n{tempkeys}", LogLevel.Info);
                }
                else if (args[0] is "translations" || args[0] is "translation" || args[0] is "tl")
                {
                    string temptl = "";
                    foreach (string s in me.TranslationsAdded)
                    {
                        temptl = temptl + $"\n   {s}";
                    }
                    me.Monitor.Log($"{Helper.Translation.Get("CLI.get.packs")}\n{temptl}", LogLevel.Info);
                }
                else if (args[0] is "internal" || args[0] is "i")
                {
                    me.Monitor.Log($"Internal info: \n\n IsLeahMarried = {me.IsLeahMarried}; \n\n IsElliottMarried = {me.IsElliottMarried}; \n\n IsKrobusRoommate = {me.IsKrobusRoommate}; \n\n currentLang = {me.currentLang}; \n\n PreviousDayRandom = {me.PreviousDayRandom}; \n\n CCC = {me.CCC}; \n\n SawDevan4H = {me.SawDevan4H}; \n\n HasSVE = {me.HasSVE}; \n\n HasC2N = {me.HasC2N}; \n\n HasExGIM = {me.HasExGIM};", LogLevel.Info);
                }
                else if (args[0] is "married" || args[0] is "m" || args[0] is "im")
                {
                    string tempM = "";
                    foreach (KeyValuePair<string, bool> kvp in me.MarriedtoNPC)
                    {
                        tempM = tempM + $"\n   {kvp.Key}: {kvp.Value}";
                    }
                    me.Monitor.Log($"Is this character married?: \n{tempM}", LogLevel.Info);
                }
                else if (args[0] is "playerconfig" || args[0] is "pc")
                {
                    me.Monitor.Log($"Config: \n\n CustomChance: {Config.CustomChance}; \n\n ScheduleRandom = {Config.ScheduleRandom}; \n\n CustomRoom = {Config.CustomRoom}; \n\n BOOLSHERE \n\n Childbedcolor = {Config.Childbedcolor}; \n\n NPCDevan = {Config.NPCDevan}; \n\n Allow_Children = {Config.Allow_Children};", LogLevel.Info);
                }
                else
                {
                    me.Monitor.Log(Helper.Translation.Get($"CLI.InvalidValue"), LogLevel.Error);
                }
            }
        }
        internal static void Chance(ModEntry me, string[] args, IModHelper Helper, ModConfig Config)
        {
            if (!args.Any())
            {
                me.Monitor.Log($"{me.RandomizedInt}", LogLevel.Info);
            }
            else if (args.Contains<string>("debug"))
            {
                if (!Context.IsWorldReady)
                {
                    me.Monitor.Log(Helper.Translation.Get("CLI.nosaveloaded"), LogLevel.Error);
                    return;
                }
                else
                {
                    me.Monitor.Log(Helper.Translation.Get("CLI.Day0") + $": {me.PreviousDayRandom}", LogLevel.Info);
                    me.Monitor.Log(Helper.Translation.Get("CLI.Day1") + $": {me.RandomizedInt}", LogLevel.Info);
                }
            }
            else if (args[0] is "set" && args[1].All(char.IsDigit))
            {
                var value = int.Parse(args[1]);
                if (value >= 0 && value <= 100)
                {
                    Config.CustomChance = value;
                    me.Monitor.Log(Helper.Translation.Get($"CLI.ChangingCC") + Config.CustomChance + "%...", LogLevel.Info);
                }
                else
                {
                    me.Monitor.Log(Helper.Translation.Get($"CLI.InvalidValue.CC"), LogLevel.Error);
                }
            }
            else
            {
                me.Monitor.Log(Helper.Translation.Get($"CLI.InvalidValue"), LogLevel.Error);
            }
        }
    }
}
