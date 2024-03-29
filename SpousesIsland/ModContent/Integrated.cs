﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using System.Collections.Generic;
using System.Linq;
using xTile;
using xTile.Tiles;
using static SpousesIsland.Values;

namespace SpousesIsland
{
    internal class Integrated
    {
        /* Maps */
        internal static void Maps(AssetRequestedEventArgs e)
        {
            if (e.NameWithoutLocale.IsEquivalentTo("Maps/FishShop"))
            {
                e.Edit(asset =>
                {
                    var editor = asset.AsMap();
                    Map map = editor.Data;
                    map.Properties.Add("NPCWarp", "4 3 IslandSouth 19 43");
                });
            }
            if (e.NameWithoutLocale.IsEquivalentTo("Maps/Custom_IslandShell"))
            {
                e.LoadFromModFile<Map>("assets/Maps/z_SpouseRoomShell.tbin", AssetLoadPriority.Medium);
            }
            if (e.NameWithoutLocale.IsEquivalentTo("Maps/Custom_IslandShell_freelove"))
            {
                e.LoadFromModFile<Map>("assets/Maps/z_SpouseRoomShell_fl.tbin", AssetLoadPriority.Medium);
            }
        }
        internal static void IslandMaps(AssetRequestedEventArgs e, ModConfig Config)
        {
            if (e.Name.StartsWith("Maps/Island", true, false))
            {
                if (e.NameWithoutLocale.IsEquivalentTo("Maps/IslandFarmHouse"))
                {

                    if (Config.Allow_Children == true && ModEntry.Children.Count >= 1 && Config.UseFurnitureBed == false)
                    {
                        e.Edit(asset =>
                        {
                            ModEntry.Mon.Log("Patching child bed onto IslandFarmHouse...", LogLevel.Trace);
                            var editor = asset.AsMap();
                            Map sourceMap = ModEntry.Help.ModContent.Load<Map>($"assets/Maps/kidbeds/z_kidbed_{Config.Childbedcolor}.tbin");
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
                if (e.NameWithoutLocale.IsEquivalentTo("Maps/Island_FieldOffice"))
                {
                    e.Edit(asset =>
                    {
                        var editor = asset.AsMap();
                        Map map = editor.Data;
                        map.Properties.Add("NPCWarp", "4 11 IslandNorth 46 46");
                    });
                }
                if (e.NameWithoutLocale.IsEquivalentTo("Maps/Island_N"))
                {
                    e.Edit(asset =>
                    {
                        var editor = asset.AsMap();
                        Map map = editor.Data;
                        map.Properties.Add("NPCWarp", "22 22 IslandFarmHouse 14 16 35 89 IslandSouth 18 0 46 45 IslandFieldOffice 4 10 40 21 VolcanoEntrance 1 1 40 22 VolcanoEntrance 1 1");
                    });
                }
                if (e.NameWithoutLocale.IsEquivalentTo("Maps/Island_S"))
                {
                    e.Edit(asset =>
                    {
                        var editor = asset.AsMap();
                        Map map = editor.Data;
                        map.Properties.Remove("NPCWarp");
                        map.Properties.Add("NPCWarp", "17 44 FishShop 3 4 36 11 IslandEast 0 45 36 12 IslandEast 0 46 36 13 IslandEast 0 47 -1 11 IslandWest 105 40 -1 10 IslandWest 105 40 -1 12 IslandWest 105 40 -1 13 IslandWest 105 40 17 -1 IslandNorth 35 89 18 -1 IslandNorth 36 89 19 -1 IslandNorth 37 89 27 -1 IslandNorth 43 89 28 -1 IslandNorth 43 89 43 28 IslandSouthEast 0 29 43 29 IslandSouthEast 0 29 43 30 IslandSouthEast 0 29");
                    });
                }
                if (e.NameWithoutLocale.IsEquivalentTo("Maps/Island_SE"))
                {
                    e.Edit(asset =>
                    {
                        var editor = asset.AsMap();
                        Map map = editor.Data;
                        map.Properties.Add("NPCWarp", "0 29 IslandSouth 43 29 29 18 IslandSouthEastCave 1 8");
                    });
                }
                if (e.NameWithoutLocale.IsEquivalentTo("Maps/Island_SouthEastCave"))
                {
                    e.Edit(asset =>
                    {
                        var editor = asset.AsMap();
                        Map map = editor.Data;
                        map.Properties.Add("NPCWarp", "0 7 IslandSouthEast 30 19");
                    });
                }
                if (e.NameWithoutLocale.IsEquivalentTo("Maps/IslandSouthEastCave_pirates"))
                {
                    e.Edit(asset =>
                    {
                        var editor = asset.AsMap();
                        ModEntry.Mon.VerboseLog("Editing IslandSouthEastCave_pirates...");
                        Map map = editor.Data;
                        map.Properties.Add("NPCWarp", "0 7 IslandSouthEast 30 19");
                    });
                }
                if (e.NameWithoutLocale.IsEquivalentTo("Maps/IslandWestCave1"))
                {
                    e.Edit(asset =>
                    {
                        var editor = asset.AsMap();
                        Map map = editor.Data;
                        map.Properties.Add("NPCWarp", "6 12 IslandWest 61 5");
                    });
                }
                if (e.NameWithoutLocale.IsEquivalentTo("Maps/IslandEast"))
                {
                    e.Edit(asset =>
                    {
                        var editor = asset.AsMap();
                        Map map = editor.Data;
                        map.Properties.Add("NPCWarp", "-1 45 IslandSouth 35 11 -1 46 IslandSouth 35 12 -1 47 IslandSouth 35 13 -1 48 IslandSouth 35 13 22 9 IslandHut 7 13 34 30 IslandShrine 13 28");
                    });
                }
                if (e.NameWithoutLocale.IsEquivalentTo("Maps/IslandShrine"))
                {
                    e.Edit(asset =>
                    {
                        var editor = asset.AsMap();
                        Map map = editor.Data;
                        map.Properties.Add("NPCWarp", "12 27 IslandEast 33 30 12 28 IslandEast 33 30 12 29 IslandEast 33 30");
                    });
                }
                if (e.NameWithoutLocale.IsEquivalentTo("Maps/IslandFarmCave"))
                {
                    e.Edit(asset =>
                    {
                        var editor = asset.AsMap();
                        Map map = editor.Data;
                        map.Properties.Add("NPCWarp", "4 10 IslandSouth 97 35");
                    });
                }
                if (e.NameWithoutLocale.IsEquivalentTo("Maps/Island_W"))
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

        /* Checks if NPC is in "MarriedAndAllowed" before patching their info. */
        internal static void Schedules(AssetRequestedEventArgs e)
        {
            //get target without path. if the name isn't in spouse list, return.
            var targetspan =
        System.IO.Path.GetFileName(e.NameWithoutLocale.Name.ToCharArray());
            var targetwithoutpath = targetspan.ToString();
            if (!ModEntry.MarriedAndAllowed.Contains(targetwithoutpath))
            {
                return;
            }

            //list of spouses invited
            var invited = ModEntry.Status[ModEntry.Player_MP_ID].Who;
            //if from a ticket and spouse isnt in list
            if (ModEntry.IsFromTicket && !(invited.Contains(targetwithoutpath)))
                return;

            //integrated data
            if (e.NameWithoutLocale.IsEquivalentTo("Characters/schedules/Abigail"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 16 9 0 \"Strings\\schedules\\Abigail:marriage_islandhouse\"/1100 IslandNorth 44 28 0 \"Strings\\schedules\\Abigail:marriage_loc1\"/{RandomMap_nPos(targetwithoutpath)}/2000 IslandWest 39 41 0 \"Strings\\schedules\\Abigail:marriage_loc3\"/a2200 IslandFarmHouse 16 9 0";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.NameWithoutLocale.IsEquivalentTo("Characters/schedules/Alex"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 19 6 0 \"Strings\\schedules\\Alex:marriage_islandhouse\"/1100 IslandWest 85 39 2 alex_lift_weights \"Strings\\schedules\\Alex:marriage_loc1\"/{RandomMap_nPos(targetwithoutpath)}/a1900 IslandSouth 12 27 2 \"Strings\\schedules\\Alex:marriage_loc3\"/a2200 IslandFarmHouse 19 6 0";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.NameWithoutLocale.IsEquivalentTo("Characters/schedules/Elliott"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 9 9 2 \"Strings\\schedules\\Elliott:marriage_islandhouse\"/1100 IslandWest 102 77 2 elliott_read/1400 IslandWest 73 83 2 \"Strings\\schedules\\Elliott:marriage_loc2\"/{RandomMap_nPos(targetwithoutpath)}/a2200 IslandFarmHouse 9 9 2";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.NameWithoutLocale.IsEquivalentTo("Characters/schedules/Emily"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 12 10 \"Strings\\schedules\\Emily:marriage_islandhouse\"/1100 IslandWest 53 52 2 \"Strings\\schedules\\Emily:marriage_loc1\"/1400 IslandWest 89 79 2 emily_exercise/{RandomMap_nPos(targetwithoutpath)}/a2200 IslandFarmHouse 12 10 0";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.NameWithoutLocale.IsEquivalentTo("Characters/schedules/Haley"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 8 6 2 \"Strings\\schedules\\Haley:marriage_islandhouse\"/1100 IslandNorth 32 74 0 \"Strings\\schedules\\Haley:marriage_loc1\"/{RandomMap_nPos(targetwithoutpath)}/1900 IslandWest 80 45 2/a2200 IslandFarmHouse 8 6 0";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.NameWithoutLocale.IsEquivalentTo("Characters/schedules/Harvey"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 16 13 0 \"Strings\\schedules\\Harvey:marriage_islandhouse\"/1100 IslandFarmHouse 3 5 0 \"Strings\\schedules\\Harvey:marriage_loc1\"/1400 IslandWest 89 75 2 harvey_excercise/{RandomMap_nPos(targetwithoutpath)}/a2100 IslandFarmHouse 16 13 0";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });

            if (e.NameWithoutLocale.IsEquivalentTo("Characters/schedules/Krobus"))
            {
                //first make file, then edit
                var schedule = new Dictionary<string, string>();

                e.LoadFrom(
                    () => schedule,
                    AssetLoadPriority.Low);

                schedule.Add("marriage_Mon", $"620 IslandFarmHouse 15 10 0 \"Characters\\Dialogue\\Krobus:marriage_islandhouse\"/1100 IslandFarmHouse 10 10 3 \"Characters\\Dialogue\\Krobus:marriage_loc1\"/1130 IslandFarmHouse 9 8 0/1200 IslandFarmHouse 9 11 2/1400 IslandWestCave1 8 8 0 \"Characters\\Dialogue\\Krobus:marriage_loc3\"/1500 IslandWestCave1 9 8 0/1600 IslandWestCave1 9 6 3/{RandomMap_nPos(targetwithoutpath)}/a2200 IslandFarmHouse 15 10 0");
                schedule.Add("marriage_Tue", "GOTO marriage_Mon");
                schedule.Add("marriage_Wed", "GOTO marriage_Mon");
                schedule.Add("marriage_Thu", "GOTO marriage_Mon");
                schedule.Add("marriage_Fri", "GOTO marriage_Mon");
                schedule.Add("marriage_Sat", "GOTO marriage_Mon");
                schedule.Add("marriage_Sun", "GOTO marriage_Mon");

                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    foreach (var pair in schedule)
                    {
                        data[pair.Key] = pair.Value;
                    }
                });
            }
            if (e.NameWithoutLocale.IsEquivalentTo("Characters/schedules/Leah"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 21 13 1 \"Strings\\schedules\\Leah:marriage_islandhouse\"/1100 IslandNorth 50 25 0 leah_draw \"Strings\\schedules\\Leah:marriage_loc1\"/1400 IslandNorth 21 16 0/{RandomMap_nPos(targetwithoutpath)}/a2200 IslandFarmHouse 21 13 1";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.NameWithoutLocale.IsEquivalentTo("Characters/schedules/Maru"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 18 15 2 \"Strings\\schedules\\Maru:marriage_islandhouse\"/1100 IslandWest 95 45 2/1400 IslandNorth 50 25 0 \"Strings\\schedules\\Maru:marriage_loc1\"/{RandomMap_nPos(targetwithoutpath)}/a2200 IslandFarmHouse 18 15 2";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                }
            );
            if (e.NameWithoutLocale.IsEquivalentTo("Characters/schedules/Penny"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 9 12 1 \"Strings\\schedules\\Penny:marriage_islandhouse\"/1100 IslandFarmHouse 3 6 0/1400 IslandWest 83 37 3 penny_read \"Strings\\schedules\\Penny:marriage_loc1\"/{RandomMap_nPos(targetwithoutpath)}/a2200 IslandFarmHouse 9 12 1";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.NameWithoutLocale.IsEquivalentTo("Characters/schedules/Sam"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 22 6 2 \"Strings\\schedules\\Sam:marriage_islandhouse\"/1100 IslandFarmHouse 8 9 0 sam_guitar/1400 IslandNorth 36 27 0 sam_skateboarding \"Strings\\schedules\\Sam:marriage_loc1\"/{RandomMap_nPos(targetwithoutpath)}/a2200 IslandFarmHouse 22 6 2";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.NameWithoutLocale.IsEquivalentTo("Characters/schedules/Sebastian"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 25 14 3 \"Strings\\schedules\\Sebastian:marriage_islandhouse\"/1100 IslandWest 88 14 0/1400 IslandWestCave1 6 4 0 \"Strings\\schedules\\Sebastian:marriage_loc1\"/{RandomMap_nPos(targetwithoutpath)}/a2200 IslandFarmHouse 25 14 3";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.NameWithoutLocale.IsEquivalentTo("Characters/schedules/Shane"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 20 5 0 \"Strings\\schedules\\Shane:marriage_islandhouse\"/1100 IslandWest 87 52 0 shane_charlie \"Strings\\schedules\\Shane:marriage_loc1\"/a1420 IslandWest 77 39 0/1430 IslandFarmHouse 15 9 0 shane_drink/{RandomMap_nPos(targetwithoutpath)}/a2200 IslandFarmHouse 20 5 0";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            //sve
            if (e.NameWithoutLocale.IsEquivalentTo("Characters/schedules/Claire"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 5 6 0 \"Characters\\Dialogue\\Claire:marriage_islandhouse\"/1100 IslandFarmHouse 17 12 2 Claire_Read \"Characters\\Dialogue\\Claire:marriage_loc1\"/1400 IslandEast 19 40 0 \"Characters\\Dialogue\\Claire:marriage_loc3\"/{RandomMap_nPos(targetwithoutpath)}/a2200 IslandFarmHouse 5 6 0";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.NameWithoutLocale.IsEquivalentTo("Characters/schedules/Lance"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 13 13 1 \"Characters\\Dialogue\\Lance:marriage_islandhouse\"/1100 IslandNorth 37 30 0/1400 Caldera 24 23 2 \"Characters\\Dialogue\\Lance:marriage_loc2\"/{RandomMap_nPos(targetwithoutpath)}/a2200 IslandFarmHouse 13 13 1";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.NameWithoutLocale.IsEquivalentTo("Characters/schedules/Wizard"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 26 6 0 \"Characters\\Dialogue\\Wizard:marriage_islandhouse\"/1100 IslandWest 38 38 0 \"Characters\\Dialogue\\Wizard:marriage_loc1\"/1400 IslandSouthEast 28 26 2/{RandomMap_nPos("Magnus")}/a2200 IslandFarmHouse 26 6 0";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.NameWithoutLocale.IsEquivalentTo("Characters/schedules/Olivia"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 7 11 1\"Characters\\Dialogue\\Olivia:marriage_islandhouse\"/1100 IslandFarmHouse 14 9 0 Olivia_Wine1 \"Characters\\Dialogue\\Olivia:marriage_loc1\"/1400 IslandSouth 31 24 2 Olivia_Yoga/{RandomMap_nPos(targetwithoutpath)}/a2200 IslandFarmHouse 7 11 1";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.NameWithoutLocale.IsEquivalentTo("Characters/schedules/Sophia"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 3 5 0 \"Characters\\Dialogue\\Sophia:marriage_islandhouse\"/1100 IslandWest 82 48 2/1400 IslandNorth 17 36 3 \"Characters\\Dialogue\\Sophia:marriage_loc2_scenery\"/{RandomMap_nPos(targetwithoutpath)}/a2200 IslandFarmHouse 3 5 2";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
            if (e.NameWithoutLocale.IsEquivalentTo("Characters/schedules/Victor"))
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_Mon"] = $"620 IslandFarmHouse 14 9 2 \"Characters\\Dialogue\\Victor:marriage_islandhouse\"/1100 IslandSouth 11 23 2 Victor_Wine2/1400 IslandFieldOffice 5 4 0 \"Characters\\Dialogue\\Victor:marriage_loc2\"/{RandomMap_nPos(targetwithoutpath)}/a2200 IslandFarmHouse 14 9 2";
                    data["marriage_Tue"] = "GOTO marriage_Mon";
                    data["marriage_Wed"] = "GOTO marriage_Mon";
                    data["marriage_Thu"] = "GOTO marriage_Mon";
                    data["marriage_Fri"] = "GOTO marriage_Mon";
                    data["marriage_Sat"] = "GOTO marriage_Mon";
                    data["marriage_Sun"] = "GOTO marriage_Mon";
                });
        }

        internal static void Dialogues(AssetRequestedEventArgs e)
        {
            //get target without path. if the name isn't in spouse list, return.
            var targetspan =
        System.IO.Path.GetFileName(e.NameWithoutLocale.Name.ToCharArray());
            var targetwithoutpath = targetspan.ToString();

            if(ModEntry.IsDebug)
            {
                ModEntry.Mon.Log($"targetwithoutpath = {targetwithoutpath};", LogLevel.Trace);
            }

            if (!ModEntry.MarriedAndAllowed.Contains(targetwithoutpath))
            {
                return;
            }
            
            ModEntry.Mon.Log($"Found {targetwithoutpath} in dictionary. Proceeding to edit.", LogLevel.Debug);
            var tl = ModEntry.TL;

            //ALL translation keys are placeholders
            if (e.NameWithoutLocale.IsEquivalentTo("Characters/Dialogue/Abigail"))
            {
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = tl.Get("Dialogue.Abigail.arrival");
                    data["marriage_loc1"] = tl.Get("Dialogue.Abigail.LocA");
                    data["marriage_loc3"] = tl.Get("Dialogue.Abigail.LocB");
                });
            }

            if (e.NameWithoutLocale.IsEquivalentTo("Characters/Dialogue/Alex"))
            {
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = tl.Get("Dialogue.Alex.arrival");
                    data["marriage_loc1"] = tl.Get("Dialogue.Alex.LocA");
                    data["marriage_loc3"] = tl.Get("Dialogue.Alex.LocB");
                });
            }

            if (e.NameWithoutLocale.IsEquivalentTo("Characters/Dialogue/Elliott"))
            {
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = tl.Get("Dialogue.Elliott.arrival");
                    data["marriage_loc1"] = tl.Get("Dialogue.Elliott.LocA");
                    data["marriage_loc3"] = tl.Get("Dialogue.Elliott.LocB");
                });
            }

            if (e.NameWithoutLocale.IsEquivalentTo("Characters/Dialogue/Emily"))
            {
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = tl.Get("Dialogue.Emily.arrival");
                    data["marriage_loc1"] = tl.Get("Dialogue.Emily.LocA");
                    data["marriage_loc3"] = tl.Get("Dialogue.Emily.LocB");
                });
            }

            if (e.NameWithoutLocale.IsEquivalentTo("Characters/Dialogue/Haley"))
            {
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = tl.Get("Dialogue.Haley.arrival");
                    data["marriage_loc1"] = tl.Get("Dialogue.Haley.LocA");
                    data["marriage_loc3"] = tl.Get("Dialogue.Haley.LocB");
                });
            }

            if (e.NameWithoutLocale.IsEquivalentTo("Characters/Dialogue/Harvey"))
            {
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = tl.Get("Dialogue.Harvey.arrival");
                    data["marriage_loc1"] = tl.Get("Dialogue.Harvey.LocA");
                    data["marriage_loc3"] = tl.Get("Dialogue.Harvey.LocB");
                });
            }

            if (e.NameWithoutLocale.IsEquivalentTo("Characters/Dialogue/Krobus"))
            {
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = tl.Get("Dialogue.Krobus.arrival");
                    data["marriage_loc1"] = tl.Get("Dialogue.Krobus.LocA");
                    data["marriage_loc3"] = tl.Get("Dialogue.Krobus.LocB");
                });
            }

            if (e.NameWithoutLocale.IsEquivalentTo("Characters/Dialogue/Leah"))
            {
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = tl.Get("Dialogue.Leah.arrival");
                    data["marriage_loc1"] = tl.Get("Dialogue.Leah.LocA");
                    data["marriage_loc3"] = tl.Get("Dialogue.Leah.LocB");
                });
            }

            if (e.NameWithoutLocale.IsEquivalentTo("Characters/Dialogue/Maru"))
            {
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = tl.Get("Dialogue.Maru.arrival");
                    data["marriage_loc1"] = tl.Get("Dialogue.Maru.LocA");
                    data["marriage_loc3"] = tl.Get("Dialogue.Maru.LocB");
                });
            }

            if (e.NameWithoutLocale.IsEquivalentTo("Characters/Dialogue/Penny"))
            {
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = tl.Get("Dialogue.Penny.arrival");
                    data["marriage_loc1"] = tl.Get("Dialogue.Penny.LocA");
                    data["marriage_loc3"] = tl.Get("Dialogue.Penny.LocB");
                });
            }

