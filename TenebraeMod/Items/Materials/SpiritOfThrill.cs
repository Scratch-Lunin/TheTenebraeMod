using System.Drawing.Text;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TenebraeMod.Items.Materials
{
    class SpiritOfThrill : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirit of Thrill");
            Tooltip.SetDefault("'The essence of Pumpking'");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 12));
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 38;
            item.value = Item.sellPrice(silver: 20);
            item.rare = ItemRarityID.LightPurple;
            item.maxStack = 999;
        }
    }

    public class SpiritOfThrillDrop : GlobalNPC
    {
        private int number;
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.Pumpking)
            {
                if (Main.invasionProgressWave > 11)
                    number = Main.rand.Next(3, 6);
                else
                    number = Main.rand.Next(5, 11);

                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<SpiritOfThrill>(), number);
            }
        }
    }
}