using System;
using StardewModdingAPI;
using StardewValley;
using System.Collections.Generic;

namespace InstantToolUpgrades
{
    public class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
        }

        public bool CanEdit<T>(IAssetInfo asset)
        {
            if (asset.Name.ToString().Contains("StringsFromCSFiles"))
            {
                return true;
            }
            return false;
        }

        public void Edit<T>(IAssetData asset)
        {
            IDictionary<string, string> data = ((IAssetData<IDictionary<string, string>>)(object)asset.AsDictionary<string, string>()).Data;
            data["ShopMenu.cs.11474"] = Helper.Translation.Get("crafting-window");
            data["Tool.cs.14317"] = Helper.Translation.Get("post-purchase-dialogue");
        }

        private void OnUpdateTicked(object sender, EventArgs e)
        {
            if (((NetFieldBase<int, NetInt>)(object)Game1.player.daysLeftForToolUpgrade).Value > 0)
            {
                Tool value = ((NetFieldBase<Tool, NetRef<Tool>>)(object)Game1.player.toolBeingUpgraded).Value;
                GenericTool genericTool = (GenericTool)(object)((value is GenericTool) ? value : null);
                if (genericTool != null)
                {
                    ((Tool)genericTool).actionWhenClaimed();
                }
                else
                {
                    Game1.player.addItemToInventory((Item)(object)((NetFieldBase<Tool, NetRef<Tool>>)(object)Game1.player.toolBeingUpgraded).Value);
                }
                ((NetFieldBase<Tool, NetRef<Tool>>)(object)Game1.player.toolBeingUpgraded).Value = null;
                ((NetFieldBase<int, NetInt>)(object)Game1.player.daysLeftForToolUpgrade).Value = 0;
            }
        }
    }
}