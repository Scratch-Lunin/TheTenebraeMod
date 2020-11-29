using Microsoft.Xna.Framework;
using TenebraeMod.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Items.Weapons {
	public class TrueArondight : ModItem { // TODO: Balance weapon
		int staticusetime = 10;
		int maxprojectiles = 3;

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("True Arondight");
			Tooltip.SetDefault("Spews holy flaming tentacles to burn your foes"+"\nEnemies killed by the tentacles drop healing orbs");
		}

		public override void SetDefaults() {
			item.damage = 50;
			item.width = 28;
			item.width = 30;
			item.useTime = staticusetime;
			item.useAnimation = staticusetime * maxprojectiles;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.UseSound = SoundID.Item103;
			item.autoReuse = true;
			item.knockBack = 4.5f;
			item.value = Item.sellPrice(0, 15, 0, 0);
			item.rare = ItemRarityID.Pink;
			item.mana = 16;
			item.magic = true;
			item.noMelee = true;
			item.shoot = ModContent.ProjectileType<TrueHolyFlame>();
			item.shootSpeed = 11f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 36f;
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
			Projectile.NewProjectile(position, perturbedSpeed, type, damage, knockBack, player.whoAmI);
			perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HallowedBar, 20);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}