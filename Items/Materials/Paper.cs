using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TenebraeMod.Items.Materials
{
    public class Paper : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.value = 3;
            item.rare = ItemRarityID.White;
            item.maxStack = 999;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Hay);
            recipe.AddTile(TileID.Anvils);
            recipe.needWater = true;
            recipe.SetResult(this, 3);
            recipe.AddRecipe();
        }
    }
}