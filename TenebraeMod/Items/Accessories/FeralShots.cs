using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TenebraeMod.Items.Accessories
{
    public class FeralShots : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Provides immunity to Weak and Feral Bite");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = 10000;
            item.rare = 6;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[33] = true;
            player.buffImmune[148] = true;
        }

        public class FeralShotsDrops : GlobalNPC
        {
            public override void NPCLoot(NPC npc)
            {
                if (npc.type == 93 || npc.type == 137 || npc.type == 151 || npc.type == 152 || npc.type == 158 || npc.type == 159)
                {
                    if (NPC.downedMechBoss3 == true || NPC.downedMechBoss2 == true || NPC.downedMechBoss1 == true)
                    {
                        if (Main.rand.Next(20) == 1)
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FeralShots"), 1);
                        }
                    }
                }
            }
        }
    }
}