using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TenebraeMod.Items.Accessories
{
    public class ReflectiveGlove : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Provides immunity to Stoned, Chilled, and Frozen");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = 10000;
            item.rare = 4;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[156] = true;
            player.buffImmune[46] = true;
            player.buffImmune[47] = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(3781);
            recipe.AddIngredient(1921);
            recipe.AddTile(114);
            recipe.SetResult(this);
            recipe.AddRecipe();

            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(901);
            recipe2.AddIngredient(902);
            recipe2.AddIngredient(903);
            recipe2.AddIngredient(904);
            recipe2.AddIngredient(888);
            recipe2.AddIngredient(null, "ReflectiveGlove");
            recipe2.AddTile(114);
            recipe2.SetResult(1612);
            recipe2.AddRecipe();
        }
    }

    public class AnkhChange : GlobalItem
    {
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.type == (1612 | 1613))
            {
                player.buffImmune[156] = true;
                player.buffImmune[47] = true;
            }
        }
    }

    public class HandWarmerDrops : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == (147 | 184 | 150 | 206))
            {
                if (Main.rand.Next(100) == 1)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1921, 1);
                }
            }

            if (npc.type == (243))
            {
                if (Main.rand.Next(4) == 1)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1921, 1);
                }
            }
        }
    }
}