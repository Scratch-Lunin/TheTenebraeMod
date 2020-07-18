using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TenebraeMod.Items.Materials
{
    public class WornCloth : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 28;
            item.value = 3;
            item.rare = 0;
            item.maxStack = 999;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Paper",3);
            recipe.AddIngredient(ItemID.TatteredCloth);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 3);
            recipe.AddRecipe();
        }
    }
}