using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Objects;
using System;
using System.Collections.Generic;

namespace SpousesIsland
{
    internal class SGIValues
    {
        internal static bool CheckSpouseName(string spouse)
        {
            switch (spouse)
            {
                case "Abigail":
                case "Alex":
                case "Elliott":
                case "Emily":
                case "Haley":
                case "Harvey":
                case "Krobus":
                case "Leah":
                case "Maru":
                case "Penny":
                case "Sam":
                case "Sebastian":
                case "Shane":
                case "Claire":
                case "Lance":
                case "Olivia":
                case "Sophia":
                case "Victor":
                case "Wizard":
                    return true;
                default:
                    return false;
            }
        }

        GameLocation ifh = Game1.getLocationFromName("IslandFarmHouse");
        public Point getBedSpot(BedFurniture.BedType bed_type = BedFurniture.BedType.Any)
        {
            return GetBed(bed_type)?.GetBedSpot() ?? new Point(-1000, -1000);
        }
        public Point getSpouseBedSpot(string spouseName)
        {
            Point bed_spot = GetSpouseBed().GetBedSpot();
            bed_spot.X++;
            return bed_spot;
        }
        public virtual BedFurniture GetSpouseBed()
        {
            return GetBed(BedFurniture.BedType.Double);
        }
        public BedFurniture GetBed(BedFurniture.BedType bed_type = BedFurniture.BedType.Any, int index = 0)
        {
            //Furniture f in IslandFarmHouse.Object
            foreach (Furniture f in ifh.furniture)
            {
                if (f is BedFurniture)
                {
                    BedFurniture bed = f as BedFurniture;
                    if (bed_type == BedFurniture.BedType.Any || bed.bedType == bed_type)
                    {
                        if (index == 0)
                        {
                            return bed;
                        }
                        index--;
                    }
                }
            }
            return null;
        }

        internal void spouseSleepEndFunction(Character c, GameLocation location)
        {
            if (c == null || c is not NPC)
            {
                return;
            }
            if (Game1.content.Load<Dictionary<string, string>>("Data\\animationDescriptions").ContainsKey(c.Name.ToLower() + "_sleep"))
            {
                (c as NPC).playSleepingAnimation();
            }
            foreach (Furniture furniture in location.furniture)
            {
                if (furniture is BedFurniture && furniture.getBoundingBox(furniture.TileLocation).Intersects(c.GetBoundingBox()))
                {
                    (furniture as BedFurniture).ReserveForNPC();
                    break;
                }
            }
        }

