using Microsoft.Xna.Framework;
using TenebraeMod.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Items.Weapons {
	public class StingNeedle : ModItem {
		int projectilecount = 0; // Variable for checking how many projectiles have been fired in a burst
		int maxprojectiles = 5; // Change this value to change how many projectiles are fired each burst
		float scope = 25f; // The scope of the weapon as an angle

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Sting Needle");
			Tooltip.SetDefault("Casts a barrage of stingers");
		}

		public override void SetDefaults() {
			item.damage = 13;
			item.width = 36;
			item.width = 34;
			item.useTime = 5;
			item.useAnimation = maxprojectiles * 5; // Projectiles will fire the same rate as useTime during the duration of the useAnimation
			item.reuseDelay = maxprojectiles * 5;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.autoReuse = true;
			item.knockBack = 5;
			item.value = Item.sellPrice(0, 15, 0, 0);
			item.rare = ItemRarityID.Blue;
			item.mana = 10;
			item.magic = true;
			item.noMelee = true;
			item.shoot = ModContent.ProjectileType<FriendlyStinger>();
			item.shootSpeed = 8f;
		}

		public override bool UseItem(Player player) {
			projectilecount = 0;
			return true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			if (projectilecount > (maxprojectiles - 1)) { // Make sure that the projectile counter value is not over the max projectile value (minus one)
				projectilecount = 0;
			}
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians((scope / 2) - (projectilecount * (scope / (maxprojectiles - 1)))));
            Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			Main.PlaySound(SoundID.Item17, player.Center);
			projectilecount++;
            return false;
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.JungleSpores, 10);
			recipe.AddIngredient(ItemID.Stinger, 15);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}