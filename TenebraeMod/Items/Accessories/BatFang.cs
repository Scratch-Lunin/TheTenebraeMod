using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TenebraeMod.Items.Accessories
{
    public class BatFang : ModItem
    { 
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = 10000;
            item.rare = 4;
            item.maxStack = 999;
        }
    }

    public class BatFangDrops : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == 93 || npc.type == 137 || npc.type == 151 || npc.type == 152 || npc.type == 158 || npc.type == 159)
            {
                if (Main.rand.Next(20) == 1)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BatFang"), 1);
                }
            }
        }
    }
}