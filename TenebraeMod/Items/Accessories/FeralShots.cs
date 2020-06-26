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

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "BatFang", 3);
            recipe.AddIngredient(892);
            recipe.AddIngredient(548, 5);
            recipe.AddTile(114);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}