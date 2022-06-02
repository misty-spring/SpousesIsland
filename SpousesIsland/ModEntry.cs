using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using GenericModConfigMenu;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Characters;
using SpousesIsland.Framework;

namespace SpousesIsland
{
    class ModConfig
    {
        public int CustomChance { get; set; } = 10;
        public bool ScheduleRandom { get; set; } = false;
        public bool CustomRoom { get; set; } = false;
        public string Childbedcolor { get; set; } = "1";
        public bool NPCDevan { get; set; } = false;
        public bool Allow_Children { get; set; } = true;
        public bool Allow_Abigail { get; set; } = true;
        public bool Allow_Alex { get; set; } = true;
        public bool Allow_Elliott { get; set; } = true;
        public bool Allow_Emily { get; set; } = true;
        public bool Allow_Haley { get; set; } = true;
        public bool Allow_Harvey { get; set; } = true;
        public bool Allow_Krobus { get; set; } = true;
        public bool Allow_Leah { get; set; } = true;
        public bool Allow_Maru { get; set; } = true;
        public bool Allow_Penny { get; set; } = true;
        public bool Allow_Sam { get; set; } = true;
        public bool Allow_Sebastian { get; set; } = true;
        public bool Allow_Shane { get; set; } = true;
        public bool Allow_Claire { get; set; } = true;
        public bool Allow_Lance { get; set; } = true;
        public bool Allow_Magnus { get; set; } = true;
        public bool Allow_Olivia { get; set; } = true;
        public bool Allow_Sophia { get; set; } = true;
        public bool Allow_Victor { get; set; } = true;
        /* (experimental) child-customizing config*/
        //child 1
        public string Child1_L1 { get; set; } = "IslandWest";
        public int Child1_X1 { get; set; } = 74;
        public int Child1_Y1 { get; set; } = 43;
        public string Child1_L2 { get; set; } = "IslandWest";
        public int Child1_X2 { get; set; } = 83;
        public int Child1_Y2 { get; set; } = 36;
        public string Child1_L3 { get; set; } = "IslandWest";
        public int Child1_X3 { get; set; } = 91;
        public int Child1_Y3 { get; set; } = 37;
        //child 2
        public string Child2_L1 { get; set; } = "IslandWest";
        public int Child2_X1 { get; set; } = 75;
        public int Child2_Y1 { get; set; } = 46;
        public string Child2_L2 { get; set; } = "IslandWest";
        public int Child2_X2 { get; set; } = 84;
        public int Child2_Y2 { get; set; } = 38;
        public string Child2_L3 { get; set; } = "IslandWest";
        public int Child2_X3 { get; set; } = 93;
        public int Child2_Y3 { get; set; } = 36;
        //debug
        public bool Verbose { get; set; } = false;
        public bool Debug { get; set; } = false;
    }
    public class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
            helper.Events.Content.AssetRequested += this.OnAssetRequested;
            helper.Events.GameLoop.SaveLoaded += this.OnSaveLoaded;
            helper.Events.GameLoop.DayEnding += this.OnDayEnding;
            Helper.Events.GameLoop.DayStarted += this.OnDayStarted;
            helper.Events.GameLoop.TimeChanged += this.OnTimeChanged;
            helper.Events.Content.LocaleChanged += this.OnLocaleChanged;

            this.Config = this.Helper.ReadConfig<ModConfig>();

