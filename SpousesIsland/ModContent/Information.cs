using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpousesIsland
{
    internal class Information
    {
        internal static bool HasMod(string ModID)
        {
            if (ModEntry.Help.ModRegistry.Get(ModID) is not null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Check if a mod's version is older OR equal.
        /// </summary>
        /// <param name="ModID">The UniqueID to check.</param>
        /// <param name="version">The max version to account for.</param>
        /// <returns>true or false depending on result.</returns>
        internal static bool IsVersionOrLower(string ModID, string version)
        {
            //get both ISemanticVersions
            var modversion = ModEntry.Help.ModRegistry.Get(ModID).Manifest.Version;
            _ = SemanticVersion.TryParse(version, out ISemanticVersion max_version);

            ModEntry.Mon.Log($"C2N version :{modversion.ToString()}, checking if it's newer than {version}..." );

            //Previously used "IsOlderThan() || modversion == max_version", but this is less bug-prone.
            return !(modversion.IsNewerThan(max_version));
        }
        internal static List<string> PlayerSpouses(string id)
        {
            var farmer = Game1.getFarmer(long.Parse(id));
            List<string> spouses = PlayerSpouses(farmer);

            return spouses;
        }

        internal static List<string> PlayerSpouses(Farmer farmer)
        {
            List<string> spouses = new();

            foreach (string name in Game1.NPCGiftTastes.Keys)
            {
                if (name.StartsWith("Universal_"))
                    continue;

                var isMarried = farmer?.friendshipData[name]?.IsMarried() ?? false;
                var isRoommate = farmer?.friendshipData[name]?.IsRoommate() ?? false;
                if (isMarried || isRoommate)
                {
                    spouses.Add(name);
                }
            }
            return spouses;
        }
    }
}
