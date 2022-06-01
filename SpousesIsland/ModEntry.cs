using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using GenericModConfigMenu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Characters;
using xTile;
using xTile.Tiles;
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
    }
    public class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
            helper.Events.Content.AssetRequested += this.OnAssetRequested;
            helper.Events.GameLoop.SaveLoaded += this.OnSaveLoaded;
            helper.Events.GameLoop.DayEnding += this.OnDayEnding;
            helper.Events.GameLoop.TimeChanged += this.OnTimeChanged;
            helper.Events.Content.LocaleChanged += this.OnLocaleChanged;

            //??
            int RandomizedInt = this.RandomizedInt;
            //commands
            helper.ConsoleCommands.Add("sgi_help", helper.Translation.Get("CLI.help"), this.SGI_Help);
            helper.ConsoleCommands.Add("sgi_chance", helper.Translation.Get("CLI.chance"), this.SGI_Chance);
            helper.ConsoleCommands.Add("sgi_reset", helper.Translation.Get("CLI.reset"), this.SGI_Reset);
            helper.ConsoleCommands.Add("sgi_list", helper.Translation.Get("CLI.list"), this.SGI_List);
            helper.ConsoleCommands.Add("sgi_about", helper.Translation.Get("CLI.about"), this.SGI_About);

            this.Config = this.Helper.ReadConfig<ModConfig>();
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
                text: SpouseT,
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
                text: SVET,
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
             * Everything is put inside this bool to make sure the game doesn't lag during 10min updates (bc for some reason it continues loading files post saveload)
             * "!Context.IsWorldReady || " taken out, it just checks for whether it can load
             */
            if (CanDoHeavyLoad)
            {
                if (e.Name.StartsWith("Maps/", false, true))
                {
                    if (e.Name.IsEquivalentTo("Maps/FishShop"))
                    {
                        e.Edit(asset =>
                        {
                            var editor = asset.AsMap();
                            this.Monitor.VerboseLog("Editing FishShop...");
                            Map map = editor.Data;
                            map.Properties.Add("NPCWarp", "4 3 IslandSouth 19 43");
                        });
                    }
                    if (e.Name.IsEquivalentTo("Maps/Custom_IslandShell"))
                    {
                        e.Edit(asset =>
                        {
                            var editor = asset.AsMap();
                            Map sourceMap = Helper.ModContent.Load<Map>("assets/Maps/z_SpouseRoomShell.tbin");
                            editor.PatchMap(sourceMap, patchMode: PatchMapMode.ReplaceByLayer);
                        });
                    }
                    if (e.Name.IsEquivalentTo("Maps/Custom_IslandShell_freelove"))
                    {
                        e.Edit(asset =>
                        {
                            var editor = asset.AsMap();
                            Map sourceMap = Helper.ModContent.Load<Map>("assets/Maps/z_SpouseRoomShell_fl.tbin");
                            editor.PatchMap(sourceMap, patchMode: PatchMapMode.ReplaceByLayer);
                        });
                    }
                    if (e.Name.IsEquivalentTo("Maps/Saloon") && Config.NPCDevan == true)
                    {
                        if (HasSVE is false)
                            e.Edit(asset =>
                            {
                                var editor = asset.AsMap();
                                Map sourceMap = Helper.ModContent.Load<Map>("assets/Maps/z_DevanRoom.tbin");
                                editor.PatchMap(sourceMap, sourceArea: new Rectangle(0, 0, 11, 11), targetArea: new Rectangle(35, 0, 11, 11), patchMode: (PatchMapMode)PatchMode.Replace);
                                Map map = editor.Data;
                                map.Properties["NPCWarp"] = "14 25 Town 45 71";

                                xTile.ObjectModel.PropertyValue doors;
                                map.Properties.TryGetValue("Doors", out doors);
                                map.Properties.Remove("Doors");
                                map.Properties.Add("Doors", $"{doors} 39 8 1 120");

                                Map PatchFix = Helper.ModContent.Load<Map>("assets/Maps/z_DevanRoom_FIX.tbin");
                                editor.PatchMap(PatchFix, sourceArea: new Rectangle(0, 0, 4, 4), targetArea: new Rectangle(35, 7, 4, 4), patchMode: (PatchMapMode)PatchMode.Replace);
                                //remember to take out the objects. somehow.
                            });
                        else
                            e.Edit(asset =>
                            {
                                var editor = asset.AsMap();
                                Map sourceMap = Helper.ModContent.Load<Map>("assets/Maps/z_DevanRoom_comp.tbin");
                                editor.PatchMap(sourceMap, sourceArea: new Rectangle(3, 0, 8, 11), targetArea: new Rectangle(38, 0, 8, 11));
                                editor.PatchMap(sourceMap, sourceArea: new Rectangle(0, 8, 3, 2), targetArea: new Rectangle(35, 8, 3, 2));
                                Map map = editor.Data;
                                map.Properties["NPCWarp"] = "14 25 Town 45 71";

                                xTile.ObjectModel.PropertyValue doors;
                                map.Properties.TryGetValue("Doors", out doors);
                                map.Properties.Remove("Doors");
                                map.Properties.Add("Doors", $"{doors} 39 8 1 120");

                            });
                        if (SawDevan4H == true)
                            e.Edit(asset =>
                            {
                                //edit map to have picture in devans room be of their hometown
                                var editor = asset.AsMap();
                                Map sourceMap = Helper.ModContent.Load<Map>("assets/Maps/z_Devan_post4h.tbin");
                                editor.PatchMap(sourceMap, sourceArea: new Rectangle(0, 0, 2, 2), targetArea: new Rectangle(40, 1, 2, 2));
                            });
                    }
                }
                if (e.Name.StartsWith("Maps/Island", true, false))
                {
                    if (e.Name.IsEquivalentTo("Maps/IslandFarmHouse"))
                    {
                        if (Config.CustomRoom == true && (Config.Allow_Children == false || Children.Count is 0))
                        {
                            e.LoadFromModFile<Map>("assets/Maps/FarmHouse_Custom.tbin", AssetLoadPriority.Medium);
                        }
                        if (Config.Allow_Children == true && Children.Count >= 1)
                        {
                            e.LoadFromModFile<Map>($"assets/Maps/FarmHouse_kid_custom{Config.CustomRoom}.tbin", AssetLoadPriority.Medium);
                            e.Edit(asset =>
                            {
                                this.Monitor.Log("Patching child bed onto IslandFarmHouse...", LogLevel.Trace);
                                var editor = asset.AsMap();
                                Map sourceMap = Helper.ModContent.Load<Map>($"assets/Maps/kidbeds/z_kidbed_{Config.Childbedcolor}.tbin");
                                editor.PatchMap(sourceMap, sourceArea: new Rectangle(0, 0, 2, 4), targetArea: new Rectangle(35, 13, 2, 4), patchMode: PatchMapMode.Overlay);

                            });
                        }
                        e.Edit(asset =>
                        {
                            var editor = asset.AsMap();
                            Map map = editor.Data;
                            map.Properties.Add("NPCWarp", "14 17 IslandWest 77 41");
                        });
                    }
                    if (e.Name.IsEquivalentTo("Maps/Island_FieldOffice"))
                    {
                        e.Edit(asset =>
                        {
                            var editor = asset.AsMap();
                            Map map = editor.Data;
                            map.Properties.Add("NPCWarp", "4 11 IslandNorth 46 46");
                        });
                    }
                    if (e.Name.IsEquivalentTo("Maps/Island_N"))
                    {
                        e.Edit(asset =>
                        {
                            var editor = asset.AsMap();
                            Map map = editor.Data;
                            map.Properties.Add("NPCWarp", "22 22 IslandFarmHouse 14 16 35 89 IslandSouth 18 0 46 45 IslandFieldOffice 4 10 40 21 VolcanoEntrance 1 1 40 22 VolcanoEntrance 1 1");
                        });
                    }
                    if (e.Name.IsEquivalentTo("Maps/Island_S"))
                    {
                        e.Edit(asset =>
                        {
                            var editor = asset.AsMap();
                            Map map = editor.Data;
                            map.Properties.Remove("NPCWarp");
                            map.Properties.Add("NPCWarp", "17 44 FishShop 3 4 36 11 IslandEast 0 45 36 12 IslandEast 0 46 36 13 IslandEast 0 47 -1 11 IslandWest 105 40 -1 10 IslandWest 105 40 -1 12 IslandWest 105 40 -1 13 IslandWest 105 40 17 -1 IslandNorth 35 89 18 -1 IslandNorth 36 89 19 -1 IslandNorth 37 89 27 -1 IslandNorth 43 89 28 -1 IslandNorth 43 89 43 28 IslandSouthEast 0 29 43 29 IslandSouthEast 0 29 43 30 IslandSouthEast 0 29");
                        });
                    }
                    if (e.Name.IsEquivalentTo("Maps/Island_SE"))
                    {
                        e.Edit(asset =>
                        {
                            var editor = asset.AsMap();
                            Map map = editor.Data;
                            map.Properties.Add("NPCWarp", "0 29 IslandSouth 43 29 29 18 IslandSouthEastCave 1 8");
                        });
                    }
                    if (e.Name.IsEquivalentTo("Maps/Island_SouthEastCave"))
                    {
                        e.Edit(asset =>
                        {
                            var editor = asset.AsMap();
                            Map map = editor.Data;
                            map.Properties.Add("NPCWarp", "0 7 IslandSouthEast 30 19");
                        });
                    }
                    if (e.Name.IsEquivalentTo("Maps/IslandSouthEastCave_pirates"))
                    {
                        e.Edit(asset =>
                        {
                            var editor = asset.AsMap();
                            this.Monitor.VerboseLog("Editing IslandSouthEastCave_pirates...");
                            Map map = editor.Data;
                            map.Properties.Add("NPCWarp", "0 7 IslandSouthEast 30 19");
                        });
                    }
                    if (e.Name.IsEquivalentTo("Maps/IslandWestCave1"))
                    {
                        e.Edit(asset =>
                        {
                            var editor = asset.AsMap();
                            Map map = editor.Data;
                            map.Properties.Add("NPCWarp", "6 12 IslandWest 61 5");
                        });
                    }
                    if (e.Name.IsEquivalentTo("Maps/IslandEast"))
                    {
                        e.Edit(asset =>
                        {
                            var editor = asset.AsMap();
                            Map map = editor.Data;
                            map.Properties.Add("NPCWarp", "-1 45 IslandSouth 35 11 -1 46 IslandSouth 35 12 -1 47 IslandSouth 35 13 -1 48 IslandSouth 35 13 22 9 IslandHut 7 13 34 30 IslandShrine 13 28");
                        });
                    }
                    if (e.Name.IsEquivalentTo("Maps/IslandShrine"))
                    {
                        e.Edit(asset =>
                        {
                            var editor = asset.AsMap();
                            Map map = editor.Data;
                            map.Properties.Add("NPCWarp", "12 27 IslandEast 33 30 12 28 IslandEast 33 30 12 29 IslandEast 33 30");
                        });
                    }
                    if (e.Name.IsEquivalentTo("Maps/IslandFarmCave"))
                    {
                        e.Edit(asset =>
                        {
                            var editor = asset.AsMap();
                            Map map = editor.Data;
                            map.Properties.Add("NPCWarp", "4 10 IslandSouth 97 35");
                        });
                    }
                    if (e.Name.IsEquivalentTo("Maps/Island_W"))
                    {
                        e.Edit(asset =>
                        {
                            var editor = asset.AsMap();
                            Map map = editor.Data;
                            /*  77 41 IslandFarmHouse 14 16 was taken out, because that's the coord NPCs warp to (from islandfarmhouse). */
                            map.Properties.Add("NPCWarp", "106 39 IslandSouth 0 10 106 40 IslandSouth 0 11 106 41 IslandSouth 0 12 106 42 IslandSouth 0 12 61 3 IslandWestCave1 6 11 96 32 IslandFarmCave 4 10 60 92 CaptainRoom 0 5 77 40 IslandFarmHouse 14 16");

                            Tile BridgeBarrier = map.GetLayer("Back").Tiles[62, 16];
                            if (BridgeBarrier is not null)
                                BridgeBarrier.Properties.Remove("NPCBarrier");

                            Tile a = map.GetLayer("Back").Tiles[60, 18];
                            if (a is not null)
                                a.Properties.Add("NPCBarrier", "T");

                            Tile b = map.GetLayer("Back").Tiles[60, 17];
                            if (b is not null)
                                b.Properties.Add("NPCBarrier", "T");

                            Tile c = map.GetLayer("Back").Tiles[60, 16];
                            if (c is not null)
                                c.Properties.Add("NPCBarrier", "T");

                            Tile d = map.GetLayer("Back").Tiles[60, 15];
                            if (d is not null)
                                d.Properties.Add("NPCBarrier", "T");

                            Tile e = map.GetLayer("Back").Tiles[60, 14];
                            if (e is not null)
                                e.Properties.Add("NPCBarrier", "T");

                            Tile f = map.GetLayer("Back").Tiles[60, 13];
                            if (f is not null)
                                f.Properties.Add("NPCBarrier", "T");

                            Tile g = map.GetLayer("Back").Tiles[60, 12];
                            if (g is not null)
                                g.Properties.Add("NPCBarrier", "T");
                        });
                    }
                }
                if (e.Name.StartsWith("Characters/", false, true))
                {
                    if (e.Name.StartsWith("Characters/schedules/", false, true))
                    {
                        /*this is set at the top so it doesn't overwrite krobus data by accident*/
                        if (e.Name.IsEquivalentTo("Characters/schedules/Krobus") && Config.CustomChance >= RandomizedInt)
                        { e.LoadFromModFile<Dictionary<string, string>>("assets/Spouses/Empty.json", AssetLoadPriority.Low); }
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
                        //integrated data
                        if (Config.CustomChance >= RandomizedInt)
                        {
                            if (HasC2N is true && Config.Allow_Children == true && Children.Count is not 0)
                            {
                                this.Monitor.LogOnce("Child To NPC is in the mod folder. Adding compatibility...", LogLevel.Trace);
                                if (e.Name.IsEquivalentTo("Characters/schedules/" + Children?[0].Name))
                                    e.Edit(asset =>
                                    {
                                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                        data["Mon"] = $"620 IslandFarmHouse 20 10 3/1100 {Config.Child1_L1} {Config.Child1_X1} {Config.Child1_Y1}/1400 {Config.Child1_L2} {Config.Child1_X2} {Config.Child1_Y2}/1700 {Config.Child1_L3} {Config.Child1_X3} {Config.Child1_Y3}/1900 IslandFarmHouse 15 12 0/2000 IslandFarmHouse 30 15 2/2100 IslandFarmHouse 35 14 3";
                                        data["Tue"] = "GOTO Mon";
                                        data["Wed"] = "GOTO Mon";
                                        data["Thu"] = "GOTO Mon";
                                        data["Fri"] = "GOTO Mon";
                                        data["Sat"] = "GOTO Mon";
                                        data["Sun"] = "GOTO Mon";
                                    });
                                if (e.Name.IsEquivalentTo("Characters/schedules/" + Children?[1].Name))
                                    e.Edit(asset =>
                                    {
                                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                        data["Mon"] = $"620 IslandFarmHouse 20 8 0/1030 IslandFarmHouse 22 6 1/1100 {Config.Child2_L1} {Config.Child2_X1} {Config.Child2_Y1}/1400 {Config.Child2_L2} {Config.Child2_X2} {Config.Child2_Y2}/1700 {Config.Child2_L3} {Config.Child2_X3} {Config.Child2_Y3}/1900 IslandFarmHouse 15 14 0/2000 IslandFarmHouse 27 14 2/2100 IslandFarmHouse 36 14 2";
                                        data["Tue"] = "GOTO Mon";
                                        data["Wed"] = "GOTO Mon";
                                        data["Thu"] = "GOTO Mon";
                                        data["Fri"] = "GOTO Mon";
                                        data["Sat"] = "GOTO Mon";
                                        data["Sun"] = "GOTO Mon";
                                    });
                            }
                            if (e.Name.IsEquivalentTo("Characters/schedules/Abigail") && Config.Allow_Abigail == true)
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data["marriage_Mon"] = $"620 IslandFarmHouse 16 9 0 \"Strings\\schedules\\Abigail:marriage_islandhouse\"/1100 IslandNorth 44 28 0 \"Strings\\schedules\\Abigail:marriage_loc1\"/a1800 {SGIValues.RandomMap_nPos(Random, "Abigail", HasExGIM, Config.ScheduleRandom)}/2000 IslandWest 39 41 0 \"Strings\\schedules\\Abigail:marriage_loc3\"/a2200 IslandFarmHouse 16 9 0";
                                    data["marriage_Tue"] = "GOTO marriage_Mon";
                                    data["marriage_Wed"] = "GOTO marriage_Mon";
                                    data["marriage_Thu"] = "GOTO marriage_Mon";
                                    data["marriage_Fri"] = "GOTO marriage_Mon";
                                    data["marriage_Sat"] = "GOTO marriage_Mon";
                                    data["marriage_Sun"] = "GOTO marriage_Mon";
                                });
                            if (e.Name.IsEquivalentTo("Characters/schedules/Alex") && Config.Allow_Alex == true)
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data["marriage_Mon"] = $"620 IslandFarmHouse 19 6 0 \"Strings\\schedules\\Alex:marriage_islandhouse\"/1100 IslandWest 85 39 2 alex_lift_weights \"Strings\\schedules\\Alex:marriage_loc1\"/1300 {SGIValues.RandomMap_nPos(Random, "Alex", HasExGIM, Config.ScheduleRandom)}/1500 IslandWest 64 83 2/a1900 IslandSouth 12 27 2 \"Strings\\schedules\\Alex:marriage_loc3\"/a2200 IslandFarmHouse 19 6 0";
                                    data["marriage_Tue"] = "GOTO marriage_Mon";
                                    data["marriage_Wed"] = "GOTO marriage_Mon";
                                    data["marriage_Thu"] = "GOTO marriage_Mon";
                                    data["marriage_Fri"] = "GOTO marriage_Mon";
                                    data["marriage_Sat"] = "GOTO marriage_Mon";
                                    data["marriage_Sun"] = "GOTO marriage_Mon";
                                });
                            if (e.Name.IsEquivalentTo("Characters/schedules/Elliott") && Config.Allow_Elliott == true)
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data["marriage_Mon"] = $"620 IslandFarmHouse 9 9 2 \"Strings\\schedules\\Elliott:marriage_islandhouse\"/1100 IslandWest 102 77 2 elliott_read/1400 IslandWest 73 83 2 \"Strings\\schedules\\Elliott:marriage_loc2\"/a1900 {SGIValues.RandomMap_nPos(Random, "Elliott", HasExGIM, Config.ScheduleRandom)}/a2200 IslandFarmHouse 9 9 2";
                                    data["marriage_Tue"] = "GOTO marriage_Mon";
                                    data["marriage_Wed"] = "GOTO marriage_Mon";
                                    data["marriage_Thu"] = "GOTO marriage_Mon";
                                    data["marriage_Fri"] = "GOTO marriage_Mon";
                                    data["marriage_Sat"] = "GOTO marriage_Mon";
                                    data["marriage_Sun"] = "GOTO marriage_Mon";
                                });
                            if (e.Name.IsEquivalentTo("Characters/schedules/Emily") && Config.Allow_Emily == true)
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data["marriage_Mon"] = $"620 IslandFarmHouse 12 10 \"Strings\\schedules\\Emily:marriage_islandhouse\"/1100 IslandWest 53 52 2 \"Strings\\schedules\\Emily:marriage_loc1\"/1400 IslandWest 89 79 2 emily_exercise/1700 {SGIValues.RandomMap_nPos(Random, "Emily", HasExGIM, Config.ScheduleRandom)}/a2200 IslandFarmHouse 12 10";
                                    data["marriage_Tue"] = "GOTO marriage_Mon";
                                    data["marriage_Wed"] = "GOTO marriage_Mon";
                                    data["marriage_Thu"] = "GOTO marriage_Mon";
                                    data["marriage_Fri"] = "GOTO marriage_Mon";
                                    data["marriage_Sat"] = "GOTO marriage_Mon";
                                    data["marriage_Sun"] = "GOTO marriage_Mon";
                                });
                            if (e.Name.IsEquivalentTo("Characters/schedules/Haley") && Config.Allow_Haley == true)
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data["marriage_Mon"] = $"620 IslandFarmHouse 8 6 2 \"Strings\\schedules\\Haley:marriage_islandhouse\"/1100 IslandNorth 32 74 0 \"Strings\\schedules\\Haley:marriage_loc1\"/1400 {SGIValues.RandomMap_nPos(Random, "Haley", HasExGIM, Config.ScheduleRandom)}/1900 IslandWest 80 45 2/a2200 IslandFarmHouse 8 6 0";
                                    data["marriage_Tue"] = "GOTO marriage_Mon";
                                    data["marriage_Wed"] = "GOTO marriage_Mon";
                                    data["marriage_Thu"] = "GOTO marriage_Mon";
                                    data["marriage_Fri"] = "GOTO marriage_Mon";
                                    data["marriage_Sat"] = "GOTO marriage_Mon";
                                    data["marriage_Sun"] = "GOTO marriage_Mon";
                                });
                            if (e.Name.IsEquivalentTo("Characters/schedules/Harvey") && Config.Allow_Harvey == true)
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data["marriage_Mon"] = $"620 IslandFarmHouse 16 13 0 \"Strings\\schedules\\Harvey:marriage_islandhouse\"/1100 IslandFarmHouse 3 5 0 \"Strings\\schedules\\Harvey:marriage_loc1\"/1400 IslandWest 89 75 2 harvey_excercise/1600 {SGIValues.RandomMap_nPos(Random, "Harvey", HasExGIM, Config.ScheduleRandom)}/a2100 IslandFarmHouse 16 13";
                                    data["marriage_Tue"] = "GOTO marriage_Mon";
                                    data["marriage_Wed"] = "GOTO marriage_Mon";
                                    data["marriage_Thu"] = "GOTO marriage_Mon";
                                    data["marriage_Fri"] = "GOTO marriage_Mon";
                                    data["marriage_Sat"] = "GOTO marriage_Mon";
                                    data["marriage_Sun"] = "GOTO marriage_Mon";
                                });

                            if (e.Name.IsEquivalentTo("Characters/schedules/Krobus") && Config.Allow_Krobus == true)
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data["marriage_Mon"] = $"620 IslandFarmHouse 15 10 0 \"Characters\\Dialogue\\Krobus:marriage_islandhouse\"/1100 IslandFarmHouse 10 10 3 \"Characters\\Dialogue\\Krobus:marriage_loc1\"/1130 IslandFarmHouse 9 8 0/1200 IslandFarmHouse 9 11 2/1400 IslandWestCave1 8 8 0 \"Characters\\Dialogue\\Krobus:marriage_loc3\"/1500 IslandWestCave1 9 8 0/1600 IslandWestCave1 9 6 3/1900 {SGIValues.RandomMap_nPos(Random, "Krobus", HasExGIM, Config.ScheduleRandom)}/a2200 IslandFarmHouse 15 10 0";
                                    data["marriage_Tue"] = "GOTO marriage_Mon";
                                    data["marriage_Wed"] = "GOTO marriage_Mon";
                                    data["marriage_Thu"] = "GOTO marriage_Mon";
                                    data["marriage_Fri"] = "GOTO marriage_Mon";
                                    data["marriage_Sat"] = "GOTO marriage_Mon";
                                    data["marriage_Sun"] = "GOTO marriage_Mon";
                                });
                            if (e.Name.IsEquivalentTo("Characters/schedules/Leah") && Config.Allow_Leah == true)
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data["marriage_Mon"] = $"620 IslandFarmHouse 21 13 1 \"Strings\\schedules\\Leah:marriage_islandhouse\"/1100 IslandNorth 50 25 0 leah_draw \"Strings\\schedules\\Leah:marriage_loc1\"/1400 IslandNorth 21 16 0/1600 {SGIValues.RandomMap_nPos(Random, "Leah", HasExGIM, Config.ScheduleRandom)}/a2200 IslandFarmHouse 21 13 1";
                                    data["marriage_Tue"] = "GOTO marriage_Mon";
                                    data["marriage_Wed"] = "GOTO marriage_Mon";
                                    data["marriage_Thu"] = "GOTO marriage_Mon";
                                    data["marriage_Fri"] = "GOTO marriage_Mon";
                                    data["marriage_Sat"] = "GOTO marriage_Mon";
                                    data["marriage_Sun"] = "GOTO marriage_Mon";
                                });
                            if (e.Name.IsEquivalentTo("Characters/schedules/Maru") && Config.Allow_Maru == true)
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data["marriage_Mon"] = $"620 IslandFarmHouse 18 15 2 \"Strings\\schedules\\Maru:marriage_islandhouse\"/1100 IslandWest 95 45 2/1400 IslandNorth 50 25 0 \"Strings\\schedules\\Maru:marriage_loc1\"/1700 {SGIValues.RandomMap_nPos(Random, "Maru", HasExGIM, Config.ScheduleRandom)}/a2200 IslandFarmHouse 18 15 2";
                                    data["marriage_Tue"] = "GOTO marriage_Mon";
                                    data["marriage_Wed"] = "GOTO marriage_Mon";
                                    data["marriage_Thu"] = "GOTO marriage_Mon";
                                    data["marriage_Fri"] = "GOTO marriage_Mon";
                                    data["marriage_Sat"] = "GOTO marriage_Mon";
                                    data["marriage_Sun"] = "GOTO marriage_Mon";
                                }
                            );
                            if (e.Name.IsEquivalentTo("Characters/schedules/Penny") && Config.Allow_Penny == true)
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data["marriage_Mon"] = $"620 IslandFarmHouse 9 12 1 \"Strings\\schedules\\Penny:marriage_islandhouse\"/1100 IslandFarmHouse 3 6 0/1400 IslandWest 83 37 3 penny_read \"Strings\\schedules\\Penny:marriage_loc1\"/1700 {SGIValues.RandomMap_nPos(Random, "Penny", HasExGIM, Config.ScheduleRandom)}/a2200 IslandFarmHouse 9 12 1";
                                    data["marriage_Tue"] = "GOTO marriage_Mon";
                                    data["marriage_Wed"] = "GOTO marriage_Mon";
                                    data["marriage_Thu"] = "GOTO marriage_Mon";
                                    data["marriage_Fri"] = "GOTO marriage_Mon";
                                    data["marriage_Sat"] = "GOTO marriage_Mon";
                                    data["marriage_Sun"] = "GOTO marriage_Mon";
                                });
                            if (e.Name.IsEquivalentTo("Characters/schedules/Sam") && Config.Allow_Sam == true)
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data["marriage_Mon"] = $"620 IslandFarmHouse 22 6 2 \"Strings\\schedules\\Sam:marriage_islandhouse\"/1100 IslandFarmHouse 8 9 0 sam_guitar/1400 IslandNorth 36 27 0 sam_skateboarding \"Strings\\schedules\\Sam:marriage_loc1\"/1700 {SGIValues.RandomMap_nPos(Random, "Sam", HasExGIM, Config.ScheduleRandom)}/a2200 IslandFarmHouse 22 6 2";
                                    data["marriage_Tue"] = "GOTO marriage_Mon";
                                    data["marriage_Wed"] = "GOTO marriage_Mon";
                                    data["marriage_Thu"] = "GOTO marriage_Mon";
                                    data["marriage_Fri"] = "GOTO marriage_Mon";
                                    data["marriage_Sat"] = "GOTO marriage_Mon";
                                    data["marriage_Sun"] = "GOTO marriage_Mon";
                                });
                            if (e.Name.IsEquivalentTo("Characters/schedules/Sebastian") && Config.Allow_Sebastian == true)
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data["marriage_Mon"] = $"620 IslandFarmHouse 25 14 3 \"Strings\\schedules\\Sebastian:marriage_islandhouse\"/1100 IslandWest 88 14 0/1400 IslandWestCave1 6 4 0 \"Strings\\schedules\\Sebastian:marriage_loc1\"/1600 {SGIValues.RandomMap_nPos(Random, "Sebastian", HasExGIM, Config.ScheduleRandom)}/a2200 IslandFarmHouse 25 14 3";
                                    data["marriage_Tue"] = "GOTO marriage_Mon";
                                    data["marriage_Wed"] = "GOTO marriage_Mon";
                                    data["marriage_Thu"] = "GOTO marriage_Mon";
                                    data["marriage_Fri"] = "GOTO marriage_Mon";
                                    data["marriage_Sat"] = "GOTO marriage_Mon";
                                    data["marriage_Sun"] = "GOTO marriage_Mon";
                                });
                            if (e.Name.IsEquivalentTo("Characters/schedules/Shane") && Config.Allow_Shane == true)
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data["marriage_Mon"] = $"620 IslandFarmHouse 20 5 0 \"Strings\\schedules\\Shane:marriage_islandhouse\"/1100 IslandWest 87 52 0 shane_charlie \"Strings\\schedules\\Shane:marriage_loc1\"/a1420 IslandWest 77 39 0/1430 IslandFarmHouse 15 9 0 shane_drink/a1900 {SGIValues.RandomMap_nPos(Random, "Shane", HasExGIM, Config.ScheduleRandom)}/a2150 IslandWest 82 43/2200 IslandFarmHouse 20 5 0";
                                    data["marriage_Tue"] = "GOTO marriage_Mon";
                                    data["marriage_Wed"] = "GOTO marriage_Mon";
                                    data["marriage_Thu"] = "GOTO marriage_Mon";
                                    data["marriage_Fri"] = "GOTO marriage_Mon";
                                    data["marriage_Sat"] = "GOTO marriage_Mon";
                                    data["marriage_Sun"] = "GOTO marriage_Mon";
                                });
                            //sve
                            if (e.Name.IsEquivalentTo("Characters/schedules/Claire") && Config.Allow_Claire == true)
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data["marriage_Mon"] = $"620 IslandFarmHouse 5 6 0 \"Characters\\Dialogue\\Claire:marriage_islandhouse\"/1100 IslandFarmHouse 17 12 2 Claire_Read \"Characters\\Dialogue\\Claire:marriage_loc1\"/1400 IslandEast 19 40 0 \"Characters\\Dialogue\\Claire:marriage_loc3\"/1600 {SGIValues.RandomMap_nPos(Random, "Claire", HasExGIM, Config.ScheduleRandom)}/a2150 IslandFarmHouse 5 6 0";
                                    data["marriage_Tue"] = "GOTO marriage_Mon";
                                    data["marriage_Wed"] = "GOTO marriage_Mon";
                                    data["marriage_Thu"] = "GOTO marriage_Mon";
                                    data["marriage_Fri"] = "GOTO marriage_Mon";
                                    data["marriage_Sat"] = "GOTO marriage_Mon";
                                    data["marriage_Sun"] = "GOTO marriage_Mon";
                                });
                            if (e.Name.IsEquivalentTo("Characters/schedules/Lance") && Config.Allow_Lance == true)
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data["marriage_Mon"] = $"620 IslandFarmHouse 13 13 1 \"Characters\\Dialogue\\Lance:marriage_islandhouse\"/1100 IslandNorth 37 30 0/1400 Caldera 24 23 2 \"Characters\\Dialogue\\Lance:marriage_loc2\"/1600 {SGIValues.RandomMap_nPos(Random, "Lance", HasExGIM, Config.ScheduleRandom)}/a2150 IslandFarmHouse 13 13 1";
                                    data["marriage_Tue"] = "GOTO marriage_Mon";
                                    data["marriage_Wed"] = "GOTO marriage_Mon";
                                    data["marriage_Thu"] = "GOTO marriage_Mon";
                                    data["marriage_Fri"] = "GOTO marriage_Mon";
                                    data["marriage_Sat"] = "GOTO marriage_Mon";
                                    data["marriage_Sun"] = "GOTO marriage_Mon";
                                });
                            if (e.Name.IsEquivalentTo("Characters/schedules/Wizard") && Config.Allow_Magnus == true)
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data["marriage_Mon"] = $"620 IslandFarmHouse 26 6 0 \"Characters\\Dialogue\\Wizard:marriage_islandhouse\"/1100 IslandWest 38 38 0 \"Characters\\Dialogue\\Wizard:marriage_loc1\"/1400 IslandSouthEast 28 26 2/1800 {SGIValues.RandomMap_nPos(Random, "Magnus", HasExGIM, Config.ScheduleRandom)}/a2150 IslandFarmHouse 26 6 0";
                                    data["marriage_Tue"] = "GOTO marriage_Mon";
                                    data["marriage_Wed"] = "GOTO marriage_Mon";
                                    data["marriage_Thu"] = "GOTO marriage_Mon";
                                    data["marriage_Fri"] = "GOTO marriage_Mon";
                                    data["marriage_Sat"] = "GOTO marriage_Mon";
                                    data["marriage_Sun"] = "GOTO marriage_Mon";
                                });
                            if (e.Name.IsEquivalentTo("Characters/schedules/Olivia") && Config.Allow_Olivia == true)
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data["marriage_Mon"] = $"620 IslandFarmHouse 7 11 1\"Characters\\Dialogue\\Olivia:marriage_islandhouse\"/1100 IslandFarmHouse 14 9 0 Olivia_Wine1 \"Characters\\Dialogue\\Olivia:marriage_loc1\"/1400 IslandSouth 31 24 2 Olivia_Yoga/1600 {SGIValues.RandomMap_nPos(Random, "Olivia", HasExGIM, Config.ScheduleRandom)}/a2150 IslandFarmHouse 7 11 1";
                                    data["marriage_Tue"] = "GOTO marriage_Mon";
                                    data["marriage_Wed"] = "GOTO marriage_Mon";
                                    data["marriage_Thu"] = "GOTO marriage_Mon";
                                    data["marriage_Fri"] = "GOTO marriage_Mon";
                                    data["marriage_Sat"] = "GOTO marriage_Mon";
                                    data["marriage_Sun"] = "GOTO marriage_Mon";
                                });
                            if (e.Name.IsEquivalentTo("Characters/schedules/Sophia") && Config.Allow_Sophia == true)
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data["marriage_Mon"] = $"620 IslandFarmHouse 3 5 0 \"Characters\\Dialogue\\Sophia:marriage_islandhouse\"/1100 IslandWest 82 48 2/1400 IslandNorth 17 36 3 \"Characters\\Dialogue\\Sophia:marriage_loc2_scenery\"/1600 {SGIValues.RandomMap_nPos(Random, "Sophia", HasExGIM, Config.ScheduleRandom)}/a2150 IslandFarmHouse 3 5 2";
                                    data["marriage_Tue"] = "GOTO marriage_Mon";
                                    data["marriage_Wed"] = "GOTO marriage_Mon";
                                    data["marriage_Thu"] = "GOTO marriage_Mon";
                                    data["marriage_Fri"] = "GOTO marriage_Mon";
                                    data["marriage_Sat"] = "GOTO marriage_Mon";
                                    data["marriage_Sun"] = "GOTO marriage_Mon";
                                });
                            if (e.Name.IsEquivalentTo("Characters/schedules/Victor") && Config.Allow_Victor == true)
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data["marriage_Mon"] = $"620 FishShop 4 7 0/900 IslandSouth 1 11/940 IslandWest 77 43 0/1020 IslandFarmHouse 14 9 2 \"Characters\\Dialogue\\Victor:marriage_islandhouse\"/1100 IslandSouth 11 23 2 Victor_Wine2/1400 IslandFieldOffice 5 4 0 \"Characters\\Dialogue\\Victor:marriage_loc2\"/1600 {SGIValues.RandomMap_nPos(Random, "Victor", HasExGIM, Config.ScheduleRandom)}/a2150 IslandFarmHouse 14 9 2";
                                    data["marriage_Tue"] = "GOTO marriage_Mon";
                                    data["marriage_Wed"] = "GOTO marriage_Mon";
                                    data["marriage_Thu"] = "GOTO marriage_Mon";
                                    data["marriage_Fri"] = "GOTO marriage_Mon";
                                    data["marriage_Sat"] = "GOTO marriage_Mon";
                                    data["marriage_Sun"] = "GOTO marriage_Mon";
                                });
                        }
                        foreach (ContentPackData cpd in CustomSchedule.Values)
                        {
                            if (e.NameWithoutLocale.IsEquivalentTo($"Characters/schedules/{cpd.Spousename}") && Config.CustomChance >= RandomizedInt)
                            {
                                string temp_loc3 = Commands.IsLoc3Valid(cpd);
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data["marriage_Mon"] = $"620 IslandFarmHouse {cpd.ArrivalPosition} \"Characters\\Dialogue\\{cpd.Spousename}:marriage_islandhouse\"/{cpd.Location1.Time} {cpd.Location1.Name} {cpd.Location1.Position} \"Characters\\Dialogue\\{cpd.Spousename}:marriage_loc1\"/{cpd.Location2.Time} {cpd.Location2.Name} {cpd.Location2.Position} \"Characters\\Dialogue\\{cpd.Spousename}:marriage_loc2\"/{temp_loc3}a2150 IslandFarmHouse {cpd.ArrivalPosition}";
                                    data["marriage_Tue"] = "GOTO marriage_Mon";
                                    data["marriage_Wed"] = "GOTO marriage_Mon";
                                    data["marriage_Thu"] = "GOTO marriage_Mon";
                                    data["marriage_Fri"] = "GOTO marriage_Mon";
                                    data["marriage_Sat"] = "GOTO marriage_Mon";
                                    data["marriage_Sun"] = "GOTO marriage_Mon";
                                }
                              );
                                this.Monitor.LogOnce($"Edited the marriage schedule of {cpd.Spousename}.", LogLevel.Debug);
                                if (!SchedulesEdited.Contains(cpd.Spousename))
                                {
                                    SchedulesEdited.Add(cpd.Spousename);
                                }
                            }
                        }
                    }
                    if (e.Name.StartsWith("Characters/Dialogue/", false, true))
                    {
                        foreach (ContentPackData cpd in CustomSchedule.Values)
                        {
                            /*first check which file its calling for:
                             If dialogue, check whether translations exist. (If they don't, just patch file. If they do, check the specific file being requested.)
                            */
                            if (e.NameWithoutLocale.IsEquivalentTo($"Characters/Dialogue/{cpd.Spousename}"))
                            {
                                if (cpd.Translations.Count is 0 || cpd.Translations is null)
                                {
                                    this.Monitor.LogOnce($"No translations found in {cpd.Spousename} contentpack. Patching all dialogue files with default dialogue", LogLevel.Trace);
                                    e.Edit(asset => Commands.EditDialogue(cpd, asset, this.Monitor));
                                    this.Monitor.Log($"Added Ginger Island dialogue to {cpd.Spousename} data.", LogLevel.Debug);
                                    if (!DialoguesEdited.Contains(cpd.Spousename))
                                    {
                                        DialoguesEdited.Add(cpd.Spousename);
                                    }
                                }
                                else
                                {
                                    if (e.Name.IsEquivalentTo($"Characters/Dialogue/{cpd.Spousename}"))
                                    {
                                        e.Edit(asset => Commands.EditDialogue(cpd, asset, this.Monitor));
                                        this.Monitor.Log($"Added Ginger Island dialogue to {cpd.Spousename} data.", LogLevel.Debug);
                                        if (!DialoguesEdited.Contains(cpd.Spousename))
                                        {
                                            DialoguesEdited.Add(cpd.Spousename);
                                        }
                                    }
                                    foreach (DialogueTranslation kpv in cpd.Translations)
                                    {
                                        if (e.Name.IsEquivalentTo($"Characters/schedules/{cpd?.Spousename}{Commands.ParseLangCode(kpv?.Key)}") && Commands.IsListValid(kpv) is true)
                                        {
                                            this.Monitor.LogOnce($"Found '{kpv.Key}' translation for {cpd.Spousename} dialogue!", LogLevel.Trace);
                                            e.Edit(asset =>
                                            {
                                                IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                                data["marriage_islandhouse"] = kpv.Arrival;
                                                data["marriage_loc1"] = kpv.Location1;
                                                data["marriage_loc2"] = kpv.Location2;
                                                data["marriage_loc3"] = kpv.Location3?.ToString();
                                            });
                                            if (!TranslationsAdded.Contains($"{cpd.Spousename} ({kpv.Key})"))
                                            {
                                                TranslationsAdded.Add($"{cpd.Spousename} ({kpv.Key})");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (e.Name.StartsWith("Characters/Dialogue/Marr", true, false))
                        {
                            if (e.Name.IsEquivalentTo("Characters/Dialogue/MarriageDialogueKrobus.es-ES"))
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data.Add("funLeave_Krobus", "Intentaré ir fuera hoy...si voy temprano, tu gente no se dará cuenta$0.#$b#Pasar tiempo contigo me ha hecho ganar interés por las actividades de tu gente.$1");
                                });
                            if (e.Name.IsEquivalentTo("Characters/Dialogue/MarriageDialogueKrobus"))
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data.Add("funLeave_Krobus", "I'll go outside today...if i'm quick, your people won't notice.$0#$b#Thanks to you, i've become curious of humans' \"Entertainment activities\".$1");
                                });
                        }
                        else
                        {
                            if (currentLang is "es")
                            {
                                SGIData.DialoguesSpanish(e, Config);
                                if (e.Name.IsEquivalentTo("Characters/Dialogue/Devan.es-ES"))
                                {
                                    e.LoadFromModFile<Dictionary<string, string>>("assets/Devan/Dialogue.es-ES.json", AssetLoadPriority.Medium);
                                };
                            }
                            if (currentLang is "en")
                            {
                                SGIData.DialoguesEnglish(e, Config);
                                if (e.Name.IsEquivalentTo("Characters/Dialogue/Devan"))
                                {
                                    e.LoadFromModFile<Dictionary<string, string>>("assets/Devan/Dialogue.json", AssetLoadPriority.Medium);
                                };
                            }
                        }

                    }
                }
                if (e.Name.StartsWith("Characters/", false, false))
                {
                    if (e.Name.IsEquivalentTo("Characters/Devan"))
                    {
                        e.LoadFromModFile<Texture2D>("assets/Devan/Character.png", AssetLoadPriority.Medium);
                    };
                    if (e.Name.IsEquivalentTo("Characters/Harvey") && Config.Allow_Harvey == true && Config.CustomChance >= RandomizedInt)
                    {
                        e.Edit(asset =>
                        {
                            var editor = asset.AsImage();
                            Texture2D Harvey = Helper.ModContent.Load<Texture2D>("assets/Spouses/Harvey_anim.png");
                            editor.PatchImage(Harvey, new Rectangle(0, 192, 64, 32), new Rectangle(0, 192, 64, 32), PatchMode.Replace);
                        });
                    }
                    if (e.Name.IsEquivalentTo("Characters/Krobus") && Config.Allow_Krobus == true && Config.CustomChance >= RandomizedInt)
                    {
                        e.LoadFromModFile<Texture2D>("assets/Spouses/Krobus_Outside_Character.png", AssetLoadPriority.Medium);
                    }
                }
                if (e.Name.StartsWith("Portraits/", false, true))
                {
                    if (e.Name.IsEquivalentTo("Portraits/Devan"))
                    {
                        e.LoadFromModFile<Texture2D>("assets/Devan/Portrait.png", AssetLoadPriority.Medium);
                    }
                    if (e.Name.IsEquivalentTo("Portraits/Krobus") && Config.Allow_Krobus == true && Config.CustomChance >= RandomizedInt)
                    {
                        e.LoadFromModFile<Texture2D>("assets/Spouses/Krobus_Outside_Portrait.png", AssetLoadPriority.Medium);
                    }
                }
                if (Config.NPCDevan is true)
                {
                    if (e.Name.StartsWith("Data/", false, false))
                    {
                        this.Monitor.LogOnce("Adding Devan", LogLevel.Trace);
                        if (e.Name.IsEquivalentTo("Data/animationDescriptions"))
                            e.Edit(asset =>
                            {
                                IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                data.Add("Devan_washing", "18/16 16 16 17 17 17/18");
                                data.Add("Devan_plate", "23/20/23");
                                data.Add("Devan_cook", "18/21 21 21 21 22 22 22 22/18");
                                data.Add("Devan_bottle", "23/19/23");
                                data.Add("Devan_spoon", "18/24 24 24 24 25 25 25 25/18");
                                data.Add("Devan_broom", "0 23 23 31 31/26 26 26 27 27 27 28 28 28 29 29 29 28 28 28 27 27 27/31 31 23 23 0");
                                data.Add("Devan_sleep", "34 34 34 34 35 35 35/32/35 35 35 35 35 34 34 34");
                                data.Add("Devan_think", "34 34 34/33/34 34 34 34");
                                data.Add("Devan_sit", "0 0 38 38 38 37 37/36/37 37 38 38 38 0 0");
                                data.Add("shane_charlie", "29/28/29");
                                data.Add("harvey_excercise_island", "24/24 24 25 25 26 27 27 27 27 27 26 25 25 24 24 24/24");
                                data.Add("krobus_napping", "17/17/17");
                            });
                        if (e.Name.IsEquivalentTo("Data/NPCExclusions"))
                            e.Edit(asset =>
                            {
                                IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                data.Add("Devan", "MovieInvite Socialize IslandEvent");
                                data.Add("Babysitter", "All");
                            });
                    }
                    if (e.Name.StartsWith("Data/Festivals/", false, false))
                    {
                        SGIData.AppendFestivalData(e);
                    }
                    if (currentLang is "es")
                    {
                        if (e.Name.StartsWith("Data/", false, false))
                        {
                            if (e.Name.IsEquivalentTo("Data/NPCGiftTastes.es-ES"))
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data.Add("Devan", "¡Siempre sabes qué regalar, @! Es mi favorito./395 432 424 296/Gracias, @. Me gusta mucho./399 410 403 240/...No creo que me sirva mucho./86 84 80 446/...¿Qué mal broma es esta?/287 288 348 346 303 459 873/Gracias, lo guardaré./82 440 349 246/");
                                });
                            if (e.Name.IsEquivalentTo("Data/NPCDispositions.es-ES"))
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data.Add("Devan", "adult/polite/outgoing/neutral/undefined/not-datable/null/Town/fall 3/Gus 'Jefe'/Saloon 44 5/Devan");
                                });
                            if (e.Name.IsEquivalentTo("Data/Mail.es-ES"))
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data.Add("Devan", "@,^Encontré esto mientras compraba en Pierre's, y me acordé de ti. A lo mejor te sirve.   ^   -Devan%item object 270 1 424 1 256 2 419 1 264 1 400 1 254 1 %%[#]Un regalo de Devan");
                                });
                        }
                        if (e.Name.StartsWith("Data/Events/", false, false))
                        {
                            SGIData.EventsSpanish(e);
                        }
                        if (e.Name.StartsWith("Data/Festivals/", false, false))
                        {
                            SGIData.FesSpanish(e);
                        }
                    }
                    if (currentLang is "en")
                    {
                        if (e.Name.StartsWith("Data/", false, false))
                        {
                            if (e.Name.IsEquivalentTo("Data/NPCGiftTastes"))
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data.Add("Devan", "I love this! How did you know?/395 432 424 296/Thanks, @. This is great./399 410 403 240/...I'm not sure i can use this./86 84 80 446/...Ugh, is this a joke?/287 288 348 346 303 459 873/Thanks, i'll save it./82 440 349 246/");
                                });
                            if (e.Name.IsEquivalentTo("Data/NPCDispositions"))
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data.Add("Devan", "adult/polite/outgoing/neutral/undefined/not-datable/null/Town/fall 3/Gus 'Boss'/Saloon 44 5/Devan");
                                });
                            if (e.Name.IsEquivalentTo("Data/Mail"))
                                e.Edit(asset =>
                                {
                                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                                    data.Add("Devan", "@,^I found this while buying groceries, and thought of you. I bet it'll be useful for your farm.   ^   -Devan%item object 270 1 424 1 256 2 419 1 264 1 400 1 254 1 %%[#]A Gift From Devan");
                                });
                        }
                        if (e.Name.StartsWith("Data/Events/", false, false))
                        {
                            SGIData.EventsEnglish(e);
                        }
                        if (e.Name.StartsWith("Data/Festivals/", false, false))
                        {
                            SGIData.FesEnglish(e);
                        }
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

        //load starts here
        foreach (keyvaluepair kvp in IntegratedSpouses)
        {
            bool LoadThisSpouse = Data.IsSpouseEnabled(kvp.Key, Config);
            if (kvp.Value is true && LoadThisSpouse is true)
            {
                //T as Dictionary<string, string>;
                Helper.GameContent.Load<Dictionary<string, string>>($"Characters/schedules/{kvp.Key}");
                Helper.GameContent.Load<Dictionary<string, string>>($"Characters/Dialogue/{kvp.Key}");
                if (kvp.Key is "Krobus")
                {
                    Helper.GameContent.Load<Dictionary<string, string>>($"Characters/Dialogue/MarriageDialogueKrobus");
                }
            }
        }
    }
    private void OnDayEnding(object sender, DayEndingEventArgs e)
    {
        CanDoHeavyLoad = true;

        PreviousDayRandom = RandomizedInt;
        RandomizedInt = Random.Next(1, 101);
        foreach (string str in SchedulesEdited)
        {
            Helper.GameContent.InvalidateCache($"Characters/schedules/{str}");
        }
        foreach (string s in MarriedtoNPC.Keys)
        {
            MarriedtoNPC.TryGetValue($"{s}", out bool IsTempMarried);
            if (IsTempMarried is true)
            {
                Helper.GameContent.InvalidateCache($"Characters/schedules/{s}");
            }
        }
        if (IsKrobusRoommate is true)
        {
            Helper.GameContent.InvalidateCache($"Characters/Krobus");
            Helper.GameContent.InvalidateCache($"Portraits/Krobus");
        }
        if (Children.Count >= 1 && HasC2N is true && Config.Allow_Children == true)
        {
            foreach (var child in Children)
            {
                Helper.GameContent.InvalidateCache($"Characters/schedules/{child.Name}");
            }
        }
        if (Config.NPCDevan == true && Children.Count >= 1)
        {
            Helper.GameContent.InvalidateCache("Characters/schedules/Devan");
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
        if (e.NewTime is 600 || e.OldTime is 600)
        {
            CanDoHeavyLoad = false;
        }
    }
    private void OnLocaleChanged(object sender, LocaleChangedEventArgs e)
    {
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
        Debugging.Reset(this, args, Helper);
    }
    internal void SGI_Help(string command, string[] args)
    {
        this.Monitor.Log(this.Helper.Translation.Get("CLI.helpdescription"), LogLevel.Info);
    }

    internal Texture2D KbcSamples() => Helper.ModContent.Load<Texture2D>("assets/kbcSamples.png");

    internal Dictionary<string, bool> MarriedtoNPC = new();
    internal List<string> SchedulesEdited = new();
    internal List<string> DialoguesEdited = new();
    internal List<string> TranslationsAdded = new();
    internal List<Child> Children = new();
    internal string currentLang = LanguageInfo.GetLanguageCode();
    internal string SpouseT()
    {
        var SpousesGrlTitle = this.Helper.Translation.Get("config.Vanillas.name");
        return SpousesGrlTitle;
    }
    internal string SpouseD()
    {
        var SpousesDesc = this.Helper.Translation.Get("config.Vanillas.description");
        return SpousesDesc;
    }
    internal string SVET()
    {
        var sve = "SVE";
        return sve;
    }
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
