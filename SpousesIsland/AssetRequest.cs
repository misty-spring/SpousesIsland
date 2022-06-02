using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpousesIsland.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using System;
using System.Collections.Generic;
using xTile;
using xTile.Tiles;

namespace SpousesIsland
{
    internal class AssetRequest
    {
        internal static void Maps(AssetRequestedEventArgs e, IModHelper Helper)
        {
            if (e.Name.IsEquivalentTo("Maps/FishShop"))
            {
                e.Edit(asset =>
                {
                    var editor = asset.AsMap();
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
        }
        internal static void IslandMaps(ModEntry modEntry, AssetRequestedEventArgs e, ModConfig Config)
        {
            if (e.Name.StartsWith("Maps/Island", true, false))
            {
                if (e.Name.IsEquivalentTo("Maps/IslandFarmHouse"))
                {
                    if (Config.CustomRoom == true && (Config.Allow_Children == false || modEntry.Children.Count is 0))
                    {
                        e.LoadFromModFile<Map>("assets/Maps/FarmHouse_Custom.tbin", AssetLoadPriority.Medium);
                    }
                    if (Config.Allow_Children == true && modEntry.Children.Count >= 1)
                    {
                        e.LoadFromModFile<Map>($"assets/Maps/FarmHouse_kid_custom{Config.CustomRoom}.tbin", AssetLoadPriority.Medium);
                        e.Edit(asset =>
                        {
                            modEntry.Monitor.Log("Patching child bed onto IslandFarmHouse...", LogLevel.Trace);
                            var editor = asset.AsMap();
                            Map sourceMap = modEntry.Helper.ModContent.Load<Map>($"assets/Maps/kidbeds/z_kidbed_{Config.Childbedcolor}.tbin");
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
                        modEntry.Monitor.VerboseLog("Editing IslandSouthEastCave_pirates...");
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
        }
        internal static void Dialogue(ModEntry modEntry, AssetRequestedEventArgs e, IModHelper Helper)
        {
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
                if (modEntry.currentLang is "es")
                {
                    SGIData.DialoguesSpanish(e, modEntry.EnabledSpouses);
                    if (e.Name.IsEquivalentTo("Characters/Dialogue/Devan.es-ES"))
                    {
                        e.LoadFromModFile<Dictionary<string, string>>("assets/Devan/Dialogue.es-ES.json", AssetLoadPriority.Medium);
                    };
                }
                if (modEntry.currentLang is "en")
                {
                    SGIData.DialoguesEnglish(e, modEntry.EnabledSpouses);
                    if (e.Name.IsEquivalentTo("Characters/Dialogue/Devan"))
                    {
                        e.LoadFromModFile<Dictionary<string, string>>("assets/Devan/Dialogue.json", AssetLoadPriority.Medium);
                    };
                }
            }
        }
        internal static void ChangeSchedules(ModEntry modEntry, AssetRequestedEventArgs e, Random Random, ModConfig Config)
        {
            /*this is set at the top so it doesn't overwrite krobus data by accident*/
            if (e.Name.IsEquivalentTo("Characters/schedules/Krobus"))
            { e.LoadFromModFile<Dictionary<string, string>>("assets/Spouses/Empty.json", AssetLoadPriority.Low); }
            //integrated data
            if (modEntry.HasC2N is true && Config.Allow_Children == true && modEntry.Children.Count is not 0)
            {
                modEntry.Monitor.LogOnce("Child To NPC is in the mod folder. Adding compatibility...", LogLevel.Trace);
                if (e.Name.IsEquivalentTo("Characters/schedules/" + modEntry.Children?[0].Name))
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
                if (e.Name.IsEquivalentTo("Characters/schedules/" + modEntry.Children?[1].Name))
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
            if (e.Name.IsEquivalentTo("Characters/schedules/Abigail") && modEntry.EnabledSpouses.Contains("Abigail"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 16 9 0 \"Strings\\schedules\\Abigail:marriage_islandhouse\"/1100 IslandNorth 44 28 0 \"Strings\\schedules\\Abigail:marriage_loc1\"/a1800 {SGIValues.RandomMap_nPos(Random, "Abigail", modEntry.HasExGIM, Config.ScheduleRandom)}/2000 IslandWest 39 41 0 \"Strings\\schedules\\Abigail:marriage_loc3\"/a2200 IslandFarmHouse 16 9 0";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.Name.IsEquivalentTo("Characters/schedules/Alex") && modEntry.EnabledSpouses.Contains("Alex"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 19 6 0 \"Strings\\schedules\\Alex:marriage_islandhouse\"/1100 IslandWest 85 39 2 alex_lift_weights \"Strings\\schedules\\Alex:marriage_loc1\"/1300 {SGIValues.RandomMap_nPos(Random, "Alex", modEntry.HasExGIM, Config.ScheduleRandom)}/1500 IslandWest 64 83 2/a1900 IslandSouth 12 27 2 \"Strings\\schedules\\Alex:marriage_loc3\"/a2200 IslandFarmHouse 19 6 0";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.Name.IsEquivalentTo("Characters/schedules/Elliott") && modEntry.EnabledSpouses.Contains("Elliott"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 9 9 2 \"Strings\\schedules\\Elliott:marriage_islandhouse\"/1100 IslandWest 102 77 2 elliott_read/1400 IslandWest 73 83 2 \"Strings\\schedules\\Elliott:marriage_loc2\"/a1900 {SGIValues.RandomMap_nPos(Random, "Elliott", modEntry.HasExGIM, Config.ScheduleRandom)}/a2200 IslandFarmHouse 9 9 2";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.Name.IsEquivalentTo("Characters/schedules/Emily") && modEntry.EnabledSpouses.Contains("Emily"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 12 10 \"Strings\\schedules\\Emily:marriage_islandhouse\"/1100 IslandWest 53 52 2 \"Strings\\schedules\\Emily:marriage_loc1\"/1400 IslandWest 89 79 2 emily_exercise/1700 {SGIValues.RandomMap_nPos(Random, "Emily", modEntry.HasExGIM, Config.ScheduleRandom)}/a2200 IslandFarmHouse 12 10";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.Name.IsEquivalentTo("Characters/schedules/Haley") && modEntry.EnabledSpouses.Contains("Haley"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 8 6 2 \"Strings\\schedules\\Haley:marriage_islandhouse\"/1100 IslandNorth 32 74 0 \"Strings\\schedules\\Haley:marriage_loc1\"/1400 {SGIValues.RandomMap_nPos(Random, "Haley", modEntry.HasExGIM, Config.ScheduleRandom)}/1900 IslandWest 80 45 2/a2200 IslandFarmHouse 8 6 0";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.Name.IsEquivalentTo("Characters/schedules/Harvey") && modEntry.EnabledSpouses.Contains("Harvey"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 16 13 0 \"Strings\\schedules\\Harvey:marriage_islandhouse\"/1100 IslandFarmHouse 3 5 0 \"Strings\\schedules\\Harvey:marriage_loc1\"/1400 IslandWest 89 75 2 harvey_excercise/1600 {SGIValues.RandomMap_nPos(Random, "Harvey", modEntry.HasExGIM, Config.ScheduleRandom)}/a2100 IslandFarmHouse 16 13";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });

            if (e.Name.IsEquivalentTo("Characters/schedules/Krobus") && modEntry.EnabledSpouses.Contains("Krobus"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 15 10 0 \"Characters\\Dialogue\\Krobus:marriage_islandhouse\"/1100 IslandFarmHouse 10 10 3 \"Characters\\Dialogue\\Krobus:marriage_loc1\"/1130 IslandFarmHouse 9 8 0/1200 IslandFarmHouse 9 11 2/1400 IslandWestCave1 8 8 0 \"Characters\\Dialogue\\Krobus:marriage_loc3\"/1500 IslandWestCave1 9 8 0/1600 IslandWestCave1 9 6 3/1900 {SGIValues.RandomMap_nPos(Random, "Krobus", modEntry.HasExGIM, Config.ScheduleRandom)}/a2200 IslandFarmHouse 15 10 0";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.Name.IsEquivalentTo("Characters/schedules/Leah") && modEntry.EnabledSpouses.Contains("Leah"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 21 13 1 \"Strings\\schedules\\Leah:marriage_islandhouse\"/1100 IslandNorth 50 25 0 leah_draw \"Strings\\schedules\\Leah:marriage_loc1\"/1400 IslandNorth 21 16 0/1600 {SGIValues.RandomMap_nPos(Random, "Leah", modEntry.HasExGIM, Config.ScheduleRandom)}/a2200 IslandFarmHouse 21 13 1";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.Name.IsEquivalentTo("Characters/schedules/Maru") && modEntry.EnabledSpouses.Contains("Maru"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 18 15 2 \"Strings\\schedules\\Maru:marriage_islandhouse\"/1100 IslandWest 95 45 2/1400 IslandNorth 50 25 0 \"Strings\\schedules\\Maru:marriage_loc1\"/1700 {SGIValues.RandomMap_nPos(Random, "Maru", modEntry.HasExGIM, Config.ScheduleRandom)}/a2200 IslandFarmHouse 18 15 2";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                }
            );
            if (e.Name.IsEquivalentTo("Characters/schedules/Penny") && modEntry.EnabledSpouses.Contains("Penny"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 9 12 1 \"Strings\\schedules\\Penny:marriage_islandhouse\"/1100 IslandFarmHouse 3 6 0/1400 IslandWest 83 37 3 penny_read \"Strings\\schedules\\Penny:marriage_loc1\"/1700 {SGIValues.RandomMap_nPos(Random, "Penny", modEntry.HasExGIM, Config.ScheduleRandom)}/a2200 IslandFarmHouse 9 12 1";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.Name.IsEquivalentTo("Characters/schedules/Sam") && modEntry.EnabledSpouses.Contains("Sam"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 22 6 2 \"Strings\\schedules\\Sam:marriage_islandhouse\"/1100 IslandFarmHouse 8 9 0 sam_guitar/1400 IslandNorth 36 27 0 sam_skateboarding \"Strings\\schedules\\Sam:marriage_loc1\"/1700 {SGIValues.RandomMap_nPos(Random, "Sam", modEntry.HasExGIM, Config.ScheduleRandom)}/a2200 IslandFarmHouse 22 6 2";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.Name.IsEquivalentTo("Characters/schedules/Sebastian") && modEntry.EnabledSpouses.Contains("Sebastian"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 25 14 3 \"Strings\\schedules\\Sebastian:marriage_islandhouse\"/1100 IslandWest 88 14 0/1400 IslandWestCave1 6 4 0 \"Strings\\schedules\\Sebastian:marriage_loc1\"/1600 {SGIValues.RandomMap_nPos(Random, "Sebastian", modEntry.HasExGIM, Config.ScheduleRandom)}/a2200 IslandFarmHouse 25 14 3";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.Name.IsEquivalentTo("Characters/schedules/Shane") && modEntry.EnabledSpouses.Contains("Shane"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 20 5 0 \"Strings\\schedules\\Shane:marriage_islandhouse\"/1100 IslandWest 87 52 0 shane_charlie \"Strings\\schedules\\Shane:marriage_loc1\"/a1420 IslandWest 77 39 0/1430 IslandFarmHouse 15 9 0 shane_drink/a1900 {SGIValues.RandomMap_nPos(Random, "Shane", modEntry.HasExGIM, Config.ScheduleRandom)}/a2150 IslandWest 82 43/2200 IslandFarmHouse 20 5 0";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            //sve
            if (e.Name.IsEquivalentTo("Characters/schedules/Claire") && modEntry.EnabledSpouses.Contains("Claire"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 5 6 0 \"Characters\\Dialogue\\Claire:marriage_islandhouse\"/1100 IslandFarmHouse 17 12 2 Claire_Read \"Characters\\Dialogue\\Claire:marriage_loc1\"/1400 IslandEast 19 40 0 \"Characters\\Dialogue\\Claire:marriage_loc3\"/1600 {SGIValues.RandomMap_nPos(Random, "Claire", modEntry.HasExGIM, Config.ScheduleRandom)}/a2150 IslandFarmHouse 5 6 0";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.Name.IsEquivalentTo("Characters/schedules/Lance") && modEntry.EnabledSpouses.Contains("Lance"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 13 13 1 \"Characters\\Dialogue\\Lance:marriage_islandhouse\"/1100 IslandNorth 37 30 0/1400 Caldera 24 23 2 \"Characters\\Dialogue\\Lance:marriage_loc2\"/1600 {SGIValues.RandomMap_nPos(Random, "Lance", modEntry.HasExGIM, Config.ScheduleRandom)}/a2150 IslandFarmHouse 13 13 1";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.Name.IsEquivalentTo("Characters/schedules/Wizard") && modEntry.EnabledSpouses.Contains("Magnus"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 26 6 0 \"Characters\\Dialogue\\Wizard:marriage_islandhouse\"/1100 IslandWest 38 38 0 \"Characters\\Dialogue\\Wizard:marriage_loc1\"/1400 IslandSouthEast 28 26 2/1800 {SGIValues.RandomMap_nPos(Random, "Magnus", modEntry.HasExGIM, Config.ScheduleRandom)}/a2150 IslandFarmHouse 26 6 0";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.Name.IsEquivalentTo("Characters/schedules/Olivia") && modEntry.EnabledSpouses.Contains("Olivia"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 7 11 1\"Characters\\Dialogue\\Olivia:marriage_islandhouse\"/1100 IslandFarmHouse 14 9 0 Olivia_Wine1 \"Characters\\Dialogue\\Olivia:marriage_loc1\"/1400 IslandSouth 31 24 2 Olivia_Yoga/1600 {SGIValues.RandomMap_nPos(Random, "Olivia", modEntry.HasExGIM, Config.ScheduleRandom)}/a2150 IslandFarmHouse 7 11 1";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.Name.IsEquivalentTo("Characters/schedules/Sophia") && modEntry.EnabledSpouses.Contains("Sophia"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 3 5 0 \"Characters\\Dialogue\\Sophia:marriage_islandhouse\"/1100 IslandWest 82 48 2/1400 IslandNorth 17 36 3 \"Characters\\Dialogue\\Sophia:marriage_loc2_scenery\"/1600 {SGIValues.RandomMap_nPos(Random, "Sophia", modEntry.HasExGIM, Config.ScheduleRandom)}/a2150 IslandFarmHouse 3 5 2";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.Name.IsEquivalentTo("Characters/schedules/Victor") && modEntry.EnabledSpouses.Contains("Victor"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 FishShop 4 7 0/900 IslandSouth 1 11/940 IslandWest 77 43 0/1020 IslandFarmHouse 14 9 2 \"Characters\\Dialogue\\Victor:marriage_islandhouse\"/1100 IslandSouth 11 23 2 Victor_Wine2/1400 IslandFieldOffice 5 4 0 \"Characters\\Dialogue\\Victor:marriage_loc2\"/1600 {SGIValues.RandomMap_nPos(Random, "Victor", modEntry.HasExGIM, Config.ScheduleRandom)}/a2150 IslandFarmHouse 14 9 2";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
        }
        internal static void ContentPackSchedule(ModEntry modEntry, AssetRequestedEventArgs e, Random Random, ContentPackData cpd)
        {
            if (e.NameWithoutLocale.IsEquivalentTo($"Characters/schedules/{cpd.Spousename}"))
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
                modEntry.Monitor.LogOnce($"Edited the marriage schedule of {cpd.Spousename}.", LogLevel.Debug);
                if (!modEntry.SchedulesEdited.Contains(cpd.Spousename))
                {
                    modEntry.SchedulesEdited.Add(cpd.Spousename);
                }
            }
        }
        internal static void ContentPackDialogue(ModEntry modEntry, AssetRequestedEventArgs e, Dictionary<string, ContentPackData> CustomSchedule, ContentPackData cpd)
        {
            /*first check which file its calling for:
            * If dialogue, check whether translations exist. (If they don't, just patch file. If they do, check the specific file being requested.)
            */
            if (cpd.Translations.Count is 0 || cpd.Translations is null)
            {
                modEntry.Monitor.LogOnce($"No translations found in {cpd.Spousename} contentpack. Patching all dialogue files with default dialogue", LogLevel.Trace);
                e.Edit(asset => Commands.EditDialogue(cpd, asset, modEntry.Monitor));
                modEntry.Monitor.Log($"Added Ginger Island dialogue to {cpd.Spousename} data.", LogLevel.Debug);
                if (!modEntry.DialoguesEdited.Contains(cpd.Spousename))
                {
                    modEntry.DialoguesEdited.Add(cpd.Spousename);
                }
            }
            else
            {
                if (e.Name.IsEquivalentTo($"Characters/Dialogue/{cpd.Spousename}"))
                {
                    e.Edit(asset => Commands.EditDialogue(cpd, asset, modEntry.Monitor));
                    modEntry.Monitor.Log($"Added Ginger Island dialogue to {cpd.Spousename} data.", LogLevel.Debug);
                    if (!modEntry.DialoguesEdited.Contains(cpd.Spousename))
                    {
                        modEntry.DialoguesEdited.Add(cpd.Spousename);
                    }
                }
                foreach (DialogueTranslation kpv in cpd.Translations)
                {
                    if (e.Name.IsEquivalentTo($"Characters/schedules/{cpd?.Spousename}{Commands.ParseLangCode(kpv?.Key)}") && Commands.IsListValid(kpv) is true)
                    {
                        modEntry.Monitor.LogOnce($"Found '{kpv.Key}' translation for {cpd.Spousename} dialogue!", LogLevel.Trace);
                        e.Edit(asset =>
                        {
                            IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                            data["marriage_islandhouse"] = kpv.Arrival;
                            data["marriage_loc1"] = kpv.Location1;
                            data["marriage_loc2"] = kpv.Location2;
                            data["marriage_loc3"] = kpv.Location3?.ToString();
                        });
                        if (!modEntry.TranslationsAdded.Contains($"{cpd.Spousename} ({kpv.Key})"))
                        {
                            modEntry.TranslationsAdded.Add($"{cpd.Spousename} ({kpv.Key})");
                        }
                    }
                }
            }
        }
        internal static void CharacterSheets(ModEntry modEntry, AssetRequestedEventArgs e, IModHelper Helper, int chance)
        {
            if (e.Name.IsEquivalentTo("Characters/Devan"))
            {
                e.LoadFromModFile<Texture2D>("assets/Devan/Character.png", AssetLoadPriority.Medium);
            };
            if (e.Name.IsEquivalentTo("Characters/Harvey") && modEntry.EnabledSpouses.Contains("Harvey") && chance >= modEntry.RandomizedInt)
            {
                e.Edit(asset =>
                {
                    var editor = asset.AsImage();
                    Texture2D Harvey = Helper.ModContent.Load<Texture2D>("assets/Spouses/Harvey_anim.png");
                    editor.PatchImage(Harvey, new Rectangle(0, 192, 64, 32), new Rectangle(0, 192, 64, 32), PatchMode.Replace);
                });
            }
            if (e.Name.IsEquivalentTo("Characters/Krobus") && modEntry.EnabledSpouses.Contains("Krobus") && chance >= modEntry.RandomizedInt)
            {
                e.LoadFromModFile<Texture2D>("assets/Spouses/Krobus_Outside_Character.png", AssetLoadPriority.Medium);
            }
        }

        internal static void PictureInRoom(IAssetData asset, IModHelper Helper)
        {
            var editor = asset.AsMap();
            Map sourceMap = Helper.ModContent.Load<Map>("assets/Maps/z_Devan_post4h.tbin");
            editor.PatchMap(sourceMap, sourceArea: new Rectangle(0, 0, 2, 2), targetArea: new Rectangle(40, 1, 2, 2));
        }
        internal static void NormalSaloon(IAssetData asset, IModHelper Helper)
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

        }
        internal static void SVESaloon(IAssetData asset, IModHelper Helper)
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
        }

        internal static void Devan(ModEntry modEntry, AssetRequestedEventArgs e)
        {
            if (e.Name.StartsWith("Data/", false, false))
            {
                modEntry.Monitor.LogOnce("Adding Devan", LogLevel.Trace);
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
            if (modEntry.currentLang is "es")
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
            if (modEntry.currentLang is "en")
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