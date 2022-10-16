using Microsoft.Xna.Framework;
using SpousesIsland.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System;
using System.Collections.Generic;

namespace SpousesIsland
{
    internal class ContentPacks
    {
        public static void EditDialogue(IslandVisitData cpd, IAssetData asset)
        {
            IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
            data["marriage_islandhouse"] = cpd.ArrivalDialogue;
            data["marriage_loc1"] = cpd.Location1.Dialogue;
            data["marriage_loc2"] = cpd.Location2.Dialogue;
            if (cpd.Location3?.Dialogue is not null)
            {
                data["marriage_loc3"] = cpd.Location3.Dialogue;
            };
        }

        internal static void Schedule(AssetRequestedEventArgs e, IslandVisitData cpd)
        {
            if (e.NameWithoutLocale.IsEquivalentTo($"Characters/schedules/{cpd.Spousename}"))
            {
                string temp_loc3 = Information.IsLoc3Valid(cpd);
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
                ModEntry.Mon.LogOnce($"Edited the marriage schedule of {cpd.Spousename}.", LogLevel.Debug);
                if (!ModEntry.SchedulesEdited.Contains(cpd.Spousename))
                {
                    ModEntry.SchedulesEdited.Add(cpd.Spousename);
                }
            }
        }

        internal static void Dialogue(AssetRequestedEventArgs e, IslandVisitData cpd)
        {
            /*first check which file its calling for:
            * If dialogue, check whether translations exist. (If they don't, just patch file. If they do, check the specific file being requested.)
            */
            if (cpd.Translations.Count is 0 || cpd.Translations is null)
            {
                ModEntry.Mon.LogOnce($"No translations found in {cpd.Spousename} contentpack. Patching all dialogue files with default dialogue", LogLevel.Trace);
                e.Edit(asset => EditDialogue(cpd, asset));
                ModEntry.Mon.Log($"Added Ginger Island dialogue to {cpd.Spousename} data.", LogLevel.Debug);
                if (!ModEntry.DialoguesEdited.Contains(cpd.Spousename))
                {
                    ModEntry.DialoguesEdited.Add(cpd.Spousename);
                }
            }
            else
            {
                if (e.NameWithoutLocale.IsEquivalentTo($"Characters/Dialogue/{cpd.Spousename}"))
                {
                    e.Edit(asset => EditDialogue(cpd, asset));
                    ModEntry.Mon.Log($"Added Ginger Island dialogue to {cpd.Spousename} data.", LogLevel.Debug);
                    if (!ModEntry.DialoguesEdited.Contains(cpd.Spousename))
                    {
                        ModEntry.DialoguesEdited.Add(cpd.Spousename);
                    }
                }
                foreach (DialogueTranslation kpv in cpd.Translations)
                {
                    if (e.NameWithoutLocale.IsEquivalentTo($"Characters/schedules/{cpd?.Spousename}{Information.ParseLangCode(kpv?.Key)}") && Information.IsListValid(kpv) is true)
                    {
                        ModEntry.Mon.LogOnce($"Found '{kpv.Key}' translation for {cpd.Spousename} dialogue!", LogLevel.Trace);
                        e.Edit(asset =>
                        {
                            IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                            data["marriage_islandhouse"] = kpv.Arrival;
                            data["marriage_loc1"] = kpv.Location1;
                            data["marriage_loc2"] = kpv.Location2;
                            data["marriage_loc3"] = kpv.Location3?.ToString();
                        });
                        if (!ModEntry.TranslationsAdded.Contains($"{cpd.Spousename} ({kpv.Key})"))
                        {
                            ModEntry.TranslationsAdded.Add($"{cpd.Spousename} ({kpv.Key})");
                        }
                    }
                }
            }
        }

        internal static Point GetArrivalPosition(string spouse)
        {
            var CP = ModEntry.CPSchedule;
            var pack = ModEntry.CustomSchedule;

            if(CP.ContainsKey(spouse))
            {
                var pos = CP[spouse].ArrivalPosition.Split(' ');
                return new Point(int.Parse(pos[0]), int.Parse(pos[1]));
            }

            if (pack.ContainsKey(spouse))
            {
                var pos = pack[spouse].ArrivalPosition.Split(' ');
                return new Point(int.Parse(pos[0]), int.Parse(pos[1]));
            }

            ModEntry.Mon.Log($"No position found for NPC {spouse}. Using default", LogLevel.Error);
            return new Point(15, 15);
        }

        internal static int GetDirection(string spouse)
        {

            var CP = ModEntry.CPSchedule;
            var pack = ModEntry.CustomSchedule;

            if (CP.ContainsKey(spouse))
            {
                var pos = CP[spouse].ArrivalPosition.Split(' ');
                return int.Parse(pos[2]);
            }

            if (pack.ContainsKey(spouse))
            {
                var pos = pack[spouse].ArrivalPosition.Split(' ');
                return int.Parse(pos[2]);
            }

            ModEntry.Mon.Log($"No direction found for NPC {spouse}. Using default", LogLevel.Error);
            return 0;
        }

        internal static void EditCustoms(AssetRequestedEventArgs e, Dictionary<string, DataViaCP> from)
        {
            if(from is null)
            {
                return;
            }

            foreach(var edit in from)
            {
                if(e.NameWithoutLocale.Equals($"Characters/schedules/{edit.Key}"))
                {
                    e.Edit(asset =>
                    {
                        var cpd = edit.Value;

                        IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                        data["marriage_Mon"] = $"620 IslandFarmHouse {cpd.ArrivalPosition} \"Characters\\Dialogue\\{cpd.Spousename}:marriage_islandhouse\"/{cpd.Location1.Time} {cpd.Location1.Name} {cpd.Location1.Position} \"Characters\\Dialogue\\{cpd.Spousename}:marriage_loc1\"/{cpd.Location2.Time} {cpd.Location2.Name} {cpd.Location2.Position} \"Characters\\Dialogue\\{cpd.Spousename}:marriage_loc2\"/{temp_loc3}a2200 IslandWest 77 41";
                        data["marriage_Tue"] = "GOTO marriage_Mon";
                        data["marriage_Wed"] = "GOTO marriage_Mon";
                        data["marriage_Thu"] = "GOTO marriage_Mon";
                        data["marriage_Fri"] = "GOTO marriage_Mon";
                        data["marriage_Sat"] = "GOTO marriage_Mon";
                        data["marriage_Sun"] = "GOTO marriage_Mon";
                    }
                    );
                }
                if (e.NameWithoutLocale.IsEquivalentTo($"Characters/Dialogue/{edit.Key}"))
                {
                    var cpd = edit.Value;
                    e.Edit(asset =>
                   {
                       IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;

                       data["marriage_islandhouse"] = cpd.ArrivalDialogue;

                       int index = 0;

                       foreach (var idk in )
                       {
                           index++;
                           data[$"marriage_islandvisit_{index}"] = cpd.ArrivalDialogue;
                       }
                   }
                    );
                }
            }
        }
    }
}