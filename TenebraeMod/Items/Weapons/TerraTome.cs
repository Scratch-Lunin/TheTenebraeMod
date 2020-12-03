using Microsoft.Xna.Framework;
using TenebraeMod.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Items.Weapons
{
	public class TerraTome : ModItem
	{ // TODO: Balance weapon
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Terra Tome");
			Tooltip.SetDefault("Shoots a homing accelerating terra orb"+"\nRight click to summon a larger, stationary terra orb at the cursor");
		}

		public override void SetDefaults()
		{
			item.damage = 66;
			item.width = 46;
			item.height = 48;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.knockBack = 4f;
			item.value = Item.sellPrice(0, 20, 0, 0);
			item.rare = ItemRarityID.Yellow;
			item.mana = 15;
			item.magic = true;
			item.noMelee = true;
			item.shoot = ModContent.ProjectileType<TerraSphere>();
			item.shootSpeed = 2f;
		}

        public override bool CanRightClick()
        {
			return true;
        }

        public override bool AltFunctionUse(Player player)
        {
			return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse == 2)
			{
				type = ModContent.ProjectileType<BigTerraSphere>();
				position = Main.MouseWorld;
				speedX = 0;
				speedY = 0;
			} else
			{
				Vector2 speed = item.shootSpeed*(Main.MouseWorld - player.Center).SafeNormalize(Vector2.Zero);
				speedX = speed.X;
				speedY = speed.Y;
			}
			return true;
		}

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<TrueDarklight>());
			recipe.AddIngredient(ModContent.ItemType<TrueArondight>());
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}