using System;
using StardewModdingAPI;
using StardewValley;
using System.Collections.Generic;

namespace InstantToolUpgrades
{
    public class ModEntry : Mod, IAssetEditor
    {
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
        }

        public bool CanEdit<T>(IAssetInfo asset)
        {
            if(asset.AssetNameEquals("Strings/StringsFromCSFiles"))
            {
                return true;
            }
            return false;
        }

        public void Edit<T>(IAssetData asset)
        {
            if(asset.AssetNameEquals("Strings/StringsFromCSFiles"))
            {
                IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                data["ShopMenu.cs.11474"] = "I can upgrade your tools with more power.";
                data["Tool.cs.14317"] = "Thanks. And... Here you go. Enjoy the new tool!";
            }
        }

        private void OnUpdateTicked(object sender, EventArgs e)
        {
            // Any time the player submits a tool to be upgraded...
            if (Game1.player.daysLeftForToolUpgrade.Value > 0)
            {
                // Give the player the tool right away and cancel the upgrade.
                Game1.player.addItemToInventory(Game1.player.toolBeingUpgraded.Value);
                Game1.player.toolBeingUpgraded.Value = null;
            }
        }
	}
}