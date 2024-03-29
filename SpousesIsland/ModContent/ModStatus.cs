﻿using StardewValley;
using System;
using System.Collections.Generic;

namespace SpousesIsland
{
    internal class ModStatus
    {
        public string Name { get; set; }
        public bool DayVisit { get; set; } = false;
        public (bool, int) WeekVisit { get; set; } = (false, 0);
        public List<string> Who { get; set; } = new();

        public ModStatus()
        {

        }
        public ModStatus(ModStatus m)
        {
            Name = m.Name;
            DayVisit = m.DayVisit;
            WeekVisit = m.WeekVisit;
            Who = m.Who;
        }
        public ModStatus(Farmer player, bool includeSpouses)
        {
            Name = player.Name;
            DayVisit = player?.mailReceived?.Contains("VisitTicket_day") ?? false;
            WeekVisit = (player?.mailReceived?.Contains("VisitTicket_week") ?? false, 0);
            Who = includeSpouses ? Information.PlayerSpouses(player) : new List<string>();
        }
    }

    internal class Patches
    {
        /// <summary>
        /// Handles game's actions when receiving island ticket.
        /// </summary>
        /// <param name="__instance"> NPC receiving item.</param>
        /// <param name="who">player</param>
        internal static bool tryToReceiveTicket(ref NPC __instance, Farmer who)
        {
            //ModEntry.jsonAssets?.GetObjectId("Island ticket (day)") <- old one didnt work. we check name instead
            bool isDay = who?.ActiveObject?.Name == "Island ticket (day)";
            bool isWeek = who?.ActiveObject?.Name == "Island ticket (week)";

            if (who?.ActiveObject == null || (!isDay && !isWeek))
            {
                return true;
            }

            //if a visit is already going on
            if(ModEntry.IslandToday)
            {
                //tell player
		var alreadyongoing = ModEntry.TL.Get("AlreadyOngoing.Visit");
                Game1.drawDialogueBox(Game1.parseText(alreadyongoing));
                return false;
            }
            //if festival tomorrow
            else if(Utility.isFestivalDay(Game1.dayOfMonth + 1, Game1.currentSeason))
            {
                //tell player theres festival tmrw
                var festivalnotice = Game1.parseText(ModEntry.TL.Get("FestivalTomorrow"));
                Game1.drawDialogueBox(festivalnotice);
                return false;
            }
            //if not, call method that handles NPC's reaction (+etc).
            else
            {
                TicketActions(__instance, who, isDay, isWeek);
                return false;
            }

        }

        /// <summary>
        /// Handles NPC's reaction to ticket.
        /// </summary>
        /// <param name="__instance"> NPC receiving item.</param>
        /// <param name="who">player</param>
        /// <param name="isDay">If it's a day invite.</param>
        /// <param name="isWeek">If it's a week invite.</param>
        /// <exception cref="ArgumentException">If there's any error with the item, this is sent (shouldn't happen but still added as preemptive measure).</exception>
        internal static void TicketActions(NPC __instance, Farmer who, bool isDay, bool isWeek)
        {
            who.Halt();
            who.faceGeneralDirection(__instance.getStandingPosition(), 0, opposite: false, useTileCalculations: false);

            var npcdata = who.friendshipData[__instance.Name]; //to simplify text below and make more understandable

            if (npcdata.IsMarried() || npcdata.IsRoommate())
            {
                //get invited list
                var inviteds = ModEntry.Status[who.UniqueMultiplayerID.ToString()].Who;
                bool hasInvites = inviteds.Count is not 0;

                //if: already scheduled for a week
                var scheduledWeek = isDay && who.mailbox.Contains("VisitTicket_week");
                //if: already scheduled for a day
                var scheduledDay = isWeek && who.mailbox.Contains("VisitTicket_day");


                //if already invited
                if (hasInvites && inviteds.Contains(__instance.Name))
                {
                    //tell player about it
		    var alreadyinvited = String.Format(ModEntry.TL.Get("AlreadyInvited"), __instance.displayName);
                    //Game1.showRedMessage(Game1.parseText(alreadyinvited));
                    var message = Game1.parseText(alreadyinvited);
                    Game1.drawObjectDialogue(message);
                    Game1.playSound("cancel");
                }
                //if different than current invitation
                else if(scheduledDay || scheduledWeek)
                {
                    //log just in case.
                    ModEntry.Mon.Log($"Player {who.displayName} has already scheduled a visit for {(scheduledDay ? "tomorow" : "next week")}. Can't use a different ticket (current one : {who.ActiveObject.Name})");

                    //inform day/week visit has already been scheduled.
		    var scheduleType = scheduledDay ? ModEntry.TL.Get("AlreadyScheduled.Day") : ModEntry.TL.Get("AlreadyScheduled.Week");
                    Game1.drawDialogueBox(Game1.parseText(scheduleType));
                }
                //if none above apply, continue to inviting
                else
                {
                    //add a reasonable amount of friendship - temporarily removed
                    //who.changeFriendship(100,__instance);
                    
                    Game1.drawDialogue(__instance, GetInviteDialogue(__instance));
                    var MP_ID = who.UniqueMultiplayerID.ToString();

                    //user will always have data in Status (created during SaveLoadedBasicInfo).
                    //so there's no worry about possible nulls
                    if (isDay)
                    {
                        ModEntry.Status[MP_ID].DayVisit = true;
                        who.mailbox.Add("VisitTicket_day");
                    }
                    else if (isWeek)
                    {
                        ModEntry.Status[MP_ID].WeekVisit = (true, 0);
                        who.mailbox.Add("VisitTicket_week");
                    }
                    else
                    {
                        throw new ArgumentException("Ticket is neither week nor day one. An error happened somewhere in the method.");
                    }

                    inviteds.Add(__instance.Name); //add name of spouse to allowed list

                    who.reduceActiveItemByOne();
                }
            }
            else
            {
                //send rejection dialogue like the pendant one
                Game1.drawObjectDialogue(Game1.parseText(Game1.content.LoadString("Strings\\Characters:MovieInvite_NoTheater", __instance.displayName)));
            }
        }
        /// <summary>
        /// Get the dialogue for a NPC, depending on name and personality.
        /// </summary>
        /// <param name="who"></param>
        /// <returns>The NPC's reply to being invited (to the island).</returns>
        private static string GetInviteDialogue(NPC who)
        {
            bool vanilla = who.Name switch {

                "Abigail" => true, 
                "Alex" => true,
                "Elliott" => true,
                "Emily" => true,
                "Haley" => true,
                "Harvey" => true,
                "Krobus" => true,
                "Leah" => true,
                "Maru" => true,
                "Penny" => true,
                "Sam" => true,
                "Sebastian" => true,
                "Shane" => true,
                "Claire" => true,
                "Lance" => true,
                "Olivia" => true,
                "Sophia" => true,
                "Victor" => true,
                "Wizard" => true,
                _ => false, 
            };

            if(vanilla)
            {
                return ModEntry.TL.Get($"Invite_{who.Name}");
            }
            else
            {
                int r = Game1.random.Next(1, 4);
                return ModEntry.TL.Get($"Invite_generic_{who.Optimism}_{r}"); //1 polite, 2 rude, 0 normal?
            }
        }

        
    }
}
