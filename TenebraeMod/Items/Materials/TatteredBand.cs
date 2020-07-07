using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TenebraeMod.Items.Materials
{
    public class TatteredBand : ModItem
    { 
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 20;
            item.value = 0;
            item.rare = -1;
            item.maxStack = 99;
        }
    
    
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Leather,3);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}