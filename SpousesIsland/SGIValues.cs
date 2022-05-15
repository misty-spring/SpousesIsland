using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Objects;
using System;
using System.Collections.Generic;

namespace SpousesIsland
{
    internal class SGIValues
    {
        //check if custom schedule is for already added spouse
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
                case "Magnus":
                case "Olivia":
                case "Sophia":
                case "Victor":
                    return true;
                default:
                    return false;
            }
        }

        //check if vanilla spouse is in married list
        private List<string> Spouses = new List<string>();
        internal bool IsMarriedToMain(string c)
        {
            if (Game1.MasterPlayer.spouse != null && Game1.MasterPlayer.spouse.Contains(c))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        internal void SpousesList()
        {
            foreach (string c in Game1.NPCGiftTastes.Keys)
            {
                if (IsMarriedToMain(c))
                {
                    Spouses.Add(c);
                }
            }
        }

        internal bool CheckIfMarried(string npc)
        {
            if (Spouses.Contains(npc))
            {
                return true;
            }
            else
                return false;
        }

        //spouse bed
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

        public static bool IsSpanish()
        {
            return StardewValley.LocalizedContentManager.CurrentLanguageCode == LocalizedContentManager.LanguageCode.es;
        }
    }
}