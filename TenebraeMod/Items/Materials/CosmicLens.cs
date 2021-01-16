using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TenebraeMod.NPCs;
using static Terraria.ModLoader.ModContent;

namespace TenebraeMod.Items.Materials
{
    public class CosmicLens : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Cloudy yet shiny'");
        }

        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 18;
            item.value = 3000;
            item.rare = 12;
            item.maxStack = 99;
        }
    }
        public class CosmicLensDrops : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == ModContent.NPCType<NebulaicWatcher>())
            {
                if (Main.rand.NextFloat() < .33f) // 13.23% chance
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CosmicLens"), 1);
                }
            }
        }
    }
}