            //??
            int RandomizedInt = this.RandomizedInt;
            //commands
            if (Config.Debug is true)
            {
                helper.ConsoleCommands.Add("sgi_help", helper.Translation.Get("CLI.help"), this.SGI_Help);
                helper.ConsoleCommands.Add("sgi_chance", helper.Translation.Get("CLI.chance"), this.SGI_Chance);
                helper.ConsoleCommands.Add("sgi_reset", helper.Translation.Get("CLI.reset"), this.SGI_Reset);
                helper.ConsoleCommands.Add("sgi_list", helper.Translation.Get("CLI.list"), this.SGI_List);
                helper.ConsoleCommands.Add("sgi_about", helper.Translation.Get("CLI.about"), this.SGI_About);
            }
        }

        [XmlIgnore]
        public int RandomizedInt;
        public static Dictionary<string, ContentPackData> CustomSchedule = new();

        /* Private
         * Can only be accessed by this mod.
         * Contents: Events called by Entry, player config, static Random.
         */
        private void OnGameLaunched(object sender, GameLaunchedEventArgs e)
        {
            HasSVE = Commands.HasMod("FlashShifter.StardewValleyExpandedCP", this.Helper);
            HasC2N = Commands.HasMod("Loe2run.ChildToNPC", this.Helper);
            HasExGIM = Commands.HasMod("mistyspring.extraGImaps", this.Helper);
            if (Config.Verbose == true)
            {
                this.Monitor.Log($"\n  HasSVE = {HasSVE}\n   HasC2N = {HasC2N}\n   HasExGIM = {HasExGIM}");
            }
            RandomizedInt = Random.Next(1, 101);
            //empty lists preemptively
            if (SchedulesEdited.Count is not 0)
            {
                this.Monitor.Log("Resetting schedule list...", LogLevel.Trace);
                SchedulesEdited.RemoveRange(0, SchedulesEdited.Count);
            }
            if (DialoguesEdited.Count is not 0)
            {
                this.Monitor.Log("Resetting dialogue list...", LogLevel.Trace);
                DialoguesEdited.RemoveRange(0, DialoguesEdited.Count);
            }
            // get Generic Mod Config Menu's API (if it's installed)
            var configMenu = this.Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (configMenu is null)
                return;
            // register mod
            configMenu.Register(
                mod: this.ModManifest,
                reset: () => this.Config = new ModConfig(),
                save: () => this.Helper.WriteConfig(this.Config)
            );

            // basic config options
            configMenu.AddNumberOption(
                mod: this.ModManifest,
                name: () => this.Helper.Translation.Get("config.CustomChance.name"),
                tooltip: () => this.Helper.Translation.Get("config.CustomChance.description"),
                getValue: () => this.Config.CustomChance,
                setValue: value => this.Config.CustomChance = value,
                min: 0,
                max: 100,
                interval: 1
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => this.Helper.Translation.Get("config.CustomRoom.name"),
                tooltip: () => this.Helper.Translation.Get("config.CustomRoom.description"),
                getValue: () => this.Config.CustomRoom,
                setValue: value => this.Config.CustomRoom = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => this.Helper.Translation.Get("config.Devan_Nosit.name"),
                tooltip: () => this.Helper.Translation.Get("config.Devan_Nosit.description"),
                getValue: () => this.Config.NPCDevan,
                setValue: value => this.Config.NPCDevan = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => this.Helper.Translation.Get("config.ScheduleRandom.name"),
                tooltip: () => this.Helper.Translation.Get("config.ScheduleRandom.description"),
                getValue: () => this.Config.ScheduleRandom,
                setValue: value => this.Config.ScheduleRandom = value
            );
            //links to config pages
            configMenu.AddPageLink(
                mod: this.ModManifest,
                pageId: "advancedConfig",
                text: () => this.Helper.Translation.Get("config.advancedConfig.name"),
                tooltip: () => this.Helper.Translation.Get("config.advancedConfig.description")
            );
            if (HasC2N is true)
            {
                configMenu.AddPageLink(
                    mod: this.ModManifest,
                    pageId: "C2Nconfig",
                    text: () => "Child2NPC...",
                    tooltip: () => this.Helper.Translation.Get("config.Child2NPC.description")
                );
                configMenu.AddPage(
                    mod: this.ModManifest,
                    pageId: "C2Nconfig",
                    pageTitle: () => "Child2NPC..."
                );
                configMenu.AddBoolOption(
                    mod: this.ModManifest,
                    name: () => this.Helper.Translation.Get("config.ChildVisitIsland.name"),
                    tooltip: () => this.Helper.Translation.Get("config.ChildVisitIsland.description"),
                    getValue: () => this.Config.Allow_Children,
                    setValue: value => this.Config.Allow_Children = value
                );
                configMenu.AddTextOption(
                    mod: this.ModManifest,
                    name: () => this.Helper.Translation.Get("config.Childbedcolor.name"),
                    tooltip: () => this.Helper.Translation.Get("config.Childbedcolor.description"),
                    getValue: () => this.Config.Childbedcolor,
                    setValue: value => this.Config.Childbedcolor = value,
                    allowedValues: new string[] { "1", "2", "3", "4", "5", "6" }
                );
                configMenu.AddImage(
                    mod: this.ModManifest,
                    texture: KbcSamples,
                    texturePixelArea: null,
                    scale: 1
                );
                /*advanced schedule editing starts here. first adds links, then page of child 1 schedule, then child 2
                * every schedule has 3 locations, so it's -location string, location x int, location y int. and so on for all 3 locs*/
                configMenu.AddPageLink(
                    mod: this.ModManifest,
                    pageId: "Child1_Schedule",
                    text: () => this.Helper.Translation.Get("config.Child1Sch.name"),
                    tooltip: () => this.Helper.Translation.Get("config.Child1Sch.description")
                );
                configMenu.AddPageLink(
                    mod: this.ModManifest,
                    pageId: "Child2_Schedule",
                    text: () => this.Helper.Translation.Get("config.Child2Sch.name"),
                    tooltip: () => this.Helper.Translation.Get("config.Child2Sch.description")
                );
                configMenu.AddPage(
                    mod: this.ModManifest,
                    pageId: "Child1_Schedule",
                    pageTitle: () => this.Helper.Translation.Get("config.Child1Sch.name")
                );
                configMenu.AddTextOption(
                    mod: this.ModManifest,
                    name: () => this.Helper.Translation.Get("config.Child_L1"),
                    tooltip: () => null,
                    getValue: () => this.Config.Child1_L1,
                    setValue: value => this.Config.Child1_L1 = value,
                    allowedValues: new string[] { "IslandNorth", "IslandWest", "IslandSouth", "IslandSouthEast", "IslandEast", "IslandFarmHouse", "IslandShrine", "IslandFarmCave", "IslandWestCave1", "IslandSouthEastCave", "CaptainRoom", "IslandFieldOffice" }
                );
                configMenu.AddNumberOption(
                    mod: this.ModManifest,
                    name: () => this.Helper.Translation.Get("config.Child_X"),
                    tooltip: () => null,
                    getValue: () => this.Config.Child1_X1,
                    setValue: value => this.Config.Child1_X1 = value,
                    interval: 1
                );
                configMenu.AddNumberOption(
                    mod: this.ModManifest,
                    name: () => this.Helper.Translation.Get("config.Child_Y"),
                    tooltip: () => null,
                    getValue: () => this.Config.Child1_Y1,
                    setValue: value => this.Config.Child1_Y1 = value,
                    interval: 1
                );
                configMenu.AddTextOption(
                    mod: this.ModManifest,
                    name: () => this.Helper.Translation.Get("config.Child_L2"),
                    tooltip: () => null,
                    getValue: () => this.Config.Child1_L2,
                    setValue: value => this.Config.Child1_L2 = value,
                    allowedValues: new string[] { "IslandNorth", "IslandWest", "IslandSouth", "IslandSouthEast", "IslandEast", "IslandFarmHouse", "IslandShrine", "IslandFarmCave", "IslandWestCave1", "IslandSouthEastCave", "CaptainRoom", "IslandFieldOffice" }
                );
                configMenu.AddNumberOption(
                    mod: this.ModManifest,
                    name: () => this.Helper.Translation.Get("config.Child_X"),
                    tooltip: () => null,
                    getValue: () => this.Config.Child1_X2,
                    setValue: value => this.Config.Child1_X2 = value,
                    interval: 1
                );
                configMenu.AddNumberOption(
                    mod: this.ModManifest,
                    name: () => this.Helper.Translation.Get("config.Child_Y"),
                    tooltip: () => null,
                    getValue: () => this.Config.Child1_Y2,
                    setValue: value => this.Config.Child1_Y2 = value,
                    interval: 1
                );
                configMenu.AddTextOption(
                    mod: this.ModManifest,
                    name: () => this.Helper.Translation.Get("config.Child_L3"),
                    tooltip: () => null,
                    getValue: () => this.Config.Child1_L3,
                    setValue: value => this.Config.Child1_L3 = value,
                    allowedValues: new string[] { "IslandNorth", "IslandWest", "IslandSouth", "IslandSouthEast", "IslandEast", "IslandFarmHouse", "IslandShrine", "IslandFarmCave", "IslandWestCave1", "IslandSouthEastCave", "CaptainRoom", "IslandFieldOffice" }
                );
                configMenu.AddNumberOption(
                    mod: this.ModManifest,
                    name: () => this.Helper.Translation.Get("config.Child_X"),
                    tooltip: () => null,
                    getValue: () => this.Config.Child1_X3,
                    setValue: value => this.Config.Child1_X3 = value,
                    interval: 1
                );
                configMenu.AddNumberOption(
                    mod: this.ModManifest,
                    name: () => this.Helper.Translation.Get("config.Child_Y"),
                    tooltip: () => null,
                    getValue: () => this.Config.Child1_Y3,
                    setValue: value => this.Config.Child1_Y3 = value,
                    interval: 1
                );
                configMenu.AddPage(
                    mod: this.ModManifest,
                    pageId: "Child2_Schedule",
                    pageTitle: () => this.Helper.Translation.Get("config.Child2Sch.name")
                );
                configMenu.AddTextOption(
                    mod: this.ModManifest,
                    name: () => this.Helper.Translation.Get("config.Child_L1"),
                    tooltip: () => null,
                    getValue: () => this.Config.Child2_L1,
                    setValue: value => this.Config.Child2_L1 = value,
                    allowedValues: new string[] { "IslandNorth", "IslandWest", "IslandSouth", "IslandSouthEast", "IslandEast", "IslandFarmHouse", "IslandShrine", "IslandFarmCave", "IslandWestCave1", "IslandSouthEastCave", "CaptainRoom", "IslandFieldOffice" }
                );
                configMenu.AddNumberOption(
                    mod: this.ModManifest,
                    name: () => this.Helper.Translation.Get("config.Child_X"),
                    tooltip: () => null,
                    getValue: () => this.Config.Child2_X1,
                    setValue: value => this.Config.Child2_X1 = value,
                    interval: 1
                );
                configMenu.AddNumberOption(
                    mod: this.ModManifest,
                    name: () => this.Helper.Translation.Get("config.Child_Y"),
                    tooltip: () => null,
                    getValue: () => this.Config.Child2_Y1,
                    setValue: value => this.Config.Child2_Y1 = value,
                    interval: 1
                );
                configMenu.AddTextOption(
                    mod: this.ModManifest,
                    name: () => this.Helper.Translation.Get("config.Child_L2"),
                    tooltip: () => null,
                    getValue: () => this.Config.Child2_L2,
                    setValue: value => this.Config.Child2_L2 = value,
                    allowedValues: new string[] { "IslandNorth", "IslandWest", "IslandSouth", "IslandSouthEast", "IslandEast", "IslandFarmHouse", "IslandShrine", "IslandFarmCave", "IslandWestCave1", "IslandSouthEastCave", "CaptainRoom", "IslandFieldOffice" }
                );
                configMenu.AddNumberOption(
                    mod: this.ModManifest,
                    name: () => this.Helper.Translation.Get("config.Child_X"),
                    tooltip: () => null,
                    getValue: () => this.Config.Child2_X2,
                    setValue: value => this.Config.Child2_X2 = value,
                    interval: 1
                );
                configMenu.AddNumberOption(
                    mod: this.ModManifest,
                    name: () => this.Helper.Translation.Get("config.Child_Y"),
                    tooltip: () => null,
                    getValue: () => this.Config.Child2_Y2,
                    setValue: value => this.Config.Child2_Y2 = value,
                    interval: 1
                );
                configMenu.AddTextOption(
                    mod: this.ModManifest,
                    name: () => this.Helper.Translation.Get("config.Child_L3"),
                    tooltip: () => null,
                    getValue: () => this.Config.Child2_L3,
                    setValue: value => this.Config.Child2_L3 = value,
                    allowedValues: new string[] { "IslandNorth", "IslandWest", "IslandSouth", "IslandSouthEast", "IslandEast", "IslandFarmHouse", "IslandShrine", "IslandFarmCave", "IslandWestCave1", "IslandSouthEastCave", "CaptainRoom", "IslandFieldOffice" }
                );
                configMenu.AddNumberOption(
                    mod: this.ModManifest,
                    name: () => this.Helper.Translation.Get("config.Child_X"),
                    tooltip: () => null,
                    getValue: () => this.Config.Child2_X3,
                    setValue: value => this.Config.Child2_X3 = value,
                    interval: 1
                );
                configMenu.AddNumberOption(
                    mod: this.ModManifest,
                    name: () => this.Helper.Translation.Get("config.Child_Y"),
                    tooltip: () => null,
                    getValue: () => this.Config.Child2_Y3,
                    setValue: value => this.Config.Child2_Y3 = value,
                    interval: 1
                );
            }
            //adv. config
            configMenu.AddPage(
                mod: this.ModManifest,
                pageId: "advancedConfig",
                pageTitle: () => this.Helper.Translation.Get("config.advancedConfig.name")
            );
            configMenu.AddSectionTitle(
                mod: this.ModManifest,
                text: Titles.SpouseT,
                tooltip: SpouseD
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Abigail",
                tooltip: () => null,
                getValue: () => this.Config.Allow_Abigail,
                setValue: value => this.Config.Allow_Abigail = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Alex",
                tooltip: () => null,
                getValue: () => this.Config.Allow_Alex,
                setValue: value => this.Config.Allow_Alex = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Elliott",
                tooltip: () => null,
                getValue: () => this.Config.Allow_Elliott,
                setValue: value => this.Config.Allow_Elliott = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Emily",
                tooltip: () => null,
                getValue: () => this.Config.Allow_Emily,
                setValue: value => this.Config.Allow_Emily = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Haley",
                tooltip: () => null,
                getValue: () => this.Config.Allow_Haley,
                setValue: value => this.Config.Allow_Haley = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Harvey",
                tooltip: () => null,
                getValue: () => this.Config.Allow_Harvey,
                setValue: value => this.Config.Allow_Harvey = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Krobus",
                tooltip: () => null,
                getValue: () => this.Config.Allow_Krobus,
                setValue: value => this.Config.Allow_Krobus = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Leah",
                tooltip: () => null,
                getValue: () => this.Config.Allow_Leah,
                setValue: value => this.Config.Allow_Leah = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Maru",
                tooltip: () => null,
                getValue: () => this.Config.Allow_Maru,
                setValue: value => this.Config.Allow_Maru = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Penny",
                tooltip: () => null,
                getValue: () => this.Config.Allow_Penny,
                setValue: value => this.Config.Allow_Penny = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Sam",
                tooltip: () => null,
                getValue: () => this.Config.Allow_Sam,
                setValue: value => this.Config.Allow_Sam = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Sebastian",
                tooltip: () => null,
                getValue: () => this.Config.Allow_Sebastian,
                setValue: value => this.Config.Allow_Sebastian = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Shane",
                tooltip: () => null,
                getValue: () => this.Config.Allow_Shane,
                setValue: value => this.Config.Allow_Shane = value
            );
            configMenu.AddSectionTitle(
                mod: this.ModManifest,
                text: Titles.SVET,
                tooltip: null
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Claire",
                tooltip: () => this.Helper.Translation.Get("config.RequiresSVE"),
                getValue: () => this.Config.Allow_Claire,
                setValue: value => this.Config.Allow_Claire = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Lance",
                tooltip: () => this.Helper.Translation.Get("config.RequiresSVE"),
                getValue: () => this.Config.Allow_Lance,
                setValue: value => this.Config.Allow_Lance = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Magnus",
                tooltip: () => this.Helper.Translation.Get("config.RequiresSVE"),
                getValue: () => this.Config.Allow_Magnus,
                setValue: value => this.Config.Allow_Magnus = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Olivia",
                tooltip: () => this.Helper.Translation.Get("config.RequiresSVE"),
                getValue: () => this.Config.Allow_Olivia,
                setValue: value => this.Config.Allow_Olivia = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Sophia",
                tooltip: () => this.Helper.Translation.Get("config.RequiresSVE"),
                getValue: () => this.Config.Allow_Sophia,
                setValue: value => this.Config.Allow_Sophia = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Victor",
                tooltip: () => this.Helper.Translation.Get("config.RequiresSVE"),
                getValue: () => this.Config.Allow_Victor,
                setValue: value => this.Config.Allow_Victor = value
            );
            configMenu.AddSectionTitle(
                mod: this.ModManifest,
                text: Titles.Debug,
                tooltip: null
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => this.Helper.Translation.Get("config.DebugComm.name"),
                tooltip: () => this.Helper.Translation.Get("config.DebugComm.description"),
                getValue: () => this.Config.Debug,
                setValue: value => this.Config.Debug = value
            );
            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => this.Helper.Translation.Get("config.Verbose.name"),
                tooltip: () => this.Helper.Translation.Get("config.Verbose.description"),
                getValue: () => this.Config.Verbose,
                setValue: value => this.Config.Verbose = value
            );

            ContentPackData data = new ContentPackData();
            //this.Helper.Data.WriteJsonFile("ContentTemplate.json", data);
            foreach (IContentPack contentPack in this.Helper.ContentPacks.GetOwned())
            {
                this.Monitor.Log($"Reading content pack: {contentPack.Manifest.Name} {contentPack.Manifest.Version} from {contentPack.DirectoryPath}", LogLevel.Debug);
                if (!contentPack.HasFile("content.json"))
                {
                    // show 'required file missing' error
                    this.Monitor.Log(Helper.Translation.Get($"FW.contentpack.error"), LogLevel.Warn);
                }
                ContentPackObject obj = contentPack.ReadJsonFile<ContentPackObject>("content.json");
                foreach (var cpd in obj.data)
                {
                    //in the future, replace spouse for "character" and then add bool "ischild"? or smth similar, to allow custom kid schedules
                    if (SGIValues.CheckSpouseName(cpd.Spousename))
                    {
                        this.Monitor.Log($"{contentPack.Manifest.Name} is trying to add a schedule for {cpd.Spousename}. To avoid any conflicts, please untick '{cpd.Spousename}' from Advanced config.", LogLevel.Warn);
                    }
                    //error logs
                    if (Commands.ParseContentPack(cpd, this.Monitor))
                    {
                        this.Monitor.Log(string.Format(Helper.Translation.Get($"FW.contentpack.oneOrMoreErrors"), $"{contentPack.Manifest.Name}"), LogLevel.Error);
                    }
                    else
                    {
                        CustomSchedule.Add(cpd.Spousename, cpd);
                        this.Monitor.Log($"Added {cpd.Spousename} schedule", LogLevel.Debug);
                    }
                }
            }

        }
        private void OnAssetRequested(object sender, AssetRequestedEventArgs e)
        {
            /*Format:
             * ("Word", if partial OK (e.g Word1), if subfolder OK (e.g Word/Sub/file)
             * 
             * Everything is put inside this bool to make sure the game doesn't lag during 10min updates (bc for some reason it continues loading files post saveload)
             * "!Context.IsWorldReady || " taken out, it just checks for whether it can load
             */
            if (CanDoHeavyLoad is true)
            {
                if(Config.CustomChance >= RandomizedInt)
                {
                    if (e.Name.StartsWith("Characters/schedules/", false, true))
                    {
                        AssetRequest.ChangeSchedules(this, e, Random, Config);
                    }
                    foreach (ContentPackData cpd in CustomSchedule.Values)
                    {
                        AssetRequest.ContentPackSchedule(this, e, Random, cpd);
                    }
                }

                if (e.Name.StartsWith("Maps/", false, true))
                {
                    AssetRequest.Maps(e, Helper);
                }
                if (e.Name.StartsWith("Characters/", false, true))
                {
                    if (e.Name.StartsWith("Characters/", false, false))
                    {
                        AssetRequest.CharacterSheets(this, e, Helper, Config.CustomChance);
                    }
                    if (e.Name.StartsWith("Characters/Dialogue/", false, true)) { AssetRequest.Dialogue(this, e, Helper); }
                    foreach (ContentPackData cpd in CustomSchedule.Values)
                    {
                        if (e.NameWithoutLocale.IsEquivalentTo($"Characters/Dialogue/{cpd.Spousename}"))
                            AssetRequest.ContentPackDialogue(this, e, CustomSchedule, cpd);
                    }
                }
                if (e.Name.StartsWith("Portraits/", false, true))
                {
                    if (e.Name.IsEquivalentTo("Portraits/Devan"))
                    {
                        e.LoadFromModFile<Texture2D>("assets/Devan/Portrait.png", AssetLoadPriority.Medium);
                    }
                    if (e.Name.IsEquivalentTo("Portraits/Krobus") && EnabledSpouses.Contains("Krobus") && Config.CustomChance >= RandomizedInt)
                    {
                        e.LoadFromModFile<Texture2D>("assets/Spouses/Krobus_Outside_Portrait.png", AssetLoadPriority.Medium);
                    }
                }
                if (Config.NPCDevan == true)
                {
                    AssetRequest.Devan(this, e);
                    if (e.Name.IsEquivalentTo("Maps/Saloon"))
                    {
                        if (HasSVE is false)
                            e.Edit(asset => AssetRequest.SVESaloon(asset, Helper));
                        else
                            e.Edit(asset => AssetRequest.NormalSaloon(asset, Helper));
                        if (SawDevan4H == true)
                            e.Edit(asset => AssetRequest.PictureInRoom(asset, Helper));
                    }
                    if (e.Name.IsEquivalentTo("Characters/schedules/Devan"))
                    {
                        if (Children.Count is not 0)
                        {
                            if (Config.CustomChance >= RandomizedInt && Children.Count >= 1)
                            {
                                e.LoadFromModFile<Dictionary<string, string>>("assets/Devan/Schedule_Babysit.json", AssetLoadPriority.Medium);
                            }
                            if (Config.CustomChance < RandomizedInt && Children.Count >= 1)
                            {
                                e.LoadFromModFile<Dictionary<string, string>>("assets/Devan/Schedule_Normal.json", AssetLoadPriority.Medium);
                            }
                        }
                        else
                        {
                            e.LoadFromModFile<Dictionary<string, string>>("assets/Devan/Schedule_Normal.json", AssetLoadPriority.Medium);
                        }
                        if (CCC == true)
                        {
                            e.Edit(asset =>
                            {
                                IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                data["Wed"] = "640 Saloon 39 7 2/650 Saloon 35 8 2/700 Saloon 14 17 2 Devan_broom/810 Saloon 13 22 2 Devan_broom/920 Saloon 32 19 2 Devan_broom/1020 Saloon 42 19 2 Devan_broom/1100 Saloon 33 8 2 Devan_broom/1200 Saloon 24 19 Devan_broom/1300 CommunityCenter 26 17 0 \"Characters\\Dialogue\\Devan:CommunityCenter1\"/1600 CommunityCenter 16 20 0/1630 CommunityCenter 11 27 2 Devan_sit \"Characters\\Dialogue\\Devan:CommunityCenter2\"/1700 Town 26 21 2/a2140 Saloon 31 19 1/2150 Saloon 31 8 2/2200 Saloon 39 7 0/2210 Saloon 44 5 3 devan_sleep";
                            });
                        }
                        if (IsLeahMarried is true && IsElliottMarried is false)
                        {
                            e.Edit(asset =>
                            {
                                IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                data["Fri"] = "640 Saloon 39 7 2/650 Saloon 35 8 2/700 Saloon 14 17 2 Devan_broom/810 Saloon 13 22 2 Devan_broom/920 Saloon 32 19 2 Devan_broom/1020 Saloon 42 19 2 Devan_broom/1100 Saloon 33 8 2 Devan_broom/1200 Saloon 24 19 Devan_broom/1300 Woods 8 9 0 \"Characters\\Dialogue\\Devan:statue\"/1600 ElliottHouse 5 8 1/1800 ElliottHouse 8 4 Devan_sit/a2140 Saloon 31 19 1/2150 Saloon 31 8 2/2200 39 7 0/2210 Saloon 44 5 3 devan_sleep";
                            });
                        }
                        else if (IsLeahMarried is false && IsElliottMarried is true)
                        {
                            e.Edit(asset =>
                            {
                                IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                data["Fri"] = "640 Saloon 39 7 2/650 Saloon 35 8 2/700 Saloon 14 17 2 Devan_broom/810 Saloon 13 22 2 Devan_broom/920 Saloon 32 19 2 Devan_broom/1020 Saloon 42 19 2 Devan_broom/1100 Saloon 33 8 2 Devan_broom/1200 Saloon 24 19 Devan_broom/1300 LeahHouse 6 7 0 \"Characters\\Dialogue\\Devan:leahHouse\"/1500 LeahHouse 13 4 2 Devan_sit \"Characters\\Dialogue\\Devan:leahHouse_2\"/1600 Woods 12 6 2 Devan_sit \"Characters\\Dialogue\\Devan:secretforest\"/1800 Woods 10 17 2/a2140 Saloon 31 19 1/2150 Saloon 31 8 2/2200 39 7 0/2210 Saloon 44 5 3 devan_sleep";
                            });
                        }
                        else if (IsLeahMarried is true && IsElliottMarried is true)
                        {
                            e.Edit(asset =>
                            {
                                IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                data["Fri"] = "640 Saloon 39 7 2/650 Saloon 35 8 2/700 Saloon 14 17 2 Devan_broom/810 Saloon 13 22 2 Devan_broom/920 Saloon 32 19 2 Devan_broom/1020 Saloon 42 19 2 Devan_broom/1100 Saloon 33 8 2 Devan_broom/1200 Saloon 24 19 Devan_broom/1300 Woods 8 9 0 \"Characters\\Dialogue\\Devan:statue\"/1800 Woods 12 6 2 Devan_sit \"Characters\\Dialogue\\Devan:secretforest\"/1900 Woods 10 17 2/a2140 Saloon 31 19 1/2150 Saloon 31 8 2/2200 39 7 0/2210 Saloon 44 5 3 devan_sleep";
                            });
                        }
                        if (HasSVE is true)
                        {
                            e.Edit(asset =>
                            {
                                IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                data["Sat"] = "640 Saloon 39 7 2/650 Saloon 35 8 2/700 Saloon 14 17 2 Devan_broom/810 Saloon 13 22 2 Devan_broom/920 Saloon 32 19 2 Devan_broom/1020 Saloon 42 19 2 Devan_broom/1100 Saloon 33 8 2 Devan_broom/1200 Saloon 24 19 Devan_broom/1300 Forest 43 10 2 \"Characters\\Dialogue\\Devan:forest\"/1530 Forest 45 41 0/a1840 Forest 102 64 0 \"Characters\\Dialogue\\Devan:forest_2\"/a2140 Saloon 31 19 1/2150 Saloon 31 8 2/2200 39 7 0/2210 Saloon 44 5 3 devan_sleep";
                            });
                        }
                    }
                }
            }
        }
        private void OnSaveLoaded(object sender, SaveLoadedEventArgs e)
        {
            /*contentpatcher conditions
             * gets api, makes a list with spouses mod has by default. then compares the relationship status of each one
             * if it matches, it adds the value as "true" to dictionary. if not match, its added as false
             */
            var api = this.Helper.ModRegistry.GetApi<ContentPatcher.IContentPatcherAPI>("Pathoschild.ContentPatcher");

            List<string> IntegratedSpouses = SGIValues.SpousesAddedByMod();
            foreach (string s in IntegratedSpouses)
            {
                var a = new Dictionary<string, string>
                {
                    [$"Relationship: {s}"] = "Married"
                };

                var conditions = api.ParseConditions(
                manifest: this.ModManifest,
                rawConditions: a,
                formatVersion: new SemanticVersion("1.20.0")
                );

                if (conditions.IsMatch && conditions.IsValid)
                {
                    MarriedtoNPC.Add($"{s}", true);
                }
                else
                {
                    MarriedtoNPC.Add($"{s}", false);
                }
            }

            //set values that are needed for mod to work
            Children = Game1.MasterPlayer.getChildren();
            IsKrobusRoommate = MarriedtoNPC.GetValueOrDefault<string, bool>("Krobus");
            IsLeahMarried = MarriedtoNPC.GetValueOrDefault<string, bool>("Leah");
            IsElliottMarried = MarriedtoNPC.GetValueOrDefault<string, bool>("Elliott");
            CCC = Game1.MasterPlayer.hasCompletedCommunityCenter();
            SawDevan4H = Game1.MasterPlayer.eventsSeen.Contains(110371000);

            if (Config.Verbose == true)
            {
                this.Monitor.Log($"Children (count) = {Game1.MasterPlayer.getChildren().Count};\nIsKrobusRoommate = {MarriedtoNPC.GetValueOrDefault<string, bool>("Krobus")};\nIsLeahMarried = {MarriedtoNPC.GetValueOrDefault<string, bool>("Leah")};\nIsElliottMarried = {MarriedtoNPC.GetValueOrDefault<string, bool>("Elliott")};\nCCC = {Game1.MasterPlayer.hasCompletedCommunityCenter()};\nSawDevan4H = {Game1.MasterPlayer.eventsSeen.Contains(110371000)};");
            }

            foreach (KeyValuePair<string, bool> kvp in MarriedtoNPC)
            {
                bool LoadThisSpouse = SGIData.IsSpouseEnabled(kvp.Key, Config);
                if (kvp.Value is true && LoadThisSpouse is true)
                {
                    EnabledSpouses.Add(kvp.Key);
                    if (Config.Verbose == true)
                    {
                        this.Monitor.Log($"Added {kvp.Key} to EnabledSpouses list");
                    }
                }
            }
        }
        private void OnDayEnding(object sender, DayEndingEventArgs e)
        {
            CanDoHeavyLoad = true;
            if (Config.Verbose == true)
            {
                this.Monitor.Log($"CanDoHeavyLoad set to true");
            }

            PreviousDayRandom = RandomizedInt;
            RandomizedInt = Random.Next(1, 101);
            foreach (string str in SchedulesEdited)
            {
                Helper.GameContent.InvalidateCache($"Characters/schedules/{str}");
                if (Config.Verbose == true)
                {
                    this.Monitor.Log($"Invalidated cache of \"Characters/schedules/{str}\"");
                }
            }
            foreach (string s in MarriedtoNPC.Keys)
            {
                MarriedtoNPC.TryGetValue($"{s}", out bool IsTempMarried);
                if (IsTempMarried is true)
                {
                    Helper.GameContent.InvalidateCache($"Characters/schedules/{s}");
                    if (Config.Verbose == true)
                    {
                        this.Monitor.Log($"Invalidated cache of \"Characters/schedules/{s}\"");
                    }
                }
            }
            if (IsKrobusRoommate is true)
            {
                Helper.GameContent.InvalidateCache($"Characters/Krobus");
                Helper.GameContent.InvalidateCache($"Portraits/Krobus");
                if (Config.Verbose == true)
                {
                    this.Monitor.Log($"Invalidated cache of \"Characters/schedules/Krobus\" and Portraits/Krobus");
                }
            }
            if (Children.Count >= 1 && HasC2N is true && Config.Allow_Children == true)
            {
                foreach (var child in Children)
                {
                    Helper.GameContent.InvalidateCache($"Characters/schedules/{child.Name}");
                    if (Config.Verbose == true)
                    {
                        this.Monitor.Log($"Invalidated cache of \"Characters/schedules/{child.Name}\"");
                    }
                }
            }
            if (Config.NPCDevan == true && Children.Count >= 1)
            {
                Helper.GameContent.InvalidateCache("Characters/schedules/Devan");
                if (Config.Verbose == true)
                {
                    this.Monitor.Log($"Invalidated cache of \"Characters/schedules/Devan\"");
                }
            }
        }
        private void OnDayStarted(object sender, DayStartedEventArgs e)
        {
            CanDoHeavyLoad = false;
            if(Config.Verbose == true)
            {
                this.Monitor.Log($"CanDoHeavyLoad set to false");
            }
        }
        private void OnTimeChanged(object sender, TimeChangedEventArgs e)
        {

            if (e.NewTime >= 2200)
            {
                //spouse info
                if (Config.CustomChance >= RandomizedInt)
                {
                    var sgv = new SGIValues();
                    var ifh = Game1.getLocationFromName("IslandFarmHouse");
                    foreach (NPC c in ifh.characters)
                    {
                        if (c.isMarried())
                        {
                            if (Game1.IsMasterGame && e.NewTime >= 2200 && c.getTileLocationPoint() != sgv.getSpouseBedSpot(c.Name) && (Game1.timeOfDay == 2200 || (c.controller == null && Game1.timeOfDay % 100 % 30 == 0)))
                            {
                                if (Config.Verbose == true)
                                {
                                    this.Monitor.Log($"Pathing {c.Name} to {sgv.getSpouseBedSpot(c.Name)} in {ifh.Name}");
                                }
                                //code from previous test copied ahead
                                c.controller =
                                    new PathFindController(
                                        c,
                                        location: ifh,
                                        sgv.getSpouseBedSpot(c.Name),
                                        0,
                                        (c, location) =>
                                        {
                                            c.doEmote(Character.sleepEmote);
                                            sgv.spouseSleepEndFunction(c, location);
                                        });

                                if (c.controller.pathToEndPoint == null)
                                {
                                    this.Monitor.Log($"{c.Name} can't reach the bed! They won't go to sleep.", LogLevel.Trace);
                                }
                            }
                        }
                    }
                }
                else
                    return;
            }
        }
        private void OnLocaleChanged(object sender, LocaleChangedEventArgs e)
        {
            if (Config.Verbose == true)
            {
                this.Monitor.Log($"Changing language to {LanguageInfo.GetLanguageCode()}");
            }
            currentLang = LanguageInfo.GetLanguageCode();
        }

        private ModConfig Config;
        private static Random random;

        /*   Internal (can only be accessed by current .cs) */
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
            Debugging.List(this, args, Helper, Config);
        }
        internal void SGI_Chance(string command, string[] args)
        {
            Debugging.Chance(this, args, Helper, Config);
        }
        internal void SGI_Reset(string command, string[] args)
        {
            Debugging.Reset(this, args, Helper, Config);
        }
        internal void SGI_Help(string command, string[] args)
        {
            this.Monitor.Log(this.Helper.Translation.Get("CLI.helpdescription"), LogLevel.Info);
        }

        internal Texture2D KbcSamples() => Helper.ModContent.Load<Texture2D>("assets/kbcSamples.png");
        internal string SpouseD()
        {
            var SpousesDesc = this.Helper.Translation.Get("config.Vanillas.description");
            return SpousesDesc;
        }

        internal Dictionary<string, bool> MarriedtoNPC = new();
        internal List<string> SchedulesEdited = new();
        internal List<string> DialoguesEdited = new();
        internal List<string> TranslationsAdded = new();
        internal List<string> EnabledSpouses = new();
        internal List<Child> Children = new();
        internal string currentLang = LanguageInfo.GetLanguageCode();
        
        internal static Random Random
        {
            get
            {
                random ??= new Random(((int)Game1.uniqueIDForThisGame * 26) + (int)(Game1.stats.DaysPlayed * 36));
                return random;
            }
        }
        internal bool IsLeahMarried;
        internal bool IsElliottMarried;
        internal bool IsKrobusRoommate;
        internal bool SawDevan4H = false;
        internal bool CCC;
        internal bool HasSVE;
        internal bool HasC2N;
        internal bool HasExGIM;
        internal bool CanDoHeavyLoad = true;
        internal int PreviousDayRandom;
    }
}
