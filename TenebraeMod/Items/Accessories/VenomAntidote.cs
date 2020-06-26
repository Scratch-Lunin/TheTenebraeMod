using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TenebraeMod.Items.Accessories
{
    public class VenomAntidote : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Provides immunity to Venom and Poison");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = 10000;
            item.rare = 5;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[20] = true;
            player.buffImmune[70] = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(2607, 10);
            recipe.AddIngredient(1339, 5);
            recipe.AddIngredient(887);
            recipe.AddTile(114);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}