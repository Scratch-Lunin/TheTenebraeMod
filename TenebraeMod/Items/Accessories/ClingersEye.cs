using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TenebraeMod.Items.Accessories
{
    public class ClingersEye : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Clinger's Eye");
            Tooltip.SetDefault("Provides immunity to Cursed Inferno");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 34;
            item.value = 10000;
            item.rare = 6;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[39] = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(522, 15);
            recipe.AddIngredient(549);
            recipe.AddIngredient(891);
            recipe.AddTile(114);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}