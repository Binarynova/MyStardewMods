using System;
using StardewModdingAPI;
using StardewValley;
using System.Collections.Generic;
using StardewModdingAPI.Events;
using StardewValley.Inventories;
using StardewValley.Menus;
using Microsoft.Xna.Framework;

namespace LevelUpNotifications
{
    public class ModEntry : Mod
    {
        private int numOfLevelUpsToday = 0;

        public override void Entry(IModHelper helper)
        {
            helper.Events.Player.LevelChanged += Player_LevelChanged;
            helper.Events.GameLoop.DayStarted += ResetLevelUpCounter;
        }
        
        private void Player_LevelChanged(object? sender, LevelChangedEventArgs e)
        {
            if(e.NewLevel > e.OldLevel)
            {
                if(numOfLevelUpsToday > 0)
                {
                    string levelUpMessage = Helper.Translation.Get("level-up-notification").ToString();
                    Game1.addHUDMessage(HUDMessage.ForCornerTextbox(levelUpMessage));
                }

                numOfLevelUpsToday++;
            }
        }

        private void ResetLevelUpCounter(object? sender, EventArgs e)
        {
            numOfLevelUpsToday = 0;
        }
    }
}