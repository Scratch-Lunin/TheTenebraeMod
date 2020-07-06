using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TenebraeMod.Items.Accessories
{
    public class FlameCharm : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Provides immunity to On Fire");
        }

        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 30;
            item.value = 10000;
            item.rare = 6;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[24] = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "LavaWings");
            recipe.AddIngredient(906);
            recipe.AddIngredient(2701, 15);
            recipe.AddTile(114);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}