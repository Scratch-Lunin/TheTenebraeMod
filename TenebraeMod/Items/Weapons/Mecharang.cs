using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace TenebraeMod.Items.Weapons
{
    public class Mecharang : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Mecharang");
            Tooltip.SetDefault("'Now the rib shoots lasers? This is peculiar indeed.'");
        }

        public override void SetDefaults() {
            item.melee = true;
            item.damage = 55;
            item.width = 22;
            item.height = 36;
            item.useTime = 25;
            item.useAnimation = 25;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 10;
            item.value = 75000;
            item.rare = ItemRarityID.Pink;
            item.shootSpeed = 14f;
            item.shoot = mod.ProjectileType ("MecharangProjectile");
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override bool CanUseItem(Player player)       //this make that you can shoot only 1 boomerang at once
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == item.shoot)
                {
                    return false;
                }
            }
            return true;
        }

 		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Skelerang");
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddIngredient(ItemID.SoulofFright, 20);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}