        public static List<string> SpousesAddedByMod()
        {
            List<string> SpousesAddedByMod = new ();
            SpousesAddedByMod.Add("Abigail");
            SpousesAddedByMod.Add("Alex");
            SpousesAddedByMod.Add("Emily");
            SpousesAddedByMod.Add("Elliott");
            SpousesAddedByMod.Add("Haley");
            SpousesAddedByMod.Add("Harvey");
            SpousesAddedByMod.Add("Krobus");
            SpousesAddedByMod.Add("Leah");
            SpousesAddedByMod.Add("Maru");
            SpousesAddedByMod.Add("Penny");
            SpousesAddedByMod.Add("Sam");
            SpousesAddedByMod.Add("Sebastian");
            SpousesAddedByMod.Add("Shane");
            SpousesAddedByMod.Add("Claire");
            SpousesAddedByMod.Add("Lance");
            SpousesAddedByMod.Add("Olivia");
            SpousesAddedByMod.Add("Sophia");
            SpousesAddedByMod.Add("Victor");
            SpousesAddedByMod.Add("Wizard");
            return SpousesAddedByMod;
        }
        internal static string RandomMap_nPos(Random ran, string spousename, bool ModInstalled, bool ActivatedConfig)
        {
            int choice = ran.Next(1, 2);
            if (choice is 1 || ModInstalled is false || ActivatedConfig is false)
            {
                string result = spousename switch
                {
                    "Abigail" => "IslandWest 62 84 2",
                    "Alex" => "IslandWest 69 77 2 alex_football",
                    "Elliott" => "IslandNorth 19 15 0 \"Strings\\schedules\\Elliott:marriage_loc3\"",
                    "Emily" => "IslandWestCave1 3 6 1 \"Strings\\schedules\\Emily:marriage_loc3\"",
                    "Haley" => "IslandWest 76 12 2 haley_photo \"Strings\\schedules\\Haley:marriage_loc3\"",
                    "Harvey" => "IslandWest 88 14 2 harvey_read \"Strings\\schedules\\Harvey:marriage_loc3\"",
                    "Krobus" => "IslandFarmCave 2 6 2",
                    "Leah" => "IslandWest 89 72 2 leah_draw \"Strings\\schedules\\Leah:marriage_loc3\"",
                    "Maru" => "IslandFieldOffice 7 8 0 \"Strings\\schedules\\Maru:marriage_loc3\"",
                    "Penny" => "IslandFieldOffice 2 7 2 \"Strings\\schedules\\Penny:marriage_loc3\"",
                    "Sam" => "IslandSouthEast 23 14 2 \"Strings\\schedules\\Sam:marriage_loc3\"",
                    "Sebastian" => "IslandNorth 40 23 2 \"Strings\\schedules\\Sebastian:marriage_loc3\"",
                    "Shane" => "IslandSouthEastCave 29 6 2 \"Strings\\schedules\\Shane:marriage_loc3\"",
                    "Claire" => "IslandWest 87 78 2",
                    "Lance" => "IslandSouthEast 21 28 2 \"Characters\\Dialogue\\Lance:marriage_loc3\"",
                    "Magnus" => "Caldera 22 23 0 \"Characters\\Dialogue\\Wizard:marriage_loc3\"",
                    "Olivia" => "IslandNorth 36 73 0 \"Characters\\Dialogue\\Olivia:marriage_loc3\"",
                    "Sophia" => "IslandFarmHouse 18 12 Sophia_Read \"Characters\\Dialogue\\Sophia:marriage_loc3\"",
                    "Victor" => "IslandFarmHouse 19 5 2 Victor_Book2 \"Characters\\Dialogue\\Victor:marriage_loc3\"",
                    _ => "IslandFarmHouse 5 5"
                };
                return result;
            }
            else
            {
                string MapName;
                int RandomM = ran.Next(1, 8);
                switch (RandomM)
                {
                    case 1:
                        MapName = "Custom_GiCave";
                        break;
                    case 2:
                        MapName = "Custom_GiForest";
                        break;
                    case 3:
                        MapName = "Custom_GiRiver";
                        break;
                    case 4:
                        MapName = "Custom_GiClearance";
                        break;
                    case 5:
                        MapName = "Custom_IslandSW";
                        break;
                    case 6:
                        MapName = "Custom_GiHut";
                        break;
                    case 7:
                        MapName = "Custom_GiForestEnd";
                        break;
                    case 8:
                        MapName = "Custom_GiRBeach";
                        break;
                    default:
                        MapName = null;
                        break;
                }
                Point Position = MapName switch
                {
                    "Custom_GiCave" => new Point(ran.Next(9, 22), ran.Next(10, 16)),
                    "Custom_GiForest" => new Point(ran.Next(11, 29), ran.Next(21, 31)),
                    "Custom_GiRiver" => new Point(ran.Next(15, 34), ran.Next(6, 11)),
                    "Custom_GiClearance" => new Point(ran.Next(11, 22), ran.Next(13, 26)),
                    "Custom_IslandSW" => new Point(ran.Next(10, 37), ran.Next(17, 24)),
                    "Custom_GiHut" => new Point(ran.Next(1, 7), ran.Next(6, 8)),
                    "Custom_GiForestEnd" => new Point(ran.Next(9, 25), ran.Next(25, 31)),
                    "Custom_GiRBeach" => new Point(ran.Next(27, 35), ran.Next(6, 23)),
                    _ => new Point(0, 0)
                };
                string result = MapName + Position.ToString();
                return result;
            }
        }
    }
}
