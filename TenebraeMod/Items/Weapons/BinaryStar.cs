using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using TenebraeMod.Projectiles;
using System;

namespace TenebraeMod.Items.Weapons
{
	public class BinaryStar : ModItem
	{
        private int channelTimer;
        private bool consume;

		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Binary Star");
			Tooltip.SetDefault("'Melt your foes with double the firepower!'");
		}

		public override void SetDefaults() {
			item.damage = 160;
			item.melee = true;
			item.width = 60;
			item.height = 40;
			item.useTime = 40;
			item.useAnimation = 40;
			item.useStyle = 5;
			item.noMelee = true;
            item.noUseGraphic = true;
			item.knockBack = 6.5f;
			item.value = 100000;
			item.rare = 10;
			item.UseSound = SoundID.Item1;
			item.shoot = ProjectileType<BinaryStarProjectile>();
			item.shootSpeed = 15.9f;
            item.channel = true;
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FragmentSolar, 18);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int shot = Projectile.NewProjectile(player.Center,new Vector2(speedX,speedY),type,damage,knockBack,player.whoAmI,0,0);
			(Main.projectile[shot].modProjectile as BinaryStarProjectile).theta = Math.PI;
            return true;
		}
	}
}
