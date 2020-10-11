using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TenebraeMod.Items.Materials
{
    public class NeoniteAlloyPlating : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Sturdy, sleek, and stainless'");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.value = 0;
            item.rare = ItemRarityID.Orange;
            item.maxStack = 99;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "NeoniteShard", 5);
            recipe.AddRecipeGroup("TenebraeMod:CobaltBar", 1);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}