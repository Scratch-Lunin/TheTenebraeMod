using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;

namespace TenebraeMod.Items.Weapons
{
    public class NebulaVaporwave : ModItem
    {
        public override void SetStaticDefaults()
        {
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 12));
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 42;
            item.value = 100000;
            item.rare = 10;
            item.damage = 120;
            item.magic = true;
            item.knockBack = 4f;
            item.holdStyle = 3;
            item.useStyle = 4;
            item.shoot = 634;
            item.shootSpeed = 5f;
            item.noMelee = true;
            item.mana = 15;
            item.useTime = 40;
            item.useAnimation = 40;
            item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FragmentNebula, 18);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}