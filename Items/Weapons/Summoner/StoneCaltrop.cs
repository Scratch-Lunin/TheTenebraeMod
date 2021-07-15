using Microsoft.Xna.Framework;
using TenebraeMod.Projectiles.Summoner;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Items.Weapons.Summoner
{
    public class StoneCaltrop : ModItem
	{
		private int timer;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stone Caltrop");
			Tooltip.SetDefault("Summons a stone caltrop to damage your foes" + "\nIgnores 5 defense");
		}

		public override void SetDefaults()
		{
			item.damage = 3;
			item.knockBack = 0f;
			item.width = 24;
			item.height = 40;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.sellPrice(0, 0, 2, 0);
			item.rare = ItemRarityID.White;
			item.UseSound = SoundID.Item44;
			item.autoReuse = true;

			// These below are needed for a minion weapon
			item.noMelee = true;
			item.summon = true;
			item.sentry = true;
			item.shoot = ModContent.ProjectileType<StoneCaltropSentry>();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			position = Main.MouseWorld;
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
			player.UpdateMaxTurrets();
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.StoneBlock, 15);
			recipe.AddIngredient(ItemID.Wood, 5);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