            if (e.NameWithoutLocale.IsEquivalentTo("Characters/Dialogue/Sam"))
            {
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = tl.Get("Dialogue.Sam.arrival");
                    data["marriage_loc1"] = tl.Get("Dialogue.Sam.LocA");
                    data["marriage_loc3"] = tl.Get("Dialogue.Sam.LocB");
                });
            }

            if (e.NameWithoutLocale.IsEquivalentTo("Characters/Dialogue/Sebastian"))
            {
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = tl.Get("Dialogue.Sebastian.arrival");
                    data["marriage_loc1"] = tl.Get("Dialogue.Sebastian.LocA");
                    data["marriage_loc3"] = tl.Get("Dialogue.Sebastian.LocB");
                });
            }

            if (e.NameWithoutLocale.IsEquivalentTo("Characters/Dialogue/Shane"))
            {
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = tl.Get("Dialogue.Shane.arrival");
                    data["marriage_loc1"] = tl.Get("Dialogue.Shane.LocA");
                    data["marriage_loc3"] = tl.Get("Dialogue.Shane.LocB");
                });
            }

            if (e.NameWithoutLocale.IsEquivalentTo("Characters/Dialogue/Claire"))
            {
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = tl.Get("Dialogue.Claire.arrival");
                    data["marriage_loc1"] = tl.Get("Dialogue.Claire.LocA");
                    data["marriage_loc3"] = tl.Get("Dialogue.Claire.LocB");
                });
            }

            if (e.NameWithoutLocale.IsEquivalentTo("Characters/Dialogue/Lance"))
            {
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = tl.Get("Dialogue.Lance.arrival");
                    data["marriage_loc1"] = tl.Get("Dialogue.Lance.LocA");
                    data["marriage_loc3"] = tl.Get("Dialogue.Lance.LocB");
                });
            }

            if (e.NameWithoutLocale.IsEquivalentTo("Characters/Dialogue/Wizard"))
            {
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = tl.Get("Dialogue.Wizard.arrival");
                    data["marriage_loc1"] = tl.Get("Dialogue.Wizard.LocA");
                    data["marriage_loc3"] = tl.Get("Dialogue.Wizard.LocB");
                });
            }

            if (e.NameWithoutLocale.IsEquivalentTo("Characters/Dialogue/Olivia"))
            {
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = tl.Get("Dialogue.Olivia.arrival");
                    data["marriage_loc1"] = tl.Get("Dialogue.Olivia.LocA");
                    data["marriage_loc3"] = tl.Get("Dialogue.Olivia.LocB");
                });
            }

            if (e.NameWithoutLocale.IsEquivalentTo("Characters/Dialogue/Sophia"))
            {
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = tl.Get("Dialogue.Sophia.arrival");
                    data["marriage_loc1"] = tl.Get("Dialogue.Sophia.LocA");
                    data["marriage_loc3"] = tl.Get("Dialogue.Sophia.LocB");
                });
            }

            if (e.NameWithoutLocale.IsEquivalentTo("Characters/Dialogue/Victor"))
            {
                e.Edit(asset =>
                {
                    IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                    data["marriage_islandhouse"] = tl.Get("Dialogue.Victor.arrival");
                    data["marriage_loc1"] = tl.Get("Dialogue.Victor.LocA");
                    data["marriage_loc3"] = tl.Get("Dialogue.Victor.LocB");
                });
            }
        }

        internal static Point GetPosition(string spouse)
        {
            Point result = spouse switch
            {
                "Abigail" => new Point(16, 9),
                "Alex" => new Point(19, 6),
                "Elliott" => new Point(9, 9),
                "Emily" => new Point(12, 10),
                "Haley" => new Point(8, 6),
                "Harvey" => new Point(16, 13),
                "Krobus" => new Point(15, 10),
                "Leah" => new Point(21, 13),
                "Maru" => new Point(18, 15),
                "Penny" => new Point(9, 12),
                "Sam" => new Point(22, 6),
                "Sebastian" => new Point(25, 14),
                "Shane" => new Point(20, 5),
                "Claire" => new Point(5, 6),
                "Lance" => new Point(13, 13),
                "Magnus" => new Point(26, 6),
                "Olivia" => new Point(7, 11),
                "Sophia" => new Point(3, 5),
                "Victor" => new Point(14, 9),
                _ => new Point(0, 0) //GetCustomPosition(spouse)
            };

            return result;
        }

        internal static int GetFacing(string spouse)
        {
            var result = spouse switch
            {

                "Abigail" => 0,
                "Alex" => 0,
                "Elliott" => 2,
                "Emily" => 2,
                "Haley" => 0,
                "Harvey" => 0,
                "Krobus" => 0,
                "Leah" => 1,
                "Maru" => 2,
                "Penny" => 1,
                "Sam" => 2,
                "Sebastian" => 3,
                "Shane" => 0,
                "Claire" => 0,
                "Lance" => 1,
                "Magnus" => 0,
                "Olivia" => 1,
                "Sophia" => 2,
                "Victor" => 2,
                _ => 0 //GetCustomFacing(spouse)
            };

            return result;
        }
        /* These are here for brevity */
        internal static Texture2D KbcSamples() => ModEntry.Help.ModContent.Load<Texture2D>("assets/kbcSamples.png");
        internal static string SpouseD()
        {
            var SpousesDesc = ModEntry.Help.Translation.Get("config.Vanillas.description");
            return SpousesDesc;
        }
    }
}