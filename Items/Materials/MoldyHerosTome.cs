using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TenebraeMod.Items.Materials
{
    public class MoldyHerosTome : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Moldy Hero's Tome");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 32;
            item.value = 100000;
            item.rare = ItemRarityID.Yellow;
            item.maxStack = 99;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BrokenHeroSword);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}