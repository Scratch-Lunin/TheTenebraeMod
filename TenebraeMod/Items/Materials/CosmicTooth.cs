using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TenebraeMod.NPCs;
using static Terraria.ModLoader.ModContent;

namespace TenebraeMod.Items.Materials
{
    public class CosmicTooth : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Unnervingly sharp'");
        }
        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 28;
            item.value = 3500;
            item.rare = 12;
            item.maxStack = 99;
        }
    }
        public class CosmicToothDrops : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == ModContent.NPCType<QuasarCrawlerHead>())
            {
                if (Main.rand.NextFloat() < .33f) // 13.23% chance
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CosmicTooth"), 1);
                }
            }
        }
    }
}