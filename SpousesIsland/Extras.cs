using SpousesIsland.Framework;
using StardewModdingAPI;
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
    public class Debugging : ModEntry
    {
        private readonly ModConfig Config;
        internal void SGI_About(string command, string[] args)
        {
            if (currentLang is "es")
            {
                this.Monitor.Log("Este mod permite que tu pareja vaya a la isla (compatible con ChildToNPC, SVE y otros). También permite crear paquetes de contenido / agregar rutinas personalizadas.\nMod creado por mistyspring (nexusmods)", LogLevel.Info);
            }
            else
            {
                this.Monitor.Log("This mod allows your spouse to visit the Island (compatible with ChildToNPC, SVE, Free Love and a few others). It's also a framework, so you can add custom schedules.\nMod created by mistyspring (nexusmods)", LogLevel.Info);
            }
        }
        internal void SGI_List(string command, string[] args)
        {

            if (!Context.IsWorldReady && !args.Contains("debug"))
            {
                this.Monitor.Log(Helper.Translation.Get("CLI.nosaveloaded"), LogLevel.Error);
                return;
            }
            else if (args.Count<string>() > 1 && args[1] is not "debug")
            {
                this.Monitor.Log(Helper.Translation.Get($"CLI.TooManyArguments"), LogLevel.Info);
            }
            else if (!args.Any() || args[0] is "help" || args[0] is null)
            {
                this.Monitor.Log(Helper.Translation.Get("CLI.list.description"), LogLevel.Info);
            }
            else
            {
                if (args[0] is "schedules" || args[0] is "s")
                {
                    string tempsched = "";
                    foreach (string s in SchedulesEdited)
                    {
                        tempsched = tempsched + $"\n   {s}";
                    }
                    this.Monitor.Log($"\n{Helper.Translation.Get("CLI.get.schedules")}\n{tempsched}", LogLevel.Info);
                }
                else if (args[0] is "dialogues" || args[0] is "d")
                {
                    string tempdial = "";
                    foreach (string s in DialoguesEdited)
                    {
                        tempdial = tempdial + $"\n   {s}";
                    }
                    this.Monitor.Log($"\n{Helper.Translation.Get("CLI.get.dialogues")}\n{tempdial}", LogLevel.Info);
                }
                else if (args[0] is "packs" || args[0] is "p")
                {
                    string tempkeys = "";
                    foreach (string s in CustomSchedule.Keys)
                    {
                        tempkeys = tempkeys + $"\n   {s}";
                    }
                    this.Monitor.Log($"{Helper.Translation.Get("CLI.get.packs")}\n{tempkeys}", LogLevel.Info);
                }
                else if (args[0] is "translations" || args[0] is "translation" || args[0] is "tl")
                {
                    string temptl = "";
                    foreach (string s in TranslationsAdded)
                    {
                        temptl = temptl + $"\n   {s}";
                    }
                    this.Monitor.Log($"{Helper.Translation.Get("CLI.get.packs")}\n{temptl}", LogLevel.Info);
                }
                else if (args[0] is "internal" || args[0] is "i")
                {
                    this.Monitor.Log($"Internal info: \n\n IsLeahMarried = {IsLeahMarried}; \n\n IsElliottMarried = {IsElliottMarried}; \n\n IsKrobusRoommate = {IsKrobusRoommate}; \n\n currentLang = {currentLang}; \n\n PreviousDayRandom = {PreviousDayRandom}; \n\n CCC = {CCC}; \n\n SawDevan4H = {SawDevan4H}; \n\n HasSVE = {HasSVE}; \n\n HasC2N = {HasC2N}; \n\n HasExGIM = {HasExGIM};", LogLevel.Info);
                }
                else if (args[0] is "married" || args[0] is "m" || args[0] is "im")
                {
                    string tempM = "";
                    foreach (KeyValuePair<string, bool> kvp in MarriedtoNPC)
                    {
                        tempM = tempM + $"\n   {kvp.Key}: {kvp.Value}";
                    }
                    this.Monitor.Log($"Is this character married?: \n{tempM}", LogLevel.Info);
                }
                else if (args[0] is "playerconfig" || args[0] is "pc")
                {
                    this.Monitor.Log($"Config: \n\n CustomChance: {Config.CustomChance}; \n\n ScheduleRandom = {Config.ScheduleRandom}; \n\n CustomRoom = {Config.CustomRoom}; \n\n BOOLSHERE \n\n Childbedcolor = {Config.Childbedcolor}; \n\n NPCDevan = {Config.NPCDevan}; \n\n Allow_Children = {Config.Allow_Children};", LogLevel.Info);
                }
                else
                {
                    this.Monitor.Log(Helper.Translation.Get($"CLI.InvalidValue"), LogLevel.Error);
                }
            }
        }
        internal void SGI_Chance(string command, string[] args)
        {

            if (!args.Any())
            {
                this.Monitor.Log($"{RandomizedInt}", LogLevel.Info);
            }
            else if (args.Contains<string>("debug"))
            {
                if (!Context.IsWorldReady)
                {
                    this.Monitor.Log(Helper.Translation.Get("CLI.nosaveloaded"), LogLevel.Error);
                    return;
                }
                else
                {
                    this.Monitor.Log(Helper.Translation.Get("CLI.Day0") + $": {PreviousDayRandom}", LogLevel.Info);
                    this.Monitor.Log(Helper.Translation.Get("CLI.Day1") + $": {RandomizedInt}", LogLevel.Info);
                }
            }
            else if (args[0] is "set" && args[1].All(char.IsDigit))
            {
                var value = int.Parse(args[1]);
                if (value >= 0 && value <= 100)
                {
                    Config.CustomChance = value;
                    this.Monitor.Log(Helper.Translation.Get($"CLI.ChangingCC") + Config.CustomChance + "%...", LogLevel.Info);
                }
                else
                {
                    this.Monitor.Log(Helper.Translation.Get($"CLI.InvalidValue.CC"), LogLevel.Error);
                }
            }
            else
            {
                this.Monitor.Log(Helper.Translation.Get($"CLI.InvalidValue"), LogLevel.Error);
            }
        }
        internal void SGI_Reset(string command, string[] args)
        {

            if (!Context.IsWorldReady && !args.Contains("debug"))
            {
                this.Monitor.Log(Helper.Translation.Get("CLI.nosaveloaded"), LogLevel.Error);
                return;
            }
            else if (args.Count<string>() > 1 && args[1] is not "debug")
            {
                this.Monitor.Log(Helper.Translation.Get($"CLI.TooManyArguments"), LogLevel.Info);
            }
            else if (!args.Any() || args[0] is "help" || args[0] is "h")
            {
                this.Monitor.Log(Helper.Translation.Get("CLI.reset.description"), LogLevel.Info);
            }
            else
            {
                if (args[0] is "schedules" || args[0] is "s")
                {
                    int ResetCounter = 0;
                    foreach (string str in CustomSchedule.Keys)
                    {
                        Helper.GameContent.InvalidateCache($"Characters/schedules/{str}");
                        ResetCounter++;
                    }
                    this.Monitor.Log($"Reloaded {ResetCounter} schedules.", LogLevel.Info);
                }
                else if (args[0] is "dialogues" || args[0] is "d")
                {
                    int ResetCounter = 0;
                    int TlCounter = 0;
                    foreach (ContentPackData cpd in CustomSchedule.Values)
                    {
                        Helper.GameContent.InvalidateCache($"Characters/Dialogue/{cpd.Spousename}");
                        foreach (DialogueTranslation tl in cpd.Translations)
                        {
                            Helper.GameContent.InvalidateCache($"Characters/schedules/{cpd?.Spousename}{Commands.ParseLangCode(tl?.Key)}");
                            TlCounter++;
                        }
                        ResetCounter++;
                    }
                    this.Monitor.Log($"Reloaded {ResetCounter} default dialogues and {TlCounter} translations.", LogLevel.Info);
                }
                else if (args[0] is "packs" || args[0] is "p")
                {
                    this.Monitor.Log(Helper.Translation.Get($"CLI.MustReset"), LogLevel.Warn);
                }
                else
                {
                    this.Monitor.Log(Helper.Translation.Get($"CLI.InvalidValue"), LogLevel.Error);
                }
            }
        }
        internal void SGI_Help(string command, string[] args)
        {
            this.Monitor.Log(this.Helper.Translation.Get("CLI.helpdescription"), LogLevel.Info);
        }
    }
}