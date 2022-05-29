using System;
using System.Collections.Generic;
using System.Linq;
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
        public bool HideSpouseRoom { get; set; } = false;
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
    }
    public class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
            helper.Events.GameLoop.SaveLoaded += this.OnSaveLoaded;
            helper.Events.GameLoop.DayEnding += this.OnDayEnding;
            helper.Events.Content.AssetRequested += this.OnAssetRequested;
            helper.Events.GameLoop.TimeChanged += this.OnTimeChanged;
            //mod config ahead
            this.Config = this.Helper.ReadConfig<ModConfig>();
            int CustomChance = this.Config.CustomChance;
            bool ScheduleRandom = this.Config.ScheduleRandom;
            bool CustomRoom = this.Config.CustomRoom;
            bool HideSpouseRoom = this.Config.HideSpouseRoom;
            string Childbedcolor = this.Config.Childbedcolor;
            bool NPCDevan = this.Config.NPCDevan;
            bool Allow_Children = this.Config.Allow_Children;
            bool Allow_Abigail = this.Config.Allow_Abigail;
            bool Allow_Alex = this.Config.Allow_Alex;
            bool Allow_Elliott = this.Config.Allow_Elliott;
            bool Allow_Emily = this.Config.Allow_Emily;
            bool Allow_Haley = this.Config.Allow_Haley;
            bool Allow_Harvey = this.Config.Allow_Harvey;
            bool Allow_Krobus = this.Config.Allow_Krobus;
            bool Allow_Leah = this.Config.Allow_Leah;
            bool Allow_Maru = this.Config.Allow_Maru;
            bool Allow_Penny = this.Config.Allow_Penny;
            bool Allow_Sam = this.Config.Allow_Sam;
            bool Allow_Sebastian = this.Config.Allow_Sebastian;
            bool Allow_Shane = this.Config.Allow_Shane;
            bool Allow_Claire = this.Config.Allow_Claire;
            bool Allow_Lance = this.Config.Allow_Lance;
            bool Allow_Magnus = this.Config.Allow_Magnus;
            bool Allow_Olivia = this.Config.Allow_Olivia;
            bool Allow_Sophia = this.Config.Allow_Sophia;
            bool Allow_Victor = this.Config.Allow_Victor;
            //??
            int RandomizedInt = this.RandomizedInt;
            //commands
            helper.ConsoleCommands.Add("sgi_help", helper.Translation.Get("CLI.help"), this.SGI_Help);
            helper.ConsoleCommands.Add("sgi_chance", helper.Translation.Get("CLI.chance"), this.SGI_Chance);
            helper.ConsoleCommands.Add("sgi_reset", helper.Translation.Get("CLI.reset"), this.SGI_Reset);
            helper.ConsoleCommands.Add("sgi_list", helper.Translation.Get("CLI.list"), this.SGI_List);
            helper.ConsoleCommands.Add("sgi_about", helper.Translation.Get("CLI.about"), this.SGI_About);
        }

        [XmlIgnore]
        public int RandomizedInt;
        public static Dictionary<string, ContentPackData> CustomSchedule = new ();
        
        /* Private
         * Can only be accessed by this mod.
         * Contents: Events called by Entry, player config, static Random.
         */
        private void OnGameLaunched(object sender, GameLaunchedEventArgs e)
        {
            HasSVE = Commands.HasMod("FlashShifter.StardewValleyExpandedCP", this.Helper);
            HasC2N = Commands.HasMod("Loe2run.ChildToNPC", this.Helper);
            HasExGIM = Commands.HasMod("mistyspring.extraGImaps", this.Helper);
            RandomizedInt = Random.Next(1, 100);
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
            /*this is set at the top so it doesn't overwrite krobus data by accident*/
            if (e.Name.IsEquivalentTo("Characters/schedules/Krobus") && Config.CustomChance >= RandomizedInt)
            { e.LoadFromModFile<Dictionary<string, string>>("assets/Spouses/Empty.json", AssetLoadPriority.Low); }

            /*First is framework data, then NPC stuff.*/
            foreach (ContentPackData cpd in CustomSchedule.Values)
            {
                //if translations exist, patch files accordingly. If not, use default dialogue for all languages
                if (cpd.Translations.Count is 0 || cpd.Translations is null)
                {
                    if (e.NameWithoutLocale.IsEquivalentTo($"Characters/Dialogue/{cpd.Spousename}"))
                    {
                        this.Monitor.LogOnce($"No translations found in {cpd.Spousename} contentpack. Patching all dialogue files with default dialogue", LogLevel.Trace);
                        e.Edit(asset => Commands.EditDialogue(cpd, asset, this.Monitor));
                        this.Monitor.Log($"Added Ginger Island dialogue to {cpd.Spousename} data.", LogLevel.Debug);
                        if (!DialoguesEdited.Contains(cpd.Spousename))
                        {
                            DialoguesEdited.Add(cpd.Spousename);
                        }
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
                //add schedule if visit day
                if (Config.CustomChance >= RandomizedInt)
                {
                    this.Monitor.Log("Parsing name string and editing schedule.", LogLevel.Trace);
                    if (e.NameWithoutLocale.IsEquivalentTo($"Characters/schedules/{cpd.Spousename}"))
                    {
                        string temp_loc3 = "";
                        if (Commands.IsLoc3Valid(cpd))
                        {
                            temp_loc3 = $"{cpd.Location3?.Time} {cpd.Location3?.Name} {cpd.Location3?.Position} \"Characters\\Dialogue\\{cpd.Spousename}:marriage_loc3\"/";
                        }
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
            
            if (Config.NPCDevan == true)
            {
                this.Monitor.LogOnce("Adding Devan", LogLevel.Trace);
                if (e.Name.IsEquivalentTo("Portraits/Devan"))
                {
                    e.LoadFromModFile<Texture2D>("assets/Devan/Portrait.png", AssetLoadPriority.Medium);
                }
                if (e.Name.IsEquivalentTo("Characters/Devan"))
                {
                    e.LoadFromModFile<Texture2D>("assets/Devan/Character.png", AssetLoadPriority.Medium);
                };
                //saloon addition
                if (e.Name.IsEquivalentTo("Maps/Saloon") && HasSVE is false)
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
                if (e.Name.IsEquivalentTo("Maps/Saloon") && HasSVE is true)
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

                if (SawDevan4H == true && e.Name.IsEquivalentTo("Maps/Saloon"))
                    e.Edit(asset =>
                    {
                        //edit map to have picture in devans room be of their hometown
                        var editor = asset.AsMap();
                        Map sourceMap = Helper.ModContent.Load<Map>("assets/Maps/z_Devan_post4h.tbin");
                        editor.PatchMap(sourceMap, sourceArea: new Rectangle(0, 0, 2, 2), targetArea: new Rectangle(40, 1, 2, 2));
                    });
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
                if (e.Name.IsEquivalentTo("Characters/schedules/Devan") && Config.CustomChance >= RandomizedInt && Children.Count >= 1)
                {
                    e.LoadFromModFile<Dictionary<string, string>>("assets/Devan/Schedule_Babysit.json", AssetLoadPriority.Medium);
                };
                if (e.Name.IsEquivalentTo("Characters/schedules/Devan") && Config.CustomChance < RandomizedInt && Children.Count >= 1)
                {
                    e.LoadFromModFile<Dictionary<string, string>>("assets/Devan/Schedule_Normal.json", AssetLoadPriority.Medium);
                };
                if (e.Name.IsEquivalentTo("Characters/schedules/Devan") && Config.NPCDevan == true && Children.Count <= 0)
                {
                    e.LoadFromModFile<Dictionary<string, string>>("assets/Devan/Schedule_Normal.json", AssetLoadPriority.Medium);
                };
                if (e.Name.IsEquivalentTo("Characters/schedules/Devan") && CCC == true)
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["Wed"] = "640 Saloon 39 7 2/650 Saloon 35 8 2/700 Saloon 14 17 2 Devan_broom/810 Saloon 13 22 2 Devan_broom/920 Saloon 32 19 2 Devan_broom/1020 Saloon 42 19 2 Devan_broom/1100 Saloon 33 8 2 Devan_broom/1200 Saloon 24 19 Devan_broom/1300 CommunityCenter 26 17 0 \"Characters\\Dialogue\\Devan:CommunityCenter1\"/1600 CommunityCenter 16 20 0/1630 CommunityCenter 11 27 2 Devan_sit \"Characters\\Dialogue\\Devan:CommunityCenter2\"/1700 Town 26 21 2/a2140 Saloon 31 19 1/2150 Saloon 31 8 2/2200 Saloon 39 7 0/2210 Saloon 44 5 3 devan_sleep";
                    });
                //married to leah
                if (e.Name.IsEquivalentTo("Characters/schedules/Devan") && IsLeahMarried is true && IsElliottMarried is false)
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["Fri"] = "640 Saloon 39 7 2/650 Saloon 35 8 2/700 Saloon 14 17 2 Devan_broom/810 Saloon 13 22 2 Devan_broom/920 Saloon 32 19 2 Devan_broom/1020 Saloon 42 19 2 Devan_broom/1100 Saloon 33 8 2 Devan_broom/1200 Saloon 24 19 Devan_broom/1300 Woods 8 9 0 \"Characters\\Dialogue\\Devan:statue\"/1600 ElliottHouse 5 8 1/1800 ElliottHouse 8 4 Devan_sit/a2140 Saloon 31 19 1/2150 Saloon 31 8 2/2200 39 7 0/2210 Saloon 44 5 3 devan_sleep";
                    });
                //married to elliott
                if (e.Name.IsEquivalentTo("Characters/schedules/Devan") && IsLeahMarried is false && IsElliottMarried is true)
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["Fri"] = "640 Saloon 39 7 2/650 Saloon 35 8 2/700 Saloon 14 17 2 Devan_broom/810 Saloon 13 22 2 Devan_broom/920 Saloon 32 19 2 Devan_broom/1020 Saloon 42 19 2 Devan_broom/1100 Saloon 33 8 2 Devan_broom/1200 Saloon 24 19 Devan_broom/1300 LeahHouse 6 7 0 \"Characters\\Dialogue\\Devan:leahHouse\"/1500 LeahHouse 13 4 2 Devan_sit \"Characters\\Dialogue\\Devan:leahHouse_2\"/1600 Woods 12 6 2 Devan_sit \"Characters\\Dialogue\\Devan:secretforest\"/1800 Woods 10 17 2/a2140 Saloon 31 19 1/2150 Saloon 31 8 2/2200 39 7 0/2210 Saloon 44 5 3 devan_sleep";
                    });
                //married to both
                if (e.Name.IsEquivalentTo("Characters/schedules/Devan") && IsLeahMarried is true && IsElliottMarried is true)
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["Fri"] = "640 Saloon 39 7 2/650 Saloon 35 8 2/700 Saloon 14 17 2 Devan_broom/810 Saloon 13 22 2 Devan_broom/920 Saloon 32 19 2 Devan_broom/1020 Saloon 42 19 2 Devan_broom/1100 Saloon 33 8 2 Devan_broom/1200 Saloon 24 19 Devan_broom/1300 Woods 8 9 0 \"Characters\\Dialogue\\Devan:statue\"/1800 Woods 12 6 2 Devan_sit \"Characters\\Dialogue\\Devan:secretforest\"/1900 Woods 10 17 2/a2140 Saloon 31 19 1/2150 Saloon 31 8 2/2200 39 7 0/2210 Saloon 44 5 3 devan_sleep";
                    });
                //add edit to make sat sch. compatible with SVE
                if (e.Name.IsEquivalentTo("Characters/schedules/Devan") && HasSVE is true)
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["Sat"] = "640 Saloon 39 7 2/650 Saloon 35 8 2/700 Saloon 14 17 2 Devan_broom/810 Saloon 13 22 2 Devan_broom/920 Saloon 32 19 2 Devan_broom/1020 Saloon 42 19 2 Devan_broom/1100 Saloon 33 8 2 Devan_broom/1200 Saloon 24 19 Devan_broom/1300 Forest 43 10 2 \"Characters\\Dialogue\\Devan:forest\"/1530 Forest 45 41 0/a1840 Forest 102 64 0 \"Characters\\Dialogue\\Devan:forest_2\"/a2140 Saloon 31 19 1/2150 Saloon 31 8 2/2200 39 7 0/2210 Saloon 44 5 3 devan_sleep";
                    });
                //append data
                if (e.NameWithoutLocale.IsEquivalentTo("Data/Festivals/spring13"))
                    e.Edit(asset => {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        if (data.ContainsKey("Set-Up_additionalCharacters"))
                        {
                            string spring13_setup;
                            data.TryGetValue("Set-Up_additionalCharacters", out spring13_setup);
                            data["Set-Up_additionalCharacters"] = spring13_setup + "/Devan 25 69 up";
                        }
                        else
                        {
                            data["Set-Up_additionalCharacters"] = "Devan 25 69 up";
                        }

                        if (data.ContainsKey("MainEvent_additionalCharacters"))
                        {
                            string spring13_main;
                            data.TryGetValue("MainEvent_additionalCharacters", out spring13_main);
                            data["MainEvent_additionalCharacters"] = spring13_main + "/Devan 25 73 up";
                        }
                        else
                        {
                            data["MainEvent_additionalCharacters"] = "Devan 25 73 up";
                        }


                    });
                if (e.NameWithoutLocale.IsEquivalentTo("Data/Festivals/spring24"))
                    e.Edit(asset => {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        if (data.ContainsKey("Set-Up_additionalCharacters"))
                        {
                            string spring24_setup;
                            data.TryGetValue("Set-Up_additionalCharacters", out spring24_setup);
                            data["Set-Up_additionalCharacters"] = spring24_setup + "/Devan 9 34 down";
                        }
                        else
                        {
                            data["Set-Up_additionalCharacters"] = "Devan 9 34 down";
                        }
                        if (data.ContainsKey("MainEvent_additionalCharacters"))
                        {
                            string spring24_main;
                            data.TryGetValue("MainEvent_additionalCharacters", out spring24_main);
                            data["MainEvent_additionalCharacters"] = spring24_main + "/Devan 8 30 up";
                        }
                        else
                        {
                            data["MainEvent_additionalCharacters"] = "Devan 8 30 up";
                        }
                    });

                if (e.NameWithoutLocale.IsEquivalentTo("Data/Festivals/summer11"))
                    e.Edit(asset => {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        if (data.ContainsKey("Set-Up_additionalCharacters"))
                        {
                            string summer11_setup;
                            data.TryGetValue("Set-Up_additionalCharacters", out summer11_setup);
                            data["Set-Up_additionalCharacters"] = summer11_setup + "/Devan 13 9 down";
                        }
                        else
                        {
                            data["Set-Up_additionalCharacters"] = "Devan 13 9 down";
                        }
                        if (data.ContainsKey("MainEvent_additionalCharacters"))
                        {
                            string summer11_main;
                            data.TryGetValue("MainEvent_additionalCharacters", out summer11_main);
                            data["MainEvent_additionalCharacters"] = summer11_main + "/Devan 30 14 right";
                        }
                        else
                        {
                            data["MainEvent_additionalCharacters"] = "Devan 30 14 right";
                        }
                    });

                if (e.NameWithoutLocale.IsEquivalentTo("Data/Festivals/summer28"))
                    e.Edit(asset => {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        if (data.ContainsKey("Set-Up_additionalCharacters"))
                        {
                            string summer28_setup;
                            data.TryGetValue("Set-Up_additionalCharacters", out summer28_setup);
                            data["Set-Up_additionalCharacters"] = summer28_setup + "/Devan 11 18 left";
                        }
                        else
                        {
                            data["Set-Up_additionalCharacters"] = "Devan 11 18 left";
                        }
                    });

                if (e.NameWithoutLocale.IsEquivalentTo("Data/Festivals/fall16"))
                    e.Edit(asset => {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        if (data.ContainsKey("Set-Up_additionalCharacters"))
                        {
                            string fall16_setup;
                            data.TryGetValue("Set-Up_additionalCharacters", out fall16_setup);
                            data["Set-Up_additionalCharacters"] = fall16_setup + "/Devan 66 65 down";
                        }
                        else
                        {
                            data["Set-Up_additionalCharacters"] = "Devan 66 65 down";
                        }
                    });

                if (e.NameWithoutLocale.IsEquivalentTo("Data/Festivals/fall27"))
                    e.Edit(asset => {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        if (data.ContainsKey("Set-Up_additionalCharacters"))
                        {
                            string fall27_setup;
                            data.TryGetValue("Set-Up_additionalCharacters", out fall27_setup);
                            data["Set-Up_additionalCharacters"] = fall27_setup + "/Devan 27 68 up";
                        }
                        else
                        {
                            data["Set-Up_additionalCharacters"] = "Devan 27 68 up";
                        }
                    });

                if (e.NameWithoutLocale.IsEquivalentTo("Data/Festivals/winter8"))
                    e.Edit(asset => {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        if (data.ContainsKey("Set-Up_additionalCharacters"))
                        {
                            string winter8_setup;
                            data.TryGetValue("Set-Up_additionalCharacters", out winter8_setup);
                            data["Set-Up_additionalCharacters"] = winter8_setup + "/Devan 66 14 right";
                        }
                        else
                        {
                            data["Set-Up_additionalCharacters"] = "Devan 66 14 right";
                        }
                        if (data.ContainsKey("MainEvent_additionalCharacters"))
                        {
                            string winter8_main;
                            data.TryGetValue("MainEvent_additionalCharacters", out winter8_main);
                            data["MainEvent_additionalCharacters"] = winter8_main + "/Devan 69 27 down";
                        }
                        else
                        {
                            data["MainEvent_additionalCharacters"] = "Devan 69 27 down";
                        }
                    });

                if (e.NameWithoutLocale.IsEquivalentTo("Data/Festivals/winter25"))
                    e.Edit(asset => {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        if (data.ContainsKey("Set-Up_additionalCharacters"))
                        {
                            string winter25_setup;
                            data.TryGetValue("Set-Up_additionalCharacters", out winter25_setup);
                            data["Set-Up_additionalCharacters"] = winter25_setup + "/Devan 23 74 up";
                        }
                        else
                        {
                            data["Set-Up_additionalCharacters"] = "Devan 23 74 up";
                        }
                    });
                //Devan - English data
                if (e.Name.IsEquivalentTo("Strings/StringsFromMaps"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("Saloon.DevanBook", "A book about trains in detail.");
                    });
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
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Devan"))
                {
                    e.LoadFromModFile<Dictionary<string, string>>("assets/Devan/Dialogue.json", AssetLoadPriority.Medium);
                };
                if (e.Name.IsEquivalentTo("Data/Mail"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("Devan", "@,^I found this while buying groceries, and thought of you. I bet it'll be useful for your farm.   ^   -Devan%item object 270 1 424 1 256 2 419 1 264 1 400 1 254 1 %%[#]A Gift From Devan");
                    });
                if (e.Name.IsEquivalentTo("Data/Events/Railroad"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("11037500/F/f Devan 500/p Devan", "breezy/32 45/Devan 39 41 2 farmer 29 47 0/skippable/pause 500/move farmer 0 -6 0/move farmer 5 0 1 continue/viewport move 1 -1 3000/pause 500/emote Devan 16/pause 1000/quickQuestion @?#What are you doing?#Are you waiting for someone?#(break)speak Devan \"I'm waiting for a train to pass by$0.#$b#They don't show up often, but the wait is worth it.$0#$b#Like birdwatching, but with trains.\"(break)speak Devan \"No, not really.\"/speak Devan \"I'm waiting for a train to pass by$0.#$b#They don't show up often, but the wait is worth it.$0#$b#Like birdwatching, but with trains.\"/quickQuestion #Have you seen any so far?#What's the point, anyways?#That sounds boring.(break)speak Devan \"Two, up until now.$0#$b#The last train was full of coal, and some bits dropped as it passed by...$2#$b#So I hope to see the train better next time.\"\\friendship Devan 20(break)speak Devan \"These machines are amazing. Did you know? They can run on 4 types of energy: Horse-pulled, Steam, Diesel, and Electricity.$1#$b#I used to live in a location where you could only get by train.$0\"(break)speak Devan \"Well, what's the point in farming?$5\"\\friendship Devan -50/pause 1000/emote Devan 40/speak Devan \"...At any rate, you can stay if you want.$0#$b#But i'm warning you, something may fall when the train comes.$0#$e#I hope you have something to cover yourself with.$1\"/emote farmer 28/end");
                    });
                if (e.Name.IsEquivalentTo("Data/Events/Forest"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("110371500/F/f Devan 1500/p Devan", "continue/34 25/Devan 34 25 2 farmer 33 25 2/skippable/pause 4000/speak Devan \"Hey, @.\"/pause 2000/speak Devan \"You know, i used to be a teacher there- at the town where i grew in. $0#$b#We were far from the big city, and sustained ourselves by farming.$0#$b#...Then things changed.$2\"/stopMusic/pause 500/playMusic desolate/speak Devan \"With time, the our water source dried up. People started moving out, because farming wasn't possible anymore...$2#$b#I stayed. I didn't want to leave my students behind, but their families needed to. And it was for their sake, too.#$b#When I moved to the city, I lost contact. It was hard to find a new job, and it all began to pile up.$2\"/pause 500/speak devan \"Then, I heard of the valley.$7\"/pause 2000/speak Devan \"When i arrived, i didn't have where to stay...but by some coincidence, i met Gus. He offered a spare room i could stay at- in exchange of working there.#$b#He said, many people had the same experience as i did.\"/stopMusic/pause 500/playMusic spring_night_ambient/pause 2000/quickQuestion Why did you move here, @?#I wanted to escape the city, too.#It was my grandfather's farm.#I wanted money.(break)speak Devan \"I see. So it's true...it's as if this place appears when you most need it.$7\"\\friendship Devan 50(break)speak Devan \"So, he left it to you? Makes sense.\"(break)speak Devan \"Really? That's it? $6#$b#You could make much more money at the city.\"\\friendship Devan -20(break)speak Devan \"Anyways, thanks for hearing my story.\"/end");
                    });
                if (e.Name.IsEquivalentTo("Data/Events/LeahHouse"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("110371000/F/f Devan 1000/p Leah", "jaunty/7 6/Leah 9 5 2 Devan 10 7 2 farmer 7 20 0/skippable/animate Leah false true 500 32 33 34/animate Devan false true 5000 20/pause 5000/speak Leah \"Stay still. I'm almost done.\"/speak Devan \"How longer? My arms are starting to hurt...$2#$b#The river sounds so nice...i might fall asleep.$1\"/speak Leah \"If you do that, i'll have to start over.$2\"/emote Devan 28/warp farmer 7 9/playSound doorClose/pause 500/speak Leah \"Hello, @. Sorry... I'm busy right now.$0#$b#Devan is helping me with a difficult pose i want to draw. I was having a hard time with this one.\"/speak Devan \"Hey, so about 'that'...$2#$b#What i commissioned last week. How is progress coming along?$1\"/speak Leah \"It's done. It came out just like you asked.\"/stopAnimation Leah 35/pause 500/animate Leah false false 500 8 9 10 11/playSound pickUpItem/pause 1000/stopAnimation Leah 0/addObject 8 5 99/pause 100/stopAnimation Devan 8/pause 600/speak Devan \"...$6#$b#...I swear to Yoba, it's amazing. It looks just like what i had in mind. $1\"/speak Leah \"After we're done here, you can take it home.\"/emote Devan 20/end");
                    });
                if (e.Name.IsEquivalentTo("Data/Events/Saloon"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("110372000/f Devan 2000/p Devan/t 2000 2600", "Saloon1/7 20/Devan 3 19 2 Elliott 2 20 1 Gus 10 18 2 Emily 15 17 0 Clint 19 23 3 Shane 21 17 2 Willy 17 22 2 farmer 4 21 0/skippable/showFrame Devan 36/pause 3000/speak Devan \"...Yeah, and that's what we did.\"/pause 100/emote farmer 32/pause 500/speak Elliott \"You seem a bit too amused...that crab almost tore my hair off.$2#$b#Though, you didn't escape their claws either.$1\"/speak Devan \"It's a bit hot...i'll go outside for a bit.\"/question fork goWithDevan staywithElliott \"@, do you want to come with me?#I'll go with you.#I'll stay with Elliott.\"/fork goWithDevan 2000_goWithDevan/fork staywithElliott 2000_staywithElliott");

                        data.Add("2000_goWithDevan", "skippable/stopMusic/changeLocation Town/viewport 45 72/globalFadeToClear 2000/pause 500/warp farmer 45 71 2/playSound doorClose/move farmer 0 1/move farmer -2 0/faceDirection farmer 2 continue/pause 1000/warp Devan 45 71/move Devan 0 1/playSound doorClose/pause 1500/speak Devan \"Hey, @.#$e#I wanted to tell you something.#$e#You know...the town where i grew wasnt always so lonely.$0#$b#We used to be a tight knit community. The farms made money selling their goods, and would sell them to many places in Ferngill.\"/pause 500/speak Devan \"It was normal to see trains there. Sometimes, while i'd work at the school, i'd hear its whistle as it passed by.$0#$b#...Truth be told, i miss being teacher.$2#$b#I wonder if those kids, and their families, found a better place...$7\"/pause 1000/speak Devan \"Sitting to watch for trains calmed me down. It reminded me of home. I had no idea if i'd fit here, and i was scared­.$2#$b#But, @... nowadays, i feel i'm part of the community.$7#$b#You've been a valuable friend to me­.$1#$b#I loved the town i grew in, and i'll miss it... but it's okay to move on, too. This is my home now.\"/emote Devan 20/speak Devan \"...Elliot must be getting bored. We should head back.#$e#Hey, do you want a drink?$6#$b#My treat.$1\"/end dialogue Devan \"...#$b#I really appreciate you, @.$1\"");

                        data.Add("2000_staywithElliott", "skippable/speak Devan \"Sure.#$b#I won't take too long.$1\"/pause 500/move Devan 3 0/move Devan 0 4/move Devan 8 0/move Devan 0 1/playSound doorClose/warp Devan -20 -20/pause 500/stopMusic/speak Elliott \"I get the feeling we'll have a while to talk.#$e#...@?$1#$b#Lately, i've noticed something quite intriguing.\"/pause 1000/speak Elliott \"...At first, Devan didn't talk much with our neighbors.#$b#They seemed...off, to be honest. Quite too distant.$2#$e#Except when Leah is around. Then, Devan loosens up a bit.\"/pause 500/emote Elliott 40/pause 500/speak Elliott \"However...\"/pause 300/speak Elliott \"Ever since you two became friends, Devan seems happier.#$b#You've made Devan feel at home here.\"/pause 800/speak Elliott \"It's quite refreshing.$1\"/pause 200/playSound doorClose/[make devan visible]/move Devan 0 -2/speak Devan \"Hey, the breeze outside is very refreshing.#$b#Did i take too long?#$e#...Hey, what's with that face?$6\"/pause 100/emote farmer 40/emote Elliott 40/pause 500/speak Elliott \"Not much. We were making small talk while waiting.#$b#I'd like to continue telling @ about when we tried the crab pots by ourselves.\"/emote Devan 20/pause 200/move Devan 0 -1/move Devan -5 0/end");
                    });
                if (e.Name.IsEquivalentTo("Data/Festivals/spring13"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("Devan", "Will you participate in the festival?#$b#Me? Not really. I prefer watching everyone compete. It's more exciting.");
                    });
                if (e.Name.IsEquivalentTo("Data/Festivals/spring24"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("Devan", "Mm...delicious.$8#$b#@, try some. I bet you'll love it.$1");
                    });
                if (e.Name.IsEquivalentTo("Data/Festivals/summer11"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("Devan", "%Devan seems to be having fun with Emily.");
                    });
                if (e.Name.IsEquivalentTo("Data/Festivals/summer28"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("Devan", "Moonlight jellies... i'd never heard of that.$0#$b#Can't wait to see them.$1");
                    });
                if (e.Name.IsEquivalentTo("Data/Festivals/fall16"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("Devan", "These chickens look very well cared for...$0#$b#It shows that Marnie loves them very much.$1");
                    });
                if (e.Name.IsEquivalentTo("Data/Festivals/fall27"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("Devan", "Ive heard they made an impossible labyrinth over there...$0#$b#Maybe i'll go check it out later.$1");
                    });
                if (e.Name.IsEquivalentTo("Data/Festivals/winter8"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("Devan", "The best of this festival, you say...?$0#$b#I love to see everyone's snowmen.$0#$e#But, specially, i like Leah's ice sculptures.$1");
                    });
                if (e.Name.IsEquivalentTo("Data/Festivals/winter25"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("Devan", "Ah, hello.$0#$b#Have you tried Gus' candy canes? They turned out great.$0#$b#He prepared for a while to find the best ingredients.$1");
                    });

                //Devan - Spanish data
                if (e.Name.IsEquivalentTo("Strings/StringsFromMaps.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("Saloon.DevanBook", "Un libro sobre trenes en detalle.");
                    });
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
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Devan.es-ES"))
                {
                    e.LoadFromModFile<Dictionary<string, string>>("assets/Devan/Dialogue.es-ES.json", AssetLoadPriority.Medium);
                };
                if (e.Name.IsEquivalentTo("Data/Mail.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("Devan", "@,^Encontré esto mientras compraba en Pierre's, y me acordé de ti. A lo mejor te sirve.   ^   -Devan%item object 270 1 424 1 256 2 419 1 264 1 400 1 254 1 %%[#]Un regalo de Devan");
                    });
                if (e.Name.IsEquivalentTo("Data/Events/Railroad.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("11037500/F/f Devan 500/p Devan", "breezy/32 45/Devan 39 41 2 farmer 29 47 0/skippable/pause 500/move farmer 0 -6 0/move farmer 5 0 1 continue/viewport move 1 -1 3000/pause 500/emote Devan 16/pause 1000/quickQuestion @?#¿Qué estás haciendo?#¿Estás esperando a alguien?#(break)speak Devan \"Ah, ¿Esto?\"(break)speak Devan \"No, más bien a 'algo'.\"/speak Devan \"Estoy esperando que pase un tren.$0#$b#No pasan muy seguido, pero la espera vale la pena.$0\"/quickQuestion #Y ¿Has visto alguno?#¿Por qué lo haces?#Suena aburridor.(break)speak Devan \"Si, dos hasta ahora.$0#$b#El último iba con carbón, y me cayó uno cuando pasaba...$2#$b#Así que espero poder ver el siguiente con tranquilidad.\"\\friendship Devan 20(break)speak Devan \"Son máquinas increíbles. ¿Sabes? Pueden usar 4 tipos de energía: Tirado por caballos, a vapor, diesel y electricidad.$1#$b#Donde vivía antes, sólo podías llegar a través de un tren.$0\"(break)speak Devan \"¿Lo dice quien se la pasa en la granja?$5\"\\friendship Devan -50/pause 1000/emote Devan 40/speak Devan \"...De todas maneras, puedes quedarte si quieres.$0#$b#Pero te advierto, es probable que te caiga algo encima.$0#$e#Espero que tengas con qué cubrirte.$1\"/emote farmer 28/end");
                    });
                if (e.Name.IsEquivalentTo("Data/Events/Forest.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("110371500/F/f Devan 1500/p Devan", "continue/34 25/Devan 34 25 2 farmer 33 25 2/skippable/pause 4000/speak Devan \"Hey, @.\"/pause 2000/speak Devan \"¿Recuerdas la pintura que le compré a Leah?#$b#Era del pueblo donde crecí.#$e#Solía trabajar en un colegio.$0#$b#Estábamos lejos de la ciudad, así que sobrevivíamos con la granja.$0#$b#...Pero las cosas cambiaron.$2\"/stopMusic/pause 500/playMusic desolate/speak Devan \"Con el tiempo, nuestra fuente de agua se secó. La gente comenzó a irse...vivir allí se volvió imposible.$2#$b#Yo me quedé. Era mi pueblo natal, y no quería abandonar a mis estudiantes.$2#$b#Pero sus familias tenían que irse, y era por su propio bien.$2#$b#El pueblo se volvió un desierto. También lo dejé, y perdí el contacto.$2#$e#Cuando llegué a la ciudad... era tan diferente. Y no podía encontrar un buen trabajo.$2\"/pause 500/speak devan \"En ese momento, escuché del valle.$7\"/pause 2000/speak Devan \"Cuando llegué, no tenía donde quedarme...de coincidencia, conocí a Gus.$7#$b#Me ofreció un cuarto en el que quedarme, a cambio de trabajar en el salón.#$b#Según él, muchas personas pasaron por lo mismo.\"/stopMusic/pause 500/playMusic spring_night_ambient/pause 2000/quickQuestion ¿Por qué viniste a este pueblo, @?#También quería escapar de la ciudad.#Era la granja de mi abuelo.#Quería dinero.(break)speak Devan \"Así que es verdad...este lugar aparece cuando más lo necesitas.$7\"\\friendship Devan 50(break)speak Devan \"¿Te dejó la granja? Tiene sentido.\"(break)speak Devan \"¿En serio? ¿Sólo por eso?$6#$b#Podrías ganar mucho más en la ciudad.\"\\friendship Devan -20(break)speak Devan \"Bueno, gracias por escucharme.\"/end");
                    });
                if (e.Name.IsEquivalentTo("Data/Events/LeahHouse.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("110371000/F/f Devan 1000/p Leah", "jaunty/7 6/Leah 9 5 2 Devan 10 7 2 farmer 7 20 0/skippable/animate Leah false true 500 32 33 34/animate Devan false true 5000 20/pause 5000/speak Leah \"No te muevas. Ya casi termino.\"/speak Devan \"¿Cuánto queda? Me duelen los brazos...$2#$b#El río es tan calmante...creo que me va a adormecer.$1\"/speak Leah \"Si eso pasa, tendré que empezar de nuevo.$2\"/emote Devan 28/warp farmer 7 9/playSound doorClose/pause 500/speak Leah \"Hola, @. Disculpa... no puedo hablar ahora.$0#$b#Devan me está ayudando con una pose difícil que quería dibujar.\"/speak Devan \"Ey, sobre 'eso'...$2#$b#Lo que comisioné la semana pasada. ¿Cómo va el avance?$1\"/speak Leah \"Está listo.\"/stopAnimation Leah 35/pause 500/animate Leah false false 500 8 9 10 11/playSound pickUpItem/pause 1000/stopAnimation Leah 0/addObject 8 5 99/pause 100/stopAnimation Devan 8/pause 600/speak Devan \"...$6#$b#...Por el amor de Yoba. Es justo lo que quería. $1\"/speak Leah \"Cuando terminemos, puedes llevártelo a casa.\"/emote Devan 20/end");
                    });
                if (e.Name.IsEquivalentTo("Data/Events/Saloon.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("110372000/f Devan 2000/p Devan/t 2000 2600", "Saloon1/7 20/Devan 3 19 2 Elliott 2 20 1 Gus 10 18 2 Emily 15 17 0 Clint 19 23 3 Shane 21 17 2 Willy 17 22 2 farmer 4 21 0/skippable/showFrame Devan 36/pause 3000/speak Devan \"...Y tuvimos que hacer eso.\"/pause 100/emote farmer 32/pause 500/speak Elliott \"Parece que te estás divirtiendo mucho...ese cangrejo casi me arrancó el pelo.$2#$b#Aunque tampoco te escapaste de eso.$1\"/pause 500/speak Devan \"...Bueno, no.$2#$b#Hace calor...iré afuera un rato.\"/question fork goWithDevan staywithElliott \"¿Quieres ir, @?#Iré contigo.#Me quedaré con Elliott.\"/fork goWithDevan 2000_goWithDevan/fork staywithElliott 2000_staywithElliott");

                        data.Add("2000_goWithDevan", "skippable/stopMusic/changeLocation Town/viewport 45 72/globalFadeToClear 2000/pause 500/warp farmer 45 71 2/playSound doorClose/move farmer 0 1/move farmer -2 0/faceDirection farmer 2 continue/pause 1000/warp Devan 45 71/move Devan 0 1/playSound doorClose/pause 1500/speak Devan \"Hey, @.#$e#Quería decirte algo.#$e#¿Recuerdas que te he contado del pueblo donde nací?$0#$b#Solíamos ser una comunidad unida. Las granjas ganaban dinero vendiendo sus productos, incluso a lugares lejanos.\"/pause 500/speak Devan \"Por eso, era normal ver trenes. A veces, mientras trabajaba, escuchaba su sonido.$0#$b#...La verdad, extraño trabajar en un colegio.$2#$b#Me pregunto cómo estarán mis ex alumnos, y sus familias...$7\"/pause 1000/speak Devan \"Cuando llegué, tenía mucho miedo. Ya había perdido en la ciudad, y no sabía si encajaría­. Los trenes eran mi único consuelo.$2#$b#Pero, @... últimamente, siento que soy parte de la comunidad.$7#$b#Has sido un amigo valioso para mí­.$1#$b#Amaba mi pueblo, y lo extrañaré...pero está bien disfrutar el presente. Este es mi hogar ahora.\"/emote Devan 20/speak Devan \"...Elliot se debe estar aburriendo, deberíamos volver.#$e#Oye, ¿Quieres una bebida?$6#$b#Yo invito.$1\"/end dialogue Devan \"...#$b#Aprecio mucho nuestra amistad, @.$1\"");

                        data.Add("2000_staywithElliott", "skippable/speak Devan \"Sure.#$b#I won't take too long.$1\"/pause 500/move Devan 3 0/move Devan 0 4/move Devan 8 0/move Devan 0 1/playSound doorClose/warp Devan -20 -20/pause 500/stopMusic/speak Elliott \"Presiento que tendremos tiempo para hablar.#$e#...@?$1#$b#Últimamente, me he percatado de algo intrigante.\"/pause 1000/speak Elliott \"...Al principio, Devan era bastante distante.#$b#Para ser sincero...demasiado distante.$2#$e#Excepto cuando hablaba con Leah. Tienen un gusto en común por el bosque.\"/pause 500/emote Elliott 40/pause 500/speak Elliott \"Sin embargo...\"/pause 300/speak Elliott \"Desde que te le acercaste, devan se ve más feliz.#$b#Creo que le has hecho sentirse en casa, en nuestro pequeño pueblo.\"/pause 800/speak Elliott \"Me alegro que se lleven bien.$1\"/pause 200/playSound doorClose/warp Devan 14 24/move Devan 0 -2/speak Devan \"Oigan- el viento afuera está fresquito.#$b#¿Me demoré mucho?#$e#...¿Por qué ponen esa cara?$6\"/pause 100/emote farmer 40/emote Elliott 40/pause 500/speak Elliott \"No es por nada. Estábamos hablando mientras te fuiste.#$b#Me gustaría contarle más a @ sobre la vez que intentamos atrapar cangrejos.\"/emote Devan 20/pause 200/move Devan 0 -1/move Devan -5 0/end");
                    });
                if (e.Name.IsEquivalentTo("Data/Festivals/spring13.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("Devan", "¿Vas a participar en el festival?#$b#¿Yo? No. Prefiero mirar la competencia. Es más interesante.");
                    });
                if (e.Name.IsEquivalentTo("Data/Festivals/spring24.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("Devan", "Mm...sabe exquisito.$8#$b#@, prueba un poco. De seguro te encanta.$1");
                    });
                if (e.Name.IsEquivalentTo("Data/Festivals/summer11.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("Devan", "%Devan parece estar divirtiéndose con Emily.");
                    });
                if (e.Name.IsEquivalentTo("Data/Festivals/summer28.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("Devan", "Medusas lunares... nunca había escuchado algo así.$0#$b#No puedo esperar a verlas.$1");
                    });
                if (e.Name.IsEquivalentTo("Data/Festivals/fall16.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("Devan", "Estas gallinas se ven muy bien cuidadas.$0#$b#Se nota que Marnie las quiere mucho.$1");
                    });
                if (e.Name.IsEquivalentTo("Data/Festivals/fall27.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("Devan", "Escuché que hicieron un laberinto imposible allí arriba...$0#$b#Quizás vaya a verlo más tarde.$1");
                    });
                if (e.Name.IsEquivalentTo("Data/Festivals/winter8.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("Devan", "¿Lo mejor de este festival, dices...?$0#$b#Me gusta ver cómo decoran los muñecos de nieve.$0#$e#Pero especialmente, me gustan las esculturas de Leah.$1");
                    });
                if (e.Name.IsEquivalentTo("Data/Festivals/winter25.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("Devan", "Ah, hola.$0#$b#¿Has probado los bastones de Gus? Le quedaron excelente.$0#$b#Se preparó desde antes para encontrar todos los ingredientes.$1");
                    });
            }
            
            if (Config.CustomChance >= RandomizedInt)
            {
                if (HasC2N is true && Config.Allow_Children == true)
                {
                    this.Monitor.LogOnce("Child To NPC is in the mod folder. Adding compatibility...", LogLevel.Trace);
                    if (e.Name.IsEquivalentTo("Characters/schedules/" + Children?[0].Name))
                        e.Edit(asset =>
                        {
                            IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                            data["Mon"] = "620 IslandFarmHouse 20 10 3/1100 IslandWest 74 43 3/1400 IslandWest 83 36 3/1700 IslandWest 91 37 2/a1900 IslandFarmHouse 15 12 0/2000 IslandFarmHouse 30 15 2/2100 IslandFarmHouse 35 14 3";
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
                            data["Mon"] = "620 IslandFarmHouse 20 8 0/1030 IslandFarmHouse 22 6 1/1100 IslandWest 75 46 3/1400 IslandWest 84 38 0/1700 IslandWest 93 36 0/a1900 IslandFarmHouse 15 14 0/2000 IslandFarmHouse 27 14 2/2100 IslandFarmHouse 36 14 2";
                            data["Tue"] = "GOTO Mon";
                            data["Wed"] = "GOTO Mon";
                            data["Thu"] = "GOTO Mon";
                            data["Fri"] = "GOTO Mon";
                            data["Sat"] = "GOTO Mon";
                            data["Sun"] = "GOTO Mon";
                        });
                }
                if (e.Name.IsEquivalentTo("Portraits/Krobus") && Config.Allow_Krobus == true)
                {
                    e.LoadFromModFile<Texture2D>("assets/Spouses/Krobus_Outside_Portrait.png", AssetLoadPriority.Medium);
                }
                if (e.Name.IsEquivalentTo("Characters/Krobus") && Config.Allow_Krobus == true)
                {
                    e.LoadFromModFile<Texture2D>("assets/Spouses/Krobus_Outside_Character.png", AssetLoadPriority.Medium);
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
                if (e.Name.IsEquivalentTo("Characters/Harvey") && Config.Allow_Harvey == true)
                    e.Edit(asset => {
                        var editor = asset.AsImage();
                        Texture2D Harvey = Helper.ModContent.Load<Texture2D>("assets/Spouses/Harvey_anim.png");
                        //Map sourceMap = Helper.ModContent.Load<Map>("assets/Maps/z_SpouseRoomShell.tbin");
                        //editor.PatchMap(sourceMap, patchMode: PatchMapMode.ReplaceByLayer);
                        editor.PatchImage(Harvey, new Rectangle(0, 192, 64, 32), new Rectangle(0, 192, 64, 32), PatchMode.Replace);
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
            if (Config.Allow_Abigail == true)
            {
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Abigail"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Ah, hi @.$0#$b#This weather is so warm...$0";
                        data["marriage_loc1"] = "Do you think we can explore this volcano?$0#$b#Willy said we shouldn't get close..$2#$b#But I still brought my sword.$1";
                        data["marriage_loc3"] = "Do you ever think of fighting these slimes, @?";
                    });
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Abigail.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Ah, hola, @.$0#$b#Este clima es tan cálido..$0";
                        data["marriage_loc1"] = "Crees que podemos entrar a este volcán?$0#$b#Willy dijo quedebemos quedarnos en la playa...$2#$b#Pero aún traje mi espada.$1";
                        data["marriage_loc3"] = "¿Has luchado contra esas babas, @?";
                    });
            }
            if (Config.Allow_Alex == true)
            {
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Alex"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Oh, hey @. I'm finally here.$0#$b#I can't wait to spend the day with you.$1";
                        data["marriage_loc1"] = "%Alex is lifting weights.";
                        data["marriage_loc3"] = "Nothing's better than the beach on a hot day like this.$1";
                    });
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Alex.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Ey, @. Por fin estoy aquí.$1#$b#Ya quiero pasar todo el día contigo.$l";
                        data["marriage_loc1"] = "%Alex está haciendo ejercicio.";
                        data["marriage_loc3"] = "Phew, sienta bien un día en la playa con este calor.$1";
                    });
            }
            if (Config.Allow_Elliott == true)
            {
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Elliott"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Oh, @! That was quite the ride!$8#$b#It's a pleasure to see you again.$1";
                        data["marriage_loc1"] = "%Elliott is reading.";
                        data["marriage_loc3"] = "Ah, the view here is quite virtuous$0.#$b#I can feel an idea incoming...$1";
                    });
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Elliott.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Oh, @! ¡Llegar aquí fue arduoso!$8#$b#Es un placer volver a verte.$1";
                        data["marriage_loc1"] = "%Elliott está leyendo.";
                        data["marriage_loc3"] = "Ah, la vista desde aquí es armoniosa$0.#$b#Puedo sentir una idea en camino...$1";
                    });
            }
            if (Config.Allow_Emily == true)
            {
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Emily"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Hi, @!#$b#This island is full of parrots, it's lovely!";
                        data["marriage_loc1"] = "%Emily seems lost in her thoughts.";
                        data["marriage_loc3"] = "@, do you feel it?$0#$b#These crystals are full of powerful energy.$1";
                    });
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Emily.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "¡Hola, @!#$b#¡Esta isla está llena de aves hermosas!";
                        data["marriage_loc1"] = "%Emily está concentrada pensando.";
                        data["marriage_loc3"] = "@, ¿Sientes eso?$0#$b#Estos cristales son muy poderosos.$1";
                    });
            }
            if (Config.Allow_Haley == true)
            {
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Haley"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "@, can we go see the island today?$0#$b#There's something about this place that makes me feel at ease.";
                        data["marriage_loc1"] = "Hi, honey. I'm so glad you're here.$0#$b#I love how sunny the weather is.$1";
                        data["marriage_loc3"] = "%Haley is taking pictures.";
                    });
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Haley.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "@, ¿Podemos ver la isla hoy?$0#$b#Hay algo aquí que me hace sentir bien.";
                        data["marriage_loc1"] = "Hola, cariño.$0#$b#Amo el clima aquí.$1";
                        data["marriage_loc3"] = "%Haley está tomando fotos.";
                    });
            }
            if (Config.Allow_Harvey == true)
            {
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Harvey"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Hi, honey.$l#$b#This is so much nicer than being cooped up in the clinic all day.$1";
                        data["marriage_loc1"] = "I'll stay inside for a while, @.$0#$b#Make sure you stay hydrated in this weather, alright?$1";
                        data["marriage_loc3"] = "%Harvey is engrossed in a book.";
                    });
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Harvey.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Hola, cariño.$l#$b#Está bien tomar una cantidad moderada de luz solar a veces.$1";
                        data["marriage_loc1"] = "@, me quedaré adentro por un rato.$0#$b#Asegúrate de hidratarte en este clima, ¿está bien?$1";
                        data["marriage_loc3"] = "%Harvey está concentrado en un libro.";
                    });
            }
            if (Config.Allow_Krobus == true)
            {
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Krobus"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Good morning, @.$0#$b#The weather here is so damp, i feel right at home.$h#$e#And the sun is strong...$8";
                        data["marriage_loc1"] = "You said there were caves here...i'll visit them later.$0";
                        data["marriage_loc3"] = "I've never seen crystals like these!$0#$b#My people loved crystals, you know.$0#$e#...$2#$b#...You're right. Maybe there's others like me out there.#$b#Thank you, @.$h";
                    });
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Krobus.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Buenos días, @.$0#$b#El clima es tan húmedo, me siento como en casa.$h#$e#Y el sol es tan fuerte...$8";
                        data["marriage_loc1"] = "@, dijiste que habían cuevas aquí... Iré a verlas más tarde.$0";
                        data["marriage_loc3"] = "¡Nunca he visto estos cristales!$0#$b#A mi gente le fascinaban.$0#$e#...$2#$b#...Es cierto. Puede que hayan otros como yo allí afuera.#$b#Gracias, @.$h";
                    });
                if (e.Name.IsEquivalentTo("Characters/Dialogue/MarriageDialogueKrobus"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("funLeave_Krobus", "I'll go outside today...if i'm quick, your people won't notice.$0#$b#Thanks to you, i've become curious of humans' \"Entertainment activities\".$1");
                    });
                if (e.Name.IsEquivalentTo("Characters/Dialogue/MarriageDialogueKrobus.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data.Add("funLeave_Krobus", "Intentaré ir fuera hoy...si voy temprano, tu gente no se dará cuenta$0.#$b#Pasar tiempo contigo me ha hecho ganar interés por las actividades de tu gente.$1");
                    });
            }
            if (Config.Allow_Leah == true)
            {
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Leah"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "It's so quiet and peaceful here.$0#$b#It's the perfect weather for some wine.$1";
                        data["marriage_loc1"] = "The view here is inspiring, i just have to draw it.$0";
                        data["marriage_loc3"] = "%Leah is drawing.";
                    });
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Leah.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Está tranquilo aquí.$0#$b#Y el clima es perfecto para un poco de vino.$1";
                        data["marriage_loc1"] = "La vista desde aquí es hermosa. Necesito dibujarla.$0";
                        data["marriage_loc3"] = "%Leah está dibujando.";
                    });
            }
            if (Config.Allow_Maru == true)
            {
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Maru"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Hey! What's up?$0#$b#I can't wait to see the stars at night.$1";
                        data["marriage_loc1"] = "@! Have you seen this?$9#$b#It seem there hasn't been volcanic activity in a while.";
                        data["marriage_loc3"] = "Professor Snail has been telling us about Ginger Island's traditions.$0#$b#Their oral history is amazing.$1";
                    });
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Maru.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Ey, qué tal?$0#$b#No puedo esperar a ver las estrellas de noche.$1";
                        data["marriage_loc1"] = "¡@! ¿Has visto esto?$9#$b#Al parecer, no ha habido actividad volcánica en un tiempo.";
                        data["marriage_loc3"] = "El Profesor Snail nos ha contado sobre las tradiciones en Isla Gengibre.$0#$b#Sus historias orales son asombrosas.$1";
                    });
            }
            if (Config.Allow_Penny == true)
            {
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Penny"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Hi @.$h#$b#This island is so beautiful...";
                        data["marriage_loc1"] = "Oh, I'm reading the book about a sailor i told you about.$0#$b#It's very interesting.$1";
                        data["marriage_loc3"] = "Honey, Professor Snail's stories about the island are amazing.$0#$b#I didn't know they had so many traditions!$1";
                    });
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Penny.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Hola @.$h#$b#Esta isla es hermosa...";
                        data["marriage_loc1"] = "Oh, estoy leyendo el libro que traje la última vez.$0#$b#Es muy interesante.$1";
                        data["marriage_loc3"] = "Cariño, el Profesor Snail tiene anécdotas maravillosas sobre la isla.$0#$b#¡No sabía que tenían tradiciones tan interesantes!$1";
                    });
            }
            if (Config.Allow_Sam == true)
            {
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Sam"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Hi, @.$0#$b#It's a nice island... isn't it?$1";
                        data["marriage_loc1"] = "Hey... have you ever gone inside?$0#$b#...$0#$b#Really?$8";
                        data["marriage_loc3"] = "I'm feeling pretty good here.";
                    });
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Sam.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Hola, @.$0#$b#Es una isla grande, ¿no?$1";
                        data["marriage_loc1"] = "Oye... ¿Alguna vez has entrado?$0#$b#...$0#$b#¿En serio?$8";
                        data["marriage_loc3"] = "Ey, @. Me siento bien aquí.";
                    });
            }
            if (Config.Allow_Sebastian == true)
            {
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Sebastian"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Hey, I'm glad you're here.$h#$b#How have you been coping with this heat?";
                        data["marriage_loc1"] = "Man, this looks just like in 'Cave Saga X'...";
                        data["marriage_loc3"] = "I'm not one for crowded places.$0#$b#This place is perfect.$h";
                    });
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Sebastian.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Hey, que bueno que estás aquí.$h#$b#¿Cómo has lidiado con este calor?";
                        data["marriage_loc1"] = "Hombre, se parece mucho a 'Cave Saga X'...";
                        data["marriage_loc3"] = "No soy mucho de interacción social...$0#$b#Este lugar me sienta bien.$h";
                    });
            }
            if (Config.Allow_Shane == true)
            {
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Shane"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Hey, @.$1#$b#The tropical plants here are great.$1";
                        data["marriage_loc1"] = "Charlie seems to like this place.$8";
                        data["marriage_loc3"] = "Hmm...this place isn't so bad.";
                    });
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Shane.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Hola, @.$1#$b#Las plantas tropicales son hermosas aquí.$1";
                        data["marriage_loc1"] = "A Charlie le gusta este lugar.$8";
                        data["marriage_loc3"] = "Hmm...no está tan mal.";
                    });
            }
            if (Config.Allow_Claire == true)
            {
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Claire"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Hello, dear.$0#$b#I'm ready for this island's hot weather!$1";
                        data["marriage_loc1"] = "%Claire is reading a screenplay.";
                        data["marriage_loc3"] = "The birds here...$0#$b#There's so many of them. I can hear them chirping.$1";
                    });
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Claire.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Hola, cariño.$0#$b#¡Estoy lista para el clima de esta isla!$1";
                        data["marriage_loc1"] = "%Claire está leyendo.";
                        data["marriage_loc3"] = "Las aves de este lugar...$0#$b#Puedo escucharlas cantando.$1";
                    });
            }
            if (Config.Allow_Lance == true)
            {
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Lance"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Hello, my dear. This house is just as cozy as our farmland's.";
                        data["marriage_loc1"] = "The caldera in this volcano holds inmense arcane power.#$b#Only the most powerful of mages can cast the spells it requires.";
                        data["marriage_loc3"] = "The gentle waves of this beach...I've become accustomed to their sound.";
                    });
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Lance.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Hola, mi cariño. Esta casa es tan cómoda como la de nuestra granja.";
                        data["marriage_loc1"] = "La caldera en este volcán contiene un poder inmenso.#$b#Sólo los magos más poderosos pueden usar los hechizos que requiere.";
                        data["marriage_loc3"] = "Las olas de esta playa...Me he acostumbrado a su sonido calmante.";
                    });
            }
            if (Config.Allow_Magnus == true)
            {
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Wizard"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Hello, dearest! I'm overjoyed to see you.$1#$b#This place is quite wonderful. You've made the house quite cozy.";
                        data["marriage_loc1"] = "The creatures of this island are aware of our presence. It has alerted them.#$b#Their arcane power is inmense...";
                        data["marriage_loc3"] = "The forge in this place...the rituals it requires are only mastered by a handful.#$b#Even with such mastery, the results are near impossible to predict.";
                    });
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Wizard.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "¡Buenos días, amor! Estoy encantado de verte.$1#$b#Este lugar es hermoso. Has hecho esta choza bastante acogedora.";
                        data["marriage_loc1"] = "Las criaturas de esta isla se han percatado de nuestra presencia. Están a la defensiva.#$b#Su poder arcano es inmenso...";
                        data["marriage_loc3"] = "Este lugar...sólo unos pocos pueden llevar a cabo los rituales de la forja.#$b#Incluso con tal experiencia, es imposible predecir sus resultados.";
                    });
            }
            if (Config.Allow_Olivia == true)
            {
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Olivia"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Hi, sweetie. How is this weather treating you?$0#$b#We should invite Victor here someday.";
                        data["marriage_loc1"] = "Sautine City might have some competition...this place is just unique.$1#$b#You remembered i wanted a house in this island...didn't you, dear?$4";
                        data["marriage_loc3"] = "My, the ocean in this island is so clear. I would love to paint it some day.";
                    });
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Olivia.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Hola, cariño. ¿Cómo te ha tratado este clima?$0#$b#Deberíamos invitar a Victor uno de estos días.";
                        data["marriage_loc1"] = "Este lugar es único.$1#$b#Recordaste que quería una casa en esta isla...¿cierto, cariño?$4";
                        data["marriage_loc3"] = "El océano aquí es espectacular. Me encantaría pintarlo algún día.";
                    });
            }
            if (Config.Allow_Sophia == true)
            {
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Sophia"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "@, can we stay here tonight?#$b#I'm really enjoying this trip.$0#$b#We can? Yay!$1";
                        data["marriage_loc1"] = "Oh, @! Look, there's some fishes in the water.$1#$b#They're nibbling at something.$1#$e#...H-hey, be careful if you go to that volcano over there, okay?$0";
                        data["marriage_loc3"] = "This? It's a manga i picked up the other day.$0#$b#Wanna read it together?$0";
                    });
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Sophia.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "@, ¿Podemos quedarnos aquí hoy?#$b#Estoy disfrutando mucho este viaje.$0#$b#¿Si podemos? ¡Yay!$1";
                        data["marriage_loc1"] = "Oh, ¡@! Mira, hay unos peces aquí.$1#$b#Están comiendo algo.$1#$e#...H-hey, ten cuidado si vas al volcán de ahí arriba, ¿ok?$0";
                        data["marriage_loc3"] = "¿Esto? Es un manga que comencé a leer el otro día.$0#$b#¿Quieres verlo conmigo?$0";
                    });
            }
            if (Config.Allow_Victor == true)
            {
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Victor"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Hi, love. This beach is beautiful!#$b#Compared to Pelican Town, this weather is really hot...";
                        data["marriage_loc1"] = "So, this is the volcano dwarves used as an energy source...#$b#maybe we could go in someday.";
                        data["marriage_loc3"] = "Have you seen the bones here, @?#$b#These are quite ancient.";
                    });
                if (e.Name.IsEquivalentTo("Characters/Dialogue/Victor.es-ES"))
                    e.Edit(asset =>
                    {
                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_islandhouse"] = "Hola, cariño. ¡Este lugar es hermoso!#$b#Comparado a Pueblo Pelícano, el clima es abrasador...";
                        data["marriage_loc1"] = "Este es el volcán que los duendes usaban como fuente de energía...#$b#Tal vez podríamos entrar un día.";
                        data["marriage_loc3"] = "¿Has visto los fósiles que hay aquí, @?#$b#Están muy bien preservados.";
                    });
            }
            /* Map additions
             * First farmhouse (depending of kids+config), then patching paths onto island, then adding warps. (Island_W also includes barriers)
             */
            if (e.Name.IsEquivalentTo("Maps/IslandFarmHouse") && Config.Allow_Children == true && Children.Count >= 1)
            {
                e.LoadFromModFile<Map>($"assets/Maps/FarmHouse_kid_custom{Config.CustomRoom}.tbin", AssetLoadPriority.Medium);
                e.Edit(asset =>
                {
                    this.Monitor.Log("Patching child bed onto IslandFarmHouse...", LogLevel.Trace);
                    var editor = asset.AsMap();
                    Map sourceMap = Helper.ModContent.Load<Map>($"assets/Maps/kidbeds/z_kidbed_{Config.Childbedcolor}.tbin");
                    editor.PatchMap(sourceMap, sourceArea: new Rectangle(0, 0, 2, 4), targetArea: new Rectangle(35, 13, 2, 4), patchMode: PatchMapMode.Overlay);

                });
            };
            if (e.Name.IsEquivalentTo("Maps/IslandFarmHouse") && Config.CustomRoom == true && (Config.Allow_Children == false || Children.Count is 0))
            {
                e.LoadFromModFile<Map>("assets/Maps/FarmHouse_Custom.tbin", AssetLoadPriority.Medium);
            }
            //adds warps regardless of config above
            if (e.Name.IsEquivalentTo("Maps/IslandFarmHouse"))
            {
                e.Edit(asset =>
                {
                    var editor = asset.AsMap();
                    Map map = editor.Data;
                    map.Properties.Add("NPCWarp", "14 17 IslandWest 77 41");
                });
            }
            
            if (e.Name.IsEquivalentTo("Maps/Island_FieldOffice"))
            {
                e.Edit(asset => {
                    var editor = asset.AsMap();
                    this.Monitor.VerboseLog("Editing Island_FieldOffice...");
                    Map sourceMap = Helper.ModContent.Load<Map>("assets/Maps/z_Island_FieldOffice_paths.tbin");
                    editor.PatchMap(sourceMap, patchMode: PatchMapMode.ReplaceByLayer);
                    Map map = editor.Data;
                    map.Properties.Add("NPCWarp", "4 11 IslandNorth 46 46");
                });
            }
            if (e.Name.IsEquivalentTo("Maps/Island_N"))
            {
                e.Edit(asset => {
                    var editor = asset.AsMap();
                    Map sourceMap = Helper.ModContent.Load<Map>("assets/Maps/z_Island_N.tbin");
                    editor.PatchMap(sourceMap, patchMode: PatchMapMode.ReplaceByLayer);
                    Map map = editor.Data;
                    map.Properties.Add("NPCWarp", "22 22 IslandFarmHouse 14 16 35 89 IslandSouth 18 0 46 45 IslandFieldOffice 4 10 40 21 VolcanoEntrance 1 1 40 22 VolcanoEntrance 1 1");
                });
            }
            if (e.Name.IsEquivalentTo("Maps/Island_S"))
            {
                e.Edit(asset => {
                    var editor = asset.AsMap();
                    this.Monitor.VerboseLog("Editing IslandS...");
                    Map sourceMap = Helper.ModContent.Load<Map>("assets/Maps/z_Island_S.tbin");
                    editor.PatchMap(sourceMap, patchMode: PatchMapMode.ReplaceByLayer);
                    //add warps
                    Map map = editor.Data;
                    map.Properties.Remove("NPCWarp");
                    map.Properties.Add("NPCWarp", "17 44 FishShop 3 4 36 11 IslandEast 0 45 36 12 IslandEast 0 46 36 13 IslandEast 0 47 -1 11 IslandWest 105 40 -1 10 IslandWest 105 40 -1 12 IslandWest 105 40 -1 13 IslandWest 105 40 17 -1 IslandNorth 35 89 18 -1 IslandNorth 36 89 19 -1 IslandNorth 37 89 27 -1 IslandNorth 43 89 28 -1 IslandNorth 43 89 43 28 IslandSouthEast 0 29 43 29 IslandSouthEast 0 29 43 30 IslandSouthEast 0 29");
                });
            }
            if (e.Name.IsEquivalentTo("Maps/Island_SE"))
            {
                e.Edit(asset => {
                    var editor = asset.AsMap();
                    this.Monitor.VerboseLog("Editing IslandSE...");
                    Map sourceMap = Helper.ModContent.Load<Map>("assets/Maps/z_Island_SE.tbin");
                    editor.PatchMap(sourceMap, patchMode: PatchMapMode.ReplaceByLayer);

                    //add warps
                    Map map = editor.Data;
                    map.Properties.Add("NPCWarp", "0 29 IslandSouth 43 29 29 18 IslandSouthEastCave 1 8");
                });
            }
            if (e.Name.IsEquivalentTo("Maps/Island_SouthEastCave"))
            {
                e.Edit(asset => {
                    var editor = asset.AsMap();
                    this.Monitor.VerboseLog("Editing IslandSouthEastCave...");
                    Map sourceMap = Helper.ModContent.Load<Map>("assets/Maps/z_IslandSouthEastCave_paths.tbin");
                    editor.PatchMap(sourceMap, patchMode: PatchMapMode.ReplaceByLayer);
                    //add warps
                    Map map = editor.Data;
                    map.Properties.Add("NPCWarp", "0 7 IslandSouthEast 30 19");
                });
            }
            if (e.Name.IsEquivalentTo("Maps/IslandSouthEastCave_pirates"))
            {
                e.Edit(asset => {
                    var editor = asset.AsMap();
                    this.Monitor.VerboseLog("Editing IslandSouthEastCave_pirates...");
                    Map sourceMap = Helper.ModContent.Load<Map>("assets/Maps/z_IslandSouthEastCave_pirates_paths.tbin");
                    editor.PatchMap(sourceMap, patchMode: PatchMapMode.ReplaceByLayer);
                    //add warps
                    Map map = editor.Data;
                    map.Properties.Add("NPCWarp", "0 7 IslandSouthEast 30 19");
                });
            }
            if (e.Name.IsEquivalentTo("Maps/IslandWestCave1"))
            {
                e.Edit(asset => {
                    var editor = asset.AsMap();
                    this.Monitor.VerboseLog("Editing IslandWestCave1...");
                    Map sourceMap = Helper.ModContent.Load<Map>("assets/Maps/z_IslandWestCave1_paths.tbin");
                    editor.PatchMap(sourceMap, patchMode: PatchMapMode.ReplaceByLayer);
                    //add warps
                    Map map = editor.Data;
                    map.Properties.Add("NPCWarp", "6 12 IslandWest 61 5");
                });
            }
            if (e.Name.IsEquivalentTo("Maps/Custom_IslandShell"))
            {
                e.Edit(asset => {
                    var editor = asset.AsMap();
                    Map sourceMap = Helper.ModContent.Load<Map>("assets/Maps/z_SpouseRoomShell.tbin");
                    editor.PatchMap(sourceMap, patchMode: PatchMapMode.ReplaceByLayer);
                });
            }
            if (e.Name.IsEquivalentTo("Maps/Custom_IslandShell_freelove"))
            {
                e.Edit(asset => {
                    var editor = asset.AsMap();
                    Map sourceMap = Helper.ModContent.Load<Map>("assets/Maps/z_SpouseRoomShell_fl.tbin");
                    editor.PatchMap(sourceMap, patchMode: PatchMapMode.ReplaceByLayer);
                });
            }
            
            if (e.Name.IsEquivalentTo("Maps/FishShop"))
            {
                e.Edit(asset => {
                    var editor = asset.AsMap();
                    this.Monitor.VerboseLog("Editing FishShop...");
                    Map map = editor.Data;
                    map.Properties.Add("NPCWarp", "4 3 IslandSouth 19 43");
                });
            }
            if (e.Name.IsEquivalentTo("Maps/IslandEast"))
            {
                e.Edit(asset => {
                    var editor = asset.AsMap();
                    Map map = editor.Data;
                    map.Properties.Add("NPCWarp", "-1 45 IslandSouth 35 11 -1 46 IslandSouth 35 12 -1 47 IslandSouth 35 13 -1 48 IslandSouth 35 13 22 9 IslandHut 7 13 34 30 IslandShrine 13 28");
                });
            }
            if (e.Name.IsEquivalentTo("Maps/IslandShrine"))
            {
                e.Edit(asset => {
                    var editor = asset.AsMap();
                    Map map = editor.Data;
                    map.Properties.Add("NPCWarp", "12 27 IslandEast 33 30 12 28 IslandEast 33 30 12 29 IslandEast 33 30");
                });
            }
            if (e.Name.IsEquivalentTo("Maps/IslandFarmCave"))
            {
                e.Edit(asset => {
                    var editor = asset.AsMap();
                    Map map = editor.Data;
                    map.Properties.Add("NPCWarp", "4 10 IslandSouth 97 35");
                });
            }
            if (e.Name.IsEquivalentTo("Maps/Island_W"))
            {
                e.Edit(asset => {
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
        }
        private void OnDayEnding(object sender, DayEndingEventArgs e)
        {
            PreviousDayRandom = RandomizedInt;
            RandomizedInt = Random.Next(1, 100);
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
            else
            {
                return;
            }
        }
        

        private ModConfig Config;
        private static Random random;

        /*   Internal (can only be accessed by current .cs) */
        internal void SGI_About(string command, string[] args)
        {
            if (LocalizedContentManager.CurrentLanguageCode.Equals(LocalizedContentManager.LanguageCode.es))
            {
                this.Monitor.Log("Este mod permite que tu pareja vaya a la isla (compatible con ChildToNPC, SVE y otros). También permite crear paquetes de contenido / agregar rutinas personalizadas.\nMod creado por mistyspring (nexusmods)", LogLevel.Info);
            }
            //if(LocalizedContentManager.CurrentLanguageCode.Equals(LocalizedContentManager.LanguageCode.en))
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
                    string tempsched= "";
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
                    this.Monitor.Log($"Internal info: \n\n IsLeahMarried = {IsLeahMarried}; \n\n IsElliottMarried = {IsElliottMarried}; \n\n IsKrobusRoommate = {IsKrobusRoommate}; \n\n PreviousDayRandom = {PreviousDayRandom}; \n\n CCC = {CCC}; \n\n SawDevan4H = {SawDevan4H}; \n\n HasSVE = {HasSVE}; \n\n HasC2N = {HasC2N}; \n\n HasExGIM = {HasExGIM};", LogLevel.Info);
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
                    this.Monitor.Log($"Config: \n\n CustomChance: {Config.CustomChance}; \n\n ScheduleRandom = {Config.ScheduleRandom}; \n\n CustomRoom = {Config.CustomRoom}; \n\n HideSpouseRoom = {Config.HideSpouseRoom}; \n\n Childbedcolor = {Config.Childbedcolor}; \n\n NPCDevan = {Config.NPCDevan}; \n\n Allow_Children = {Config.Allow_Children};", LogLevel.Info);
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

        internal Texture2D KbcSamples() => Helper.ModContent.Load<Texture2D>("assets/kbcSamples.png");

        internal Dictionary<string, bool> MarriedtoNPC = new ();
        internal List<string> SchedulesEdited = new ();
        internal List<string> DialoguesEdited = new ();
        internal List<string> TranslationsAdded = new ();
        internal List<Child> Children = new ();

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
        internal int PreviousDayRandom;
        internal bool CCC;
        internal bool SawDevan4H = false;
        internal bool HasSVE;
        internal bool HasC2N;
        internal bool HasExGIM;
    }
}
