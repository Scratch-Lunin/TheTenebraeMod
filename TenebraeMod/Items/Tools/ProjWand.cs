using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Items.Tools
{
    public class ProjWand : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Projectile Containment Wand");
            Tooltip.SetDefault("A wand with the ability to summon a specific projectile."+"\nTest item");
        }

        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;
            item.maxStack = 1;
            item.value = 666666;
            item.rare = -11;
            item.useStyle = 5;
            item.shoot = 100;
            item.magic = true;
            // Set other item.X values here
        }

        public override void AddRecipes()
        {
            // Recipes here. See Basic Recipe Guide
        }
    }